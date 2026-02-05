using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IProductStandardCBService
  {
    Task AddProductAsync(ProductStandardCBDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<ProductStandardCBDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<ProductStandardCBDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductStandardCBDTO product);
    Task<IPagedList<ProductStandardCBDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
