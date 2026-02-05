using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public class RawMaterialChemicalRepository : IRawMaterialChemicalRepository
  {
    private readonly ApplicationDbContext _context;

    public RawMaterialChemicalRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task AddProductAsync(RawMaterialChemicalDTO dto)
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

    public Task<IQueryable<RawMaterialChemicalDTO>> GetProducts(int categoryId)
    {
      var query = _context.Products
          .Where(p => p.CategoryId == categoryId)
          .Select(p => new RawMaterialChemicalDTO
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

    public async Task<RawMaterialChemicalDTO> GetProductByIdAsync(int productId)
    {
      return await _context.Products
          .Where(p => p.ProductId == productId)
          .Select(p => new RawMaterialChemicalDTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId
          })
          .FirstOrDefaultAsync();
    }

    public async Task UpdateProductAsync(RawMaterialChemicalDTO dto)
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

    public Task<IQueryable<RawMaterialChemicalDTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var query = _context.Products
          .Where(p => p.name.Contains(name) && p.CategoryId == categoryId)
          .Select(p => new RawMaterialChemicalDTO
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
