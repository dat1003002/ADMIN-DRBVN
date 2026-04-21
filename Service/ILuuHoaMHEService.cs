using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace AspnetCoreMvcFull.Service
{
  public interface ILuuHoaMHEService
  {
    Task AddProductAsync(LuuHoaMHEDTO luuHoaMHEDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<LuuHoaMHEDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int ProductId);
    Task<LuuHoaMHEDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LuuHoaMHEDTO product);
    Task<IEnumerable<LuuHoaMHEDTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<IPagedList<LuuHoaMHEDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);

    Task<string> ProcessAndSaveFileAsync(IFormFile? pdfFile, IFormFile? imageFile, string imageFolderPath);

    Task<string> ProcessAndSaveReplacementFileAsync(IFormFile? pdfFile, IFormFile? imageFile, string imageFolderPath, string? currentImageName);
  }
}
