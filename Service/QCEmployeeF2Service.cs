using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Repository;
using X.PagedList;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public class QCEmployeeF2Service : IQCEmployeeF2Service
  {
    private readonly IQCEmployeeF2Repository _qcEmployeeF2Repository;

    public QCEmployeeF2Service(IQCEmployeeF2Repository qcEmployeeF2Repository)
    {
      _qcEmployeeF2Repository = qcEmployeeF2Repository;
    }

    public async Task AddProductAsync(QCEmployeeF2DTO product)
    {
      await _qcEmployeeF2Repository.AddProductAsync(product);
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _qcEmployeeF2Repository.GetCategories();
    }

    public async Task<IPagedList<QCEmployeeF2DTO>> GetProducts(int categoryId, int pageNumber, int pageSize)
    {
      var products = await _qcEmployeeF2Repository.GetProducts(categoryId);
      return await products.ToPagedListAsync(pageNumber, pageSize);
    }

    public async Task DeleteProductAsync(int productId)
    {
      await _qcEmployeeF2Repository.DeleteProductAsync(productId);
    }

    public async Task<QCEmployeeF2DTO> GetProductByIdAsync(int productId)
    {
      return await _qcEmployeeF2Repository.GetProductByIdAsync(productId);
    }

    public async Task UpdateProductAsync(QCEmployeeF2DTO product)
    {
      await _qcEmployeeF2Repository.UpdateProductAsync(product);
    }

    public async Task<IEnumerable<QCEmployeeF2DTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var products = await _qcEmployeeF2Repository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToListAsync();
    }

    public async Task<IPagedList<QCEmployeeF2DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
    {
      var products = await _qcEmployeeF2Repository.SearchProductsByNameAsync(name, categoryId);
      return await products.ToPagedListAsync(page, pageSize);
    }
  }
}
