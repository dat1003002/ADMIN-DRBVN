using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IProductStandardCTLRepository
  {
    Task AddProductAsync(ProductStandardCTLDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<ProductStandardCTLDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<ProductStandardCTLDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductStandardCTLDTO productDTO);
    Task<IQueryable<ProductStandardCTLDTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
