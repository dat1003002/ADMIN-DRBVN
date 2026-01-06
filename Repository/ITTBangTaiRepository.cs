using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Repository
{
  public interface ITTBangTaiRepository
  {
    Task AddProductAsync(TTBangTaiDTO tTBangTaiDTO);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<IQueryable<TTBangTaiDTO>> GetProductsAsync(int categoryId);
    Task<IQueryable<TTBangTaiDTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<TTBangTaiDTO?> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(TTBangTaiDTO tTBangTaiDTO);
    Task DeleteProductAsync(int productId);
  }
}
