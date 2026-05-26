using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface ICTLRepository
  {
    Task AddProductAsync(CTLDTO ctlDTO);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    IQueryable<CTLDTO> GetProducts(int categoryId);
    IQueryable<CTLDTO> SearchProductsByName(string name, int categoryId);
    Task<CTLDTO?> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(CTLDTO ctlDTO);
    Task DeleteProductAsync(int productId);
  }
}
