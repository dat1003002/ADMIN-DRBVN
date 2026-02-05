using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IQCEmployeeF2Repository
  {
    Task AddProductAsync(QCEmployeeF2DTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<QCEmployeeF2DTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<QCEmployeeF2DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(QCEmployeeF2DTO productDTO);
    Task<IQueryable<QCEmployeeF2DTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<IEnumerable<QCEmployeeF2DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
