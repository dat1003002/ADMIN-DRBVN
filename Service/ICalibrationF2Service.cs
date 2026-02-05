using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface ICalibrationF2Service
  {
    Task AddProductAsync(CalibrationF2DTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<CalibrationF2DTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<CalibrationF2DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(CalibrationF2DTO product);
    Task<IPagedList<CalibrationF2DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
