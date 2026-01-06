using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Service
{
  public interface IMSService
  {
    Task AddProductAsync(MSDTO mSDTO);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<IQueryable<MSDTO>> GetProductsAsync(int categoryId);
    Task<IQueryable<MSDTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<MSDTO?> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(MSDTO mSDTO);
    Task DeleteProductAsync(int productId);
  }
}
