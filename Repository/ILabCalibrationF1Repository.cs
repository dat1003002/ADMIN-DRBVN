using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface ILabCalibrationF1Repository
  {
    Task AddProductAsync(LabCalibrationF1DTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<LabCalibrationF1DTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<LabCalibrationF1DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LabCalibrationF1DTO productDTO);
    Task<IQueryable<LabCalibrationF1DTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
