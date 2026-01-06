using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PDFtoImage;
using SkiaSharp;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class TTBangTaiService : ITTBangTaiService
  {
    private readonly ITTBangTaiRepository _repository;
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _uploadFolder;
    private readonly string _virtualPath = "/images/";
    public TTBangTaiService(ITTBangTaiRepository repository, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
    {
      _repository = repository;
      _context = context;
      _webHostEnvironment = webHostEnvironment;
      _uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
      Directory.CreateDirectory(_uploadFolder);
    }
    public async Task AddProductAsync(TTBangTaiDTO tTBangTaiDTO)
    {
      var product = new Product
      {
        name = tTBangTaiDTO.Name,
        CategoryId = tTBangTaiDTO.CategoryId,
        ProductImages = new List<ProductImage>()
      };
      await ProcessImageFilesAsync(product, tTBangTaiDTO.ImageFiles);
      await ProcessPdfToImagesAsync(product, tTBangTaiDTO.PdfFile);
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
    }
    public Task<IEnumerable<Category>> GetCategoriesAsync() => _repository.GetCategoriesAsync();

    public IQueryable<TTBangTaiDTO> GetProducts(int categoryId)
    {
      return _repository.GetProductsAsync(categoryId).GetAwaiter().GetResult();
    }

    public IQueryable<TTBangTaiDTO> SearchProductsByName(string name, int categoryId)
    {
      return _repository.SearchProductsByNameAsync(name, categoryId).GetAwaiter().GetResult();
    }
    public Task<TTBangTaiDTO?> GetProductByIdAsync(int productId) => _repository.GetProductByIdAsync(productId);
    public async Task UpdateProductAsync(TTBangTaiDTO tTBangTaiDTO)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == tTBangTaiDTO.ProductId);
      if (product == null) return;
      product.name = tTBangTaiDTO.Name;
      product.CategoryId = tTBangTaiDTO.CategoryId;
      if (tTBangTaiDTO.DeletedImagePaths != null && tTBangTaiDTO.DeletedImagePaths.Any())
      {
        var imagesToDelete = product.ProductImages
            .Where(pi => tTBangTaiDTO.DeletedImagePaths.Contains(pi.ImagePath))
            .ToList();
        foreach (var img in imagesToDelete)
        {
          DeletePhysicalFile(img.ImagePath);
        }
        _context.ProductImages.RemoveRange(imagesToDelete);
      }
      await ProcessImageFilesAsync(product, tTBangTaiDTO.ImageFiles);
      await ProcessPdfToImagesAsync(product, tTBangTaiDTO.PdfFile);
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
      const int dpi = 300;
      var options = new RenderOptions(Dpi: dpi);
      var bitmaps = Conversion.ToImagesAsync(pdfStream, options: options);
      int sortOrder = product.ProductImages.Count;
      int pageIndex = 1;
      await foreach (var bitmap in bitmaps)
      {
        var fileName = $"{Guid.NewGuid()}-page{pageIndex}.png";
        var filePath = Path.Combine(_uploadFolder, fileName);
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
