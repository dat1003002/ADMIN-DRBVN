using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public class LabEquipF2DailyCheckRepository : ILabEquipF2DailyCheckRepository
  {
    private readonly ApplicationDbContext _context;

    public LabEquipF2DailyCheckRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task AddProductAsync(LabEquipF2DailyCheckDTO dto)
    {
      var product = new Product
      {
        name = dto.name,
        image = dto.image,
        CategoryId = dto.CategoryId
      };
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetCategories() => await _context.Categories.ToListAsync();

    public Task<IQueryable<LabEquipF2DailyCheckDTO>> GetProducts(int categoryId)
    {
      var query = _context.Products
          .Where(p => p.CategoryId == categoryId)
          .Select(p => new LabEquipF2DailyCheckDTO
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

    public async Task<LabEquipF2DailyCheckDTO> GetProductByIdAsync(int productId)
    {
      return await _context.Products
          .Where(p => p.ProductId == productId)
          .Select(p => new LabEquipF2DailyCheckDTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId
          })
          .FirstOrDefaultAsync();
    }

    public async Task UpdateProductAsync(LabEquipF2DailyCheckDTO dto)
    {
      var product = await _context.Products.FindAsync(dto.ProductId);
      if (product != null)
      {
        product.name = dto.name;
        product.image = dto.image;
        product.CategoryId = dto.CategoryId;
        await _context.SaveChangesAsync();
      }
    }

    public Task<IQueryable<LabEquipF2DailyCheckDTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var query = _context.Products
          .Where(p => p.name.Contains(name) && p.CategoryId == categoryId)
          .Select(p => new LabEquipF2DailyCheckDTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId
          });
      return Task.FromResult(query);
    }
  }
}
