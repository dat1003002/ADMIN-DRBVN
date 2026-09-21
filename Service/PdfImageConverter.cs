using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PDFtoImage;
using SkiaSharp;

namespace AspnetCoreMvcFull.Service
{
  public static class PdfImageConverter
  {
    public static async Task<string?> ConvertFirstPageAsync(IFormFile? pdfFile, ModelStateDictionary modelState)
    {
      if (pdfFile == null || pdfFile.Length == 0) return null;

      if (!string.Equals(Path.GetExtension(pdfFile.FileName), ".pdf", StringComparison.OrdinalIgnoreCase))
      {
        modelState.AddModelError("PdfFile", "Chỉ hỗ trợ file định dạng .pdf.");
        return null;
      }

      try
      {
        var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        Directory.CreateDirectory(folder);

        await using var pdfStream = pdfFile.OpenReadStream();
        var options = new RenderOptions(Dpi: 300);

        await foreach (var bitmap in Conversion.ToImagesAsync(pdfStream, options: options))
        {
          using (bitmap)
          {
            var fileName = $"{Guid.NewGuid()}-page1.png";
            using var data = bitmap.Encode(SKEncodedImageFormat.Png, quality: 100);
            await File.WriteAllBytesAsync(Path.Combine(folder, fileName), data.ToArray());
            return fileName;
          }
        }

        modelState.AddModelError("PdfFile", "File PDF không chứa trang nào để chuyển đổi.");
      }
      catch (Exception ex)
      {
        modelState.AddModelError("PdfFile", "Không thể đọc file PDF: " + ex.Message);
      }

      return null;
    }
  }
}
