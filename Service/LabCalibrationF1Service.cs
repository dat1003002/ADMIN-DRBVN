using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class LabCalibrationF1Service : ILabCalibrationF1Service
  {
    private readonly ILabCalibrationF1Repository _repo;

    public LabCalibrationF1Service(ILabCalibrationF1Repository repo)
    {
      _repo = repo;
    }

    public async Task AddProductAsync(LabCalibrationF1DTO product) => await _repo.AddProductAsync(product);
    public async Task<IEnumerable<Category>> GetCategories() => await _repo.GetCategories();
    public async Task<IPagedList<LabCalibrationF1DTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
        => await (await _repo.GetProducts(categoryId)).ToPagedListAsync(pageNumber, pageSize);
    public async Task DeleteProductAsync(int productId) => await _repo.DeleteProductAsync(productId);
    public async Task<LabCalibrationF1DTO> GetProductByIdAsync(int productId) => await _repo.GetProductByIdAsync(productId);
    public async Task UpdateProductAsync(LabCalibrationF1DTO product) => await _repo.UpdateProductAsync(product);
    public async Task<IPagedList<LabCalibrationF1DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
        => await (await _repo.SearchProductsByNameAsync(name, categoryId)).ToPagedListAsync(page, pageSize);
  }
}
