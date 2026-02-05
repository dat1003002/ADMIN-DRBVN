using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface ILabCalibrationF1Service
  {
    Task AddProductAsync(LabCalibrationF1DTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<LabCalibrationF1DTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<LabCalibrationF1DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LabCalibrationF1DTO product);
    Task<IPagedList<LabCalibrationF1DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
