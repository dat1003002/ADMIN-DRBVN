using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IQCEmployeeF1Service
  {
    Task AddProductAsync(QCEmployeeF1DTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<QCEmployeeF1DTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<QCEmployeeF1DTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(QCEmployeeF1DTO product);
    Task<IEnumerable<QCEmployeeF1DTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<IPagedList<QCEmployeeF1DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
