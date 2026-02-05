using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface ILaboratoryEquipmentF2Repository
  {
    Task AddProductAsync(LaboratoryEquipmentF2DTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<LaboratoryEquipmentF2DTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<LaboratoryEquipmentF2DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LaboratoryEquipmentF2DTO productDTO);
    Task<IQueryable<LaboratoryEquipmentF2DTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
