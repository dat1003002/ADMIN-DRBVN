using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IProductStandardMSRepository
  {
    Task AddProductAsync(ProductStandardMSDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<ProductStandardMSDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<ProductStandardMSDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductStandardMSDTO productDTO);
    Task<IQueryable<ProductStandardMSDTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
