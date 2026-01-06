using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
namespace AspnetCoreMvcFull.Service
{
  public interface ITTBangTaiService
  {
    Task AddProductAsync(TTBangTaiDTO tTBangTaiDTO);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    IQueryable<TTBangTaiDTO> GetProducts(int categoryId);
    IQueryable<TTBangTaiDTO> SearchProductsByName(string name, int categoryId);
    Task<TTBangTaiDTO?> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(TTBangTaiDTO tTBangTaiDTO);
    Task DeleteProductAsync(int productId);
  }
}
