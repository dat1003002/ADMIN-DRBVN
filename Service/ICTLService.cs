using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;

public interface ICTLService
{
  Task AddProductAsync(CTLDTO ctlDTO);
  Task<IEnumerable<Category>> GetCategoriesAsync();
  Task<IQueryable<CTLDTO>> GetProductsAsync(int categoryId);
  Task<IQueryable<CTLDTO>> SearchProductsByNameAsync(string name, int categoryId);
  Task<CTLDTO?> GetProductByIdAsync(int productId);
  Task UpdateProductAsync(CTLDTO ctlDTO);
  Task DeleteProductAsync(int productId);
}
