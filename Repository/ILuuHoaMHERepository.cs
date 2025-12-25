using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Repository
{
  public interface ILuuHoaMHERepository
  {
    Task AddProductAsync(LuuHoaMHEDTO luuHoaMHEDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<LuuHoaMHEDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int ProductId);
    Task<LuuHoaMHEDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LuuHoaMHEDTO product);
    Task<IQueryable<LuuHoaMHEDTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<IEnumerable<LuuHoaMHEDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
