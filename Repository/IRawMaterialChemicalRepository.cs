using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IRawMaterialChemicalRepository
  {
    Task AddProductAsync(RawMaterialChemicalDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<RawMaterialChemicalDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<RawMaterialChemicalDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(RawMaterialChemicalDTO productDTO);
    Task<IQueryable<RawMaterialChemicalDTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
