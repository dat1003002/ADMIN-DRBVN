using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public class QCEmployeeF2Repository : IQCEmployeeF2Repository
  {
    private readonly ApplicationDbContext _context;

    public QCEmployeeF2Repository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task AddProductAsync(QCEmployeeF2DTO productDTO)
    {
      var product = new Product
      {
        name = productDTO.name,
        image = productDTO.image,
        CategoryId = productDTO.CategoryId
      };
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _context.Categories.ToListAsync();
    }

    public Task<IQueryable<QCEmployeeF2DTO>> GetProducts(int categoryId)
    {
      var query = _context.Products
          .Where(p => p.CategoryId == categoryId)
          .Select(p => new QCEmployeeF2DTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId
          });
      return Task.FromResult(query);
    }

    public async Task DeleteProductAsync(int productId)
    {
      var product = await _context.Products.FindAsync(productId);
      if (product != null)
      {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
      }
    }

    public async Task<QCEmployeeF2DTO> GetProductByIdAsync(int productId)
    {
      return await _context.Products
          .Where(p => p.ProductId == productId)
          .Select(p => new QCEmployeeF2DTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId
          })
          .FirstOrDefaultAsync();
    }

    public async Task UpdateProductAsync(QCEmployeeF2DTO productDTO)
    {
      var product = await _context.Products.FindAsync(productDTO.ProductId);
      if (product != null)
      {
        product.name = productDTO.name;
        product.image = productDTO.image;
        product.CategoryId = productDTO.CategoryId;
        await _context.SaveChangesAsync();
      }
    }

    public Task<IQueryable<QCEmployeeF2DTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var products = _context.Products
          .Where(p => p.name.Contains(name) && p.CategoryId == categoryId)
          .Select(p => new QCEmployeeF2DTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId
          });
      return Task.FromResult(products);
    }

    public async Task<IEnumerable<QCEmployeeF2DTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
    {
      return await _context.Products
          .Where(p => p.name.Contains(name) && p.CategoryId == categoryId)
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .Select(p => new QCEmployeeF2DTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId
          })
          .ToListAsync();
    }
  }
}
