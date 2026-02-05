using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IRawMaterialSteelCordRepository
  {
    Task AddProductAsync(RawMaterialSteelCordDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<RawMaterialSteelCordDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<RawMaterialSteelCordDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(RawMaterialSteelCordDTO productDTO);
    Task<IQueryable<RawMaterialSteelCordDTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
