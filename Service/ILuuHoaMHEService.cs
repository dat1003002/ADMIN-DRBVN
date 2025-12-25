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

    // Phương thức xử lý file khi tạo mới
    Task<string> ProcessAndSaveFileAsync(IFormFile? pdfFile, IFormFile? imageFile, string imageFolderPath);

    // Phương thức mới: xử lý thay thế file khi chỉnh sửa (có xóa ảnh cũ)
    Task<string> ProcessAndSaveReplacementFileAsync(IFormFile? pdfFile, IFormFile? imageFile, string imageFolderPath, string? currentImageName);
  }
}
