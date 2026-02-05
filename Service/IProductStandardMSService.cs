using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IProductStandardMSService
  {
    Task AddProductAsync(ProductStandardMSDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<ProductStandardMSDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<ProductStandardMSDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(ProductStandardMSDTO product);
    Task<IPagedList<ProductStandardMSDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
