using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface ILabEquipF1DailyCheckRepository
  {
    Task AddProductAsync(LabEquipF1DailyCheckDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<LabEquipF1DailyCheckDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<LabEquipF1DailyCheckDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LabEquipF1DailyCheckDTO productDTO);
    Task<IQueryable<LabEquipF1DailyCheckDTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
