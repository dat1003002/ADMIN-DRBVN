using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface ICalibrationF2Repository
  {
    Task AddProductAsync(CalibrationF2DTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<CalibrationF2DTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<CalibrationF2DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(CalibrationF2DTO productDTO);
    Task<IQueryable<CalibrationF2DTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
