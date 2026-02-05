using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IProductStandardCBRepository
  {
    Task AddProductAsync(ProductStandardCBDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<ProductStandardCBDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<ProductStandardCBDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductStandardCBDTO productDTO);
    Task<IQueryable<ProductStandardCBDTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
