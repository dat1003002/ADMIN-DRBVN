using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IQCEmployeeF2Service
  {
    Task AddProductAsync(QCEmployeeF2DTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<QCEmployeeF2DTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<QCEmployeeF2DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(QCEmployeeF2DTO product);
    Task<IEnumerable<QCEmployeeF2DTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<IPagedList<QCEmployeeF2DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
