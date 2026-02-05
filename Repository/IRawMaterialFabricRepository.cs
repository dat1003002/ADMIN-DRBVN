using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IRawMaterialFabricRepository
  {
    Task AddProductAsync(RawMaterialFabricDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<RawMaterialFabricDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<RawMaterialFabricDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(RawMaterialFabricDTO productDTO);
    Task<IQueryable<RawMaterialFabricDTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
