using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IProductStandardCTLService
  {
    Task AddProductAsync(ProductStandardCTLDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<ProductStandardCTLDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<ProductStandardCTLDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductStandardCTLDTO product);
    Task<IPagedList<ProductStandardCTLDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
