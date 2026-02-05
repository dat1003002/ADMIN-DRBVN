using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IRawMaterialMetalCoreService
  {
    Task AddProductAsync(RawMaterialMetalCoreDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<RawMaterialMetalCoreDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<RawMaterialMetalCoreDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(RawMaterialMetalCoreDTO product);
    Task<IPagedList<RawMaterialMetalCoreDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
