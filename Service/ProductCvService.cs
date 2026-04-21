using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Service
{
  public class ProductCvService : IProductCvService
  {
    private readonly IProductCvRepository _productCvRepository;

    public ProductCvService(IProductCvRepository productCvRepository)
    {
      _productCvRepository = productCvRepository;
    }

    public async Task AddProductAsync(ProductDTO productDTO)
    {
      await _productCvRepository.AddProductAsync(productDTO);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _productCvRepository.GetCategories();
    }

    public async Task<IPagedList<ProductDTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
    {
      var query = await _productCvRepository.GetProducts(categoryId);
      return await query.ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task DeleteProductAsync(int ProductId)
    {
      await _productCvRepository.DeleteProductAsync(ProductId);
    }

    public async Task<ProductDTO> GetProductByIdAsync(int productId)
    {
      return await _productCvRepository.GetProductByIdAsync(productId);
    }

    public async Task UpdateProductAsync(ProductDTO productDTO)
    {
      await _productCvRepository.UpdateProductAsync(productDTO);
    }

    public async Task<IQueryable<ProductDTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      return await _productCvRepository.SearchProductsByNameAsync(name, categoryId);
    }

    public async Task<IPagedList<ProductDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
    {
      var query = await _productCvRepository.SearchProductsByNameAsync(name, categoryId);
      return await query.ToPagedListAsync(page, pageSize);
    }
  }
}
