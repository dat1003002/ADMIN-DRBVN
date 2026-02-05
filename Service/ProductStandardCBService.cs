using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using X.PagedList;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class ProductStandardCBService : IProductStandardCBService
  {
    private readonly IProductStandardCBRepository _repo;

    public ProductStandardCBService(IProductStandardCBRepository repo)
    {
      _repo = repo;
    }

    public async Task AddProductAsync(ProductStandardCBDTO product)
        => await _repo.AddProductAsync(product);

    public async Task<IEnumerable<Category>> GetCategories()
        => await _repo.GetCategories();

    public async Task<IPagedList<ProductStandardCBDTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
    {
      var query = await _repo.GetProducts(categoryId);
      return await query.ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task DeleteProductAsync(int productId)
        => await _repo.DeleteProductAsync(productId);

    public async Task<ProductStandardCBDTO> GetProductByIdAsync(int productId)
        => await _repo.GetProductByIdAsync(productId);

    public async Task UpdateProductAsync(ProductStandardCBDTO product)
        => await _repo.UpdateProductAsync(product);

    public async Task<IPagedList<ProductStandardCBDTO>> SearchProductsByNameAsync(
        string name, int categoryId, int page, int pageSize)
    {
      var query = await _repo.SearchProductsByNameAsync(name, categoryId);
      return await query.ToPagedListAsync(page, pageSize);
    }
  }
}
