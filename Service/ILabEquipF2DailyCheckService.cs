using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface ILabEquipF2DailyCheckService
  {
    Task AddProductAsync(LabEquipF2DailyCheckDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<LabEquipF2DailyCheckDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<LabEquipF2DailyCheckDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LabEquipF2DailyCheckDTO product);
    Task<IPagedList<LabEquipF2DailyCheckDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
