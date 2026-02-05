using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface ILabEquipF2DailyCheckRepository
  {
    Task AddProductAsync(LabEquipF2DailyCheckDTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<LabEquipF2DailyCheckDTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<LabEquipF2DailyCheckDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(LabEquipF2DailyCheckDTO productDTO);
    Task<IQueryable<LabEquipF2DailyCheckDTO>> SearchProductsByNameAsync(string name, int categoryId);
  }
}
