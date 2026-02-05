using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IRawMaterialChemicalService
  {
    Task AddProductAsync(RawMaterialChemicalDTO product);
    Task<IEnumerable<Category>> GetCategories();
    Task<IPagedList<RawMaterialChemicalDTO>> GetProducts(int categoryId, int pageNumber, int pageSize);
    Task DeleteProductAsync(int productId);
    Task<RawMaterialChemicalDTO> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(RawMaterialChemicalDTO product);
    Task<IPagedList<RawMaterialChemicalDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize);
  }
}
