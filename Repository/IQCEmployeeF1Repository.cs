using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IQCEmployeeF1Repository
  {
    Task AddProductAsync(QCEmployeeF1DTO productDTO);
    Task<IEnumerable<Category>> GetCategories();
    Task<IQueryable<QCEmployeeF1DTO>> GetProducts(int categoryId);
    Task DeleteProductAsync(int productId);
    Task<QCEmployeeF1DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(QCEmployeeF1DTO productDTO);
    Task<IQueryable<QCEmployeeF1DTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<IEnumerable<QCEmployeeF1DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
