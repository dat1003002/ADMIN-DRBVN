using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface ILaboratoryEquipmentF2Service
  {
    Task AddProductAsync(LaboratoryEquipmentF2DTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<LaboratoryEquipmentF2DTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<LaboratoryEquipmentF2DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LaboratoryEquipmentF2DTO product);
    Task<IPagedList<LaboratoryEquipmentF2DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
