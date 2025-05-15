using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public class QuyCachCSCTLRepository : IQuyCachCSCTLRepository
  {
    private readonly ApplicationDbContext _context;

    public QuyCachCSCTLRepository(ApplicationDbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CreateProductAsync(QuyCachCaoSuCTLDTO product)
    {
      if (product == null)
        throw new ArgumentNullException(nameof(product));

      var productEntity = new Product
      {
        mahang = product.mahang,
        name = product.name,

        CreatedAt = DateTime.Now,
        CategoryId = product.CategoryId
      };

      await _context.Products.AddAsync(productEntity);
      await _context.SaveChangesAsync();
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

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _context.Categories.ToListAsync();
    }

    public async Task<QuyCachCaoSuCTLDTO> GetProductByIdAsync(int productId)
    {
      var product = await _context.Products.FindAsync(productId);
      if (product == null)
        return null;

      return new QuyCachCaoSuCTLDTO
      {
        ProductId = product.ProductId,
        mahang = product.mahang,
        name = product.name,
        
        CreatedAt = product.CreatedAt,
        UpdatedAt = product.UpdatedAt
      };
    }

    public async Task<IQueryable<QuyCachCaoSuCTLDTO>> GetProducts(int categoryId)
    {
      return await Task.FromResult(_context.Products
        .Where(p => p.CategoryId == categoryId)
        .Select(p => new QuyCachCaoSuCTLDTO
        {
          ProductId = p.ProductId,
          mahang = p.mahang,
          name = p.name,
         
          CreatedAt = p.CreatedAt,
          UpdatedAt = p.UpdatedAt
        }));
    }

    public async Task UpdateProductAsync(QuyCachCaoSuCTLDTO quyCachCaoSuCTLDTO)
    {
      if (quyCachCaoSuCTLDTO == null)
        throw new ArgumentNullException(nameof(quyCachCaoSuCTLDTO));

      var product = await _context.Products.FindAsync(quyCachCaoSuCTLDTO.ProductId);
      if (product == null)
        throw new InvalidOperationException("Product not found.");

      product.mahang = quyCachCaoSuCTLDTO.mahang;
      product.name = quyCachCaoSuCTLDTO.name;
     
      product.UpdatedAt = DateTime.Now;

      _context.Products.Update(product);
      await _context.SaveChangesAsync();
    }
  }
}
