using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface ILabEquipF1DailyCheckService
  {
    Task AddProductAsync(LabEquipF1DailyCheckDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<LabEquipF1DailyCheckDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<LabEquipF1DailyCheckDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LabEquipF1DailyCheckDTO product);
    Task<IPagedList<LabEquipF1DailyCheckDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
