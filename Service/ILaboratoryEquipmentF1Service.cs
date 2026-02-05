using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface ILaboratoryEquipmentF1Service
  {
    Task AddProductAsync(LaboratoryEquipmentF1DTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<LaboratoryEquipmentF1DTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<LaboratoryEquipmentF1DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LaboratoryEquipmentF1DTO product);
    Task<IPagedList<LaboratoryEquipmentF1DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
