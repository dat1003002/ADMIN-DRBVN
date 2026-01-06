using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;

namespace AspnetCoreMvcFull.Repository
{
  public interface IMSRepository
  {
    Task AddProductAsync(MSDTO mSDTO);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    IQueryable<MSDTO> GetProducts(int categoryId);
    IQueryable<MSDTO> SearchProductsByName(string name, int categoryId);
    Task<MSDTO?> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(MSDTO mSDTO);
    Task DeleteProductAsync(int productId);
  }
}
