using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IProductStandardMHERepository
  {
    Task AddProductAsync(ProductStandardMHEDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<ProductStandardMHEDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<ProductStandardMHEDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductStandardMHEDTO productDTO);
    Task<IQueryable<ProductStandardMHEDTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
