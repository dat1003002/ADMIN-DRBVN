using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IRawMaterialSteelCordService
  {
    Task AddProductAsync(RawMaterialSteelCordDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<RawMaterialSteelCordDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<RawMaterialSteelCordDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(RawMaterialSteelCordDTO product);
    Task<IPagedList<RawMaterialSteelCordDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
