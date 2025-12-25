using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PDFtoImage;
using SkiaSharp; // Giữ nguyên để sử dụng SKBitmap và SKEncodedImageFormat
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class BangTaiService : IBangTaiService
  {
    private readonly IBangTaiRepository _repository;
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _uploadFolder; // wwwroot/images
    private readonly string _virtualPath = "/images/"; // Đường dẫn ảo lưu vào DB

    public BangTaiService(IBangTaiRepository repository, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
      _repository = repository;
      _context = context;
      _webHostEnvironment = webHostEnvironment;
      _uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
      Directory.CreateDirectory(_uploadFolder);
    }

    public async Task AddProductAsync(BangTaiDTO bangTaiDTO)
    {
      var product = new Product
      {
        name = bangTaiDTO.Name,
        CategoryId = bangTaiDTO.CategoryId,
        ProductImages = new List<ProductImage>()
      };
      await ProcessImageFilesAsync(product, bangTaiDTO.ImageFiles);
      await ProcessPdfToImagesAsync(product, bangTaiDTO.PdfFile);
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
    }

    public Task<IEnumerable<Category>> GetCategoriesAsync() => _repository.GetCategoriesAsync();
    public Task<IQueryable<BangTaiDTO>> GetProductsAsync(int categoryId) => Task.FromResult(_repository.GetProducts(categoryId));
    public Task<IQueryable<BangTaiDTO>> SearchProductsByNameAsync(string name, int categoryId) => Task.FromResult(_repository.SearchProductsByName(name, categoryId));
    public Task<BangTaiDTO?> GetProductByIdAsync(int productId) => _repository.GetProductByIdAsync(productId);

    // ... các phần khác giữ nguyên ...

    public async Task UpdateProductAsync(BangTaiDTO bangTaiDTO)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == bangTaiDTO.ProductId);

      if (product == null) return;

      product.name = bangTaiDTO.Name;
      product.CategoryId = bangTaiDTO.CategoryId;

      // XÓA CÁC ẢNH ĐƯỢC ĐÁNH DẤU (chỉ khi submit)
      if (bangTaiDTO.DeletedImagePaths != null && bangTaiDTO.DeletedImagePaths.Any())
      {
        var imagesToDelete = product.ProductImages
            .Where(pi => bangTaiDTO.DeletedImagePaths.Contains(pi.ImagePath))
            .ToList();

        foreach (var img in imagesToDelete)
        {
          DeletePhysicalFile(img.ImagePath);
        }

        _context.ProductImages.RemoveRange(imagesToDelete);
      }

      // Thêm ảnh mới và PDF mới (nếu có)
      await ProcessImageFilesAsync(product, bangTaiDTO.ImageFiles);
      await ProcessPdfToImagesAsync(product, bangTaiDTO.PdfFile);

      await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int productId)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == productId);
      if (product != null)
      {
        foreach (var img in product.ProductImages)
        {
          DeletePhysicalFile(img.ImagePath);
        }
      }
      await _repository.DeleteProductAsync(productId);
    }

    private async Task ProcessImageFilesAsync(Product product, List<IFormFile> imageFiles)
    {
      if (imageFiles == null || !imageFiles.Any()) return;
      int sortOrder = product.ProductImages.Count;
      foreach (var file in imageFiles)
      {
        if (file.Length <= 0) continue;
        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(_uploadFolder, fileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);
        product.ProductImages.Add(new ProductImage
        {
          ImagePath = _virtualPath + fileName,
          IsMain = !product.ProductImages.Any(i => i.IsMain),
          SortOrder = sortOrder++,
          IsGeneratedFromPdf = false
        });
      }
    }

    private async Task ProcessPdfToImagesAsync(Product product, IFormFile? pdfFile)
    {
      if (pdfFile == null || pdfFile.Length == 0) return;

      await using var pdfStream = pdfFile.OpenReadStream();

      const int dpi = 300; // DPI cao để ảnh nét (có thể tăng lên 400-600 nếu cần nét hơn, file sẽ lớn hơn)

      var options = new RenderOptions(Dpi: dpi);

      // Render tất cả các trang PDF thành IAsyncEnumerable<SKBitmap>
      var bitmaps = Conversion.ToImagesAsync(pdfStream, options: options);

      int sortOrder = product.ProductImages.Count;
      int pageIndex = 1; // Để đánh số trang và chọn ảnh chính (trang đầu)

      // Sử dụng await foreach để duyệt async stream
      await foreach (var bitmap in bitmaps)
      {
        var fileName = $"{Guid.NewGuid()}-page{pageIndex}.png";
        var filePath = Path.Combine(_uploadFolder, fileName);

        // Lưu SKBitmap thành file PNG lossless (quality 100)
        using var data = bitmap.Encode(SKEncodedImageFormat.Png, quality: 100);
        await File.WriteAllBytesAsync(filePath, data.ToArray());

        product.ProductImages.Add(new ProductImage
        {
          ImagePath = _virtualPath + fileName,
          IsMain = pageIndex == 1 && !product.ProductImages.Any(img => img.IsMain),
          SortOrder = sortOrder++,
          IsGeneratedFromPdf = true
        });

        pageIndex++;
        // Dispose bitmap để giải phóng bộ nhớ (tốt cho PDF nhiều trang)
        bitmap.Dispose();
      }
    }

    private void DeletePhysicalFile(string imagePath)
    {
      if (string.IsNullOrEmpty(imagePath)) return;
      var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath.TrimStart('/'));
      if (File.Exists(fullPath))
      {
        File.Delete(fullPath);
      }
    }
  }
}
