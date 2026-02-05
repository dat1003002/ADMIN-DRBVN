using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IRawMaterialFabricService
  {
    Task AddProductAsync(RawMaterialFabricDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<RawMaterialFabricDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<RawMaterialFabricDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(RawMaterialFabricDTO product);
    Task<IPagedList<RawMaterialFabricDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
