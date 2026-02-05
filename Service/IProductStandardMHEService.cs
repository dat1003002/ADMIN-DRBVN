using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IProductStandardMHEService
  {
    Task AddProductAsync(ProductStandardMHEDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<ProductStandardMHEDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<ProductStandardMHEDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductStandardMHEDTO product);
    Task<IPagedList<ProductStandardMHEDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
