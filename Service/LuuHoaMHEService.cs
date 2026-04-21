using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PDFtoImage;
using SkiaSharp;
using X.PagedList;

namespace AspnetCoreMvcFull.Service
{
  public class LuuHoaMHEService : ILuuHoaMHEService
  {
    private readonly ILuuHoaMHERepository _luuHoaMHERepository;

    public LuuHoaMHEService(ILuuHoaMHERepository luuHoaMHERepository)
    {
      _luuHoaMHERepository = luuHoaMHERepository;
    }

    public async Task AddProductAsync(LuuHoaMHEDTO luuHoaMHEDTO) =>
        await _luuHoaMHERepository.AddProductAsync(luuHoaMHEDTO);

    public async Task DeleteProductAsync(int ProductId) =>
        await _luuHoaMHERepository.DeleteProductAsync(ProductId);

    public Task<IEnumerable<Category>> GetCategories() =>
        _luuHoaMHERepository.GetCategories();

    public async Task<LuuHoaMHEDTO> GetProductByIdAsync(int productId) =>
        await _luuHoaMHERepository.GetProductByIdAsync(productId);

    public async Task<IPagedList<LuuHoaMHEDTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
    {
      var products = await _luuHoaMHERepository.GetProducts(categoryId);
      return await products.ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task<IEnumerable<LuuHoaMHEDTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var products = await _luuHoaMHERepository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToListAsync();
    }

    public async Task<IPagedList<LuuHoaMHEDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
    {
      var products = await _luuHoaMHERepository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToPagedListAsync(page, pageSize);
    }


    public async Task UpdateProductAsync(LuuHoaMHEDTO product) =>
        await _luuHoaMHERepository.UpdateProductAsync(product);

    public async Task<string> ProcessAndSaveFileAsync(IFormFile? pdfFile, IFormFile? imageFile, string imageFolderPath)
    {
      string imageFileName = null;

      if (pdfFile != null && pdfFile.Length > 0)
      {
        var extension = Path.GetExtension(pdfFile.FileName).ToLowerInvariant();

        if (extension != ".pdf")
          throw new InvalidOperationException("Chỉ hỗ trợ file định dạng .pdf.");

        await using var pdfStream = pdfFile.OpenReadStream();

        const int dpi = 300;
        var options = new RenderOptions(Dpi: dpi);
        var bitmaps = Conversion.ToImagesAsync(pdfStream, options: options);

        await foreach (var bitmap in bitmaps.WithCancellation(CancellationToken.None))
        {
          imageFileName = $"{Guid.NewGuid()}-page1.png";
          var imagePath = Path.Combine(imageFolderPath, imageFileName);

          using var data = bitmap.Encode(SKEncodedImageFormat.Png, quality: 100);
          await File.WriteAllBytesAsync(imagePath, data.ToArray());

          bitmap.Dispose();
          break;
        }

        if (imageFileName == null)
          throw new InvalidOperationException("File PDF không chứa trang nào để chuyển đổi.");
      }
      else if (imageFile != null && imageFile.Length > 0)
      {
        var originalFileName = Path.GetFileName(imageFile.FileName);
        var fullPath = Path.Combine(imageFolderPath, originalFileName);

        if (System.IO.File.Exists(fullPath))
        {
          var ext = Path.GetExtension(originalFileName);
          originalFileName = $"{Path.GetFileNameWithoutExtension(originalFileName)}_{Guid.NewGuid()}{ext}";
        }

        fullPath = Path.Combine(imageFolderPath, originalFileName);

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await imageFile.CopyToAsync(stream);

        imageFileName = originalFileName;
      }
      else
      {
        throw new InvalidOperationException("Vui lòng upload ít nhất một file PDF hoặc file ảnh.");
      }

      return imageFileName;
    }

    public async Task<string> ProcessAndSaveReplacementFileAsync(
        IFormFile? pdfFile,
        IFormFile? imageFile,
        string imageFolderPath,
        string? currentImageName)
    {
      string newImageName = currentImageName ?? throw new ArgumentNullException(nameof(currentImageName));

      if (pdfFile != null && pdfFile.Length > 0)
      {
        var extension = Path.GetExtension(pdfFile.FileName).ToLowerInvariant();

        if (extension != ".pdf")
          throw new InvalidOperationException("Chỉ hỗ trợ file định dạng .pdf.");

        if (!string.IsNullOrEmpty(currentImageName))
        {
          var oldPath = Path.Combine(imageFolderPath, currentImageName);

          if (System.IO.File.Exists(oldPath))
            System.IO.File.Delete(oldPath);
        }

        await using var pdfStream = pdfFile.OpenReadStream();

        const int dpi = 300;
        var options = new RenderOptions(Dpi: dpi);
        var bitmaps = Conversion.ToImagesAsync(pdfStream, options: options);

        await foreach (var bitmap in bitmaps.WithCancellation(CancellationToken.None))
        {
          newImageName = $"{Guid.NewGuid()}-page1.png";
          var imagePath = Path.Combine(imageFolderPath, newImageName);

          using var data = bitmap.Encode(SKEncodedImageFormat.Png, quality: 100);
          await File.WriteAllBytesAsync(imagePath, data.ToArray());

          bitmap.Dispose();
          break;
        }
      }
      else if (imageFile != null && imageFile.Length > 0)
      {
        if (!string.IsNullOrEmpty(currentImageName))
        {
          var oldPath = Path.Combine(imageFolderPath, currentImageName);

          if (System.IO.File.Exists(oldPath))
            System.IO.File.Delete(oldPath);
        }

        var originalFileName = Path.GetFileName(imageFile.FileName);
        var fullPath = Path.Combine(imageFolderPath, originalFileName);

        if (System.IO.File.Exists(fullPath))
        {
          var ext = Path.GetExtension(originalFileName);
          originalFileName = $"{Path.GetFileNameWithoutExtension(originalFileName)}_{Guid.NewGuid()}{ext}";
        }

        fullPath = Path.Combine(imageFolderPath, originalFileName);

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await imageFile.CopyToAsync(stream);

        newImageName = originalFileName;
      }

      return newImageName;
    }
  }
}
