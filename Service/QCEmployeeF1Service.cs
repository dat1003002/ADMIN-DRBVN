using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class QCEmployeeF1Service : IQCEmployeeF1Service
  {
    private readonly IQCEmployeeF1Repository _qcEmployeeF1Repository;

    public QCEmployeeF1Service(IQCEmployeeF1Repository qcEmployeeF1Repository)
    {
      _qcEmployeeF1Repository = qcEmployeeF1Repository;
    }

    public async Task AddProductAsync(QCEmployeeF1DTO product)
    {
      await _qcEmployeeF1Repository.AddProductAsync(product);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _qcEmployeeF1Repository.GetCategories();
    }

    public async Task<IPagedList<QCEmployeeF1DTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
    {
      var products = await _qcEmployeeF1Repository.GetProducts(categoryId);
      return await products.ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task DeleteProductAsync(int productId)
    {
      await _qcEmployeeF1Repository.DeleteProductAsync(productId);
    }

    public async Task<QCEmployeeF1DTO> GetProductByIdAsync(int productId)
    {
      return await _qcEmployeeF1Repository.GetProductByIdAsync(productId);
    }

    public async Task UpdateProductAsync(QCEmployeeF1DTO product)
    {
      await _qcEmployeeF1Repository.UpdateProductAsync(product);
    }

    public async Task<IEnumerable<QCEmployeeF1DTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var products = await _qcEmployeeF1Repository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToListAsync();
    }

    public async Task<IPagedList<QCEmployeeF1DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
    {
      var products = await _qcEmployeeF1Repository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToPagedListAsync(page, pageSize);
    }
  }
}
