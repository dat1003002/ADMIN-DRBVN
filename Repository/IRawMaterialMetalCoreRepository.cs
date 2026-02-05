using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IRawMaterialMetalCoreRepository
  {
    Task AddProductAsync(RawMaterialMetalCoreDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<RawMaterialMetalCoreDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<RawMaterialMetalCoreDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(RawMaterialMetalCoreDTO productDTO);
    Task<IQueryable<RawMaterialMetalCoreDTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
