using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public class TTBangTaiRepository : ITTBangTaiRepository
  {
    private readonly ApplicationDbContext _context;
    public TTBangTaiRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task AddProductAsync(TTBangTaiDTO tTBangTaiDTO)
    {
      var product = new Product
      {
        name = tTBangTaiDTO.Name,
        CategoryId = tTBangTaiDTO.CategoryId,
        ProductImages = new List<ProductImage>()
      };
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
      return await _context.Categories.ToListAsync();
    }

    public async Task<IQueryable<TTBangTaiDTO>> GetProductsAsync(int categoryId)
    {
      var query = _context.Products
          .Where(p => p.CategoryId == categoryId)
          .Select(p => new TTBangTaiDTO
          {
            ProductId = p.ProductId,
            Name = p.name,
            CategoryId = p.CategoryId,
            ExistingImagePaths = p.ProductImages
                    .OrderBy(pi => pi.SortOrder)
                    .Select(pi => pi.ImagePath)
                    .ToList()
          });
      return await Task.FromResult(query);
    }

    public async Task<IQueryable<TTBangTaiDTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var query = _context.Products
          .Where(p => p.CategoryId == categoryId && p.name.Contains(name))
          .Select(p => new TTBangTaiDTO
          {
            ProductId = p.ProductId,
            Name = p.name,
            CategoryId = p.CategoryId,
            ExistingImagePaths = p.ProductImages
                    .OrderBy(pi => pi.SortOrder)
                    .Select(pi => pi.ImagePath)
                    .ToList()
          });
      return await Task.FromResult(query);
    }

    public async Task<TTBangTaiDTO?> GetProductByIdAsync(int productId)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == productId);
      if (product == null) return null;
      return new TTBangTaiDTO
      {
        ProductId = product.ProductId,
        Name = product.name,
        CategoryId = product.CategoryId,
        ExistingImagePaths = product.ProductImages
                .OrderBy(pi => pi.SortOrder)
                .Select(pi => pi.ImagePath)
                .ToList()
      };
    }

    public async Task UpdateProductAsync(TTBangTaiDTO tTBangTaiDTO)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == tTBangTaiDTO.ProductId);
      if (product == null) return;
      product.name = tTBangTaiDTO.Name;
      product.CategoryId = tTBangTaiDTO.CategoryId;
      if (tTBangTaiDTO.DeletedImageIds.Any())
      {
        var imagesToDeleteById = product.ProductImages
            .Where(pi => tTBangTaiDTO.DeletedImageIds.Contains(pi.Id))
            .ToList();
        _context.ProductImages.RemoveRange(imagesToDeleteById);
      }
      if (tTBangTaiDTO.DeletedImagePaths.Any())
      {
        var imagesToDeleteByPath = product.ProductImages
            .Where(pi => tTBangTaiDTO.DeletedImagePaths.Contains(pi.ImagePath))
            .ToList();
        _context.ProductImages.RemoveRange(imagesToDeleteByPath);
      }
      await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int productId)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == productId);
      if (product != null)
      {
        _context.ProductImages.RemoveRange(product.ProductImages);
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
      }
    }
  }
}
