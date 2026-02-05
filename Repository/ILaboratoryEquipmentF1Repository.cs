using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface ILaboratoryEquipmentF1Repository
  {
    Task AddProductAsync(LaboratoryEquipmentF1DTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<LaboratoryEquipmentF1DTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<LaboratoryEquipmentF1DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LaboratoryEquipmentF1DTO productDTO);
    Task<IQueryable<LaboratoryEquipmentF1DTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
