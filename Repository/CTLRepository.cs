using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public class CTLRepository : ICTLRepository
  {
    private readonly ApplicationDbContext _context;

    public CTLRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task AddProductAsync(CTLDTO ctlDTO)
    {
      var product = new Product
      {
        name = ctlDTO.Name,
        CategoryId = ctlDTO.CategoryId,
        ProductImages = new List<ProductImage>()
      };

      _context.Products.Add(product);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
      return await _context.Categories.ToListAsync();
    }

    public IQueryable<CTLDTO> GetProducts(int categoryId)
    {
      return _context.Products
          .Where(p => p.CategoryId == categoryId)
          .Select(p => new CTLDTO
          {
            ProductId = p.ProductId,
            Name = p.name,
            CategoryId = p.CategoryId,
            ExistingImagePaths = p.ProductImages
                  .OrderBy(pi => pi.SortOrder)
                  .Select(pi => pi.ImagePath)
                  .ToList()
          });
    }

    public IQueryable<CTLDTO> SearchProductsByName(string name, int categoryId)
    {
      return _context.Products
          .Where(p => p.CategoryId == categoryId && p.name.Contains(name))
          .Select(p => new CTLDTO
          {
            ProductId = p.ProductId,
            Name = p.name,
            CategoryId = p.CategoryId,
            ExistingImagePaths = p.ProductImages
                  .OrderBy(pi => pi.SortOrder)
                  .Select(pi => pi.ImagePath)
                  .ToList()
          });
    }

    public async Task<CTLDTO?> GetProductByIdAsync(int productId)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == productId);

      if (product == null) return null;

      return new CTLDTO
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

    public async Task UpdateProductAsync(CTLDTO ctlDTO)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == ctlDTO.ProductId);

      if (product == null) return;

      product.name = ctlDTO.Name;
      product.CategoryId = ctlDTO.CategoryId;

      // Xóa ảnh được đánh dấu xóa
      if (ctlDTO.DeletedImagePaths != null && ctlDTO.DeletedImagePaths.Any())
      {
        var imagesToDelete = product.ProductImages
            .Where(pi => ctlDTO.DeletedImagePaths.Contains(pi.ImagePath))
            .ToList();

        _context.ProductImages.RemoveRange(imagesToDelete);
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
