using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class RawMaterialSteelCordService : IRawMaterialSteelCordService
  {
    private readonly IRawMaterialSteelCordRepository _repo;

    public RawMaterialSteelCordService(IRawMaterialSteelCordRepository repo)
    {
      _repo = repo;
    }

    public async Task AddProductAsync(RawMaterialSteelCordDTO product) => await _repo.AddProductAsync(product);
    public async Task<IEnumerable<Category>> GetCategories() => await _repo.GetCategories();
    public async Task<IPagedList<RawMaterialSteelCordDTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
        => await (await _repo.GetProducts(categoryId)).ToPagedListAsync(pageNumber, pageSize);
    public async Task DeleteProductAsync(int productId) => await _repo.DeleteProductAsync(productId);
    public async Task<RawMaterialSteelCordDTO> GetProductByIdAsync(int productId) => await _repo.GetProductByIdAsync(productId);
    public async Task UpdateProductAsync(RawMaterialSteelCordDTO product) => await _repo.UpdateProductAsync(product);
    public async Task<IPagedList<RawMaterialSteelCordDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
        => await (await _repo.SearchProductsByNameAsync(name, categoryId)).ToPagedListAsync(page, pageSize);
  }
}
