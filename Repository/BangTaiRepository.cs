using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public class BangTaiRepository : IBangTaiRepository
  {
    private readonly ApplicationDbContext _context;

    public BangTaiRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task AddProductAsync(BangTaiDTO bangTaiDTO)
    {
      var product = new Product
      {
        name = bangTaiDTO.Name,
        CategoryId = bangTaiDTO.CategoryId,
        ProductImages = new List<ProductImage>()
      };
      _context.Products.Add(product);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
      return await _context.Categories.ToListAsync();
    }

    public IQueryable<BangTaiDTO> GetProducts(int categoryId)
    {
      return _context.Products
          .Where(p => p.CategoryId == categoryId)
          .Select(p => new BangTaiDTO
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

    public IQueryable<BangTaiDTO> SearchProductsByName(string name, int categoryId)
    {
      return _context.Products
          .Where(p => p.CategoryId == categoryId && p.name.Contains(name))
          .Select(p => new BangTaiDTO
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

    public async Task<BangTaiDTO?> GetProductByIdAsync(int productId)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == productId);

      if (product == null) return null;

      return new BangTaiDTO
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

    public async Task UpdateProductAsync(BangTaiDTO bangTaiDTO)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == bangTaiDTO.ProductId);

      if (product == null) return;

      product.name = bangTaiDTO.Name;
      product.CategoryId = bangTaiDTO.CategoryId;

      if (bangTaiDTO.DeletedImageIds.Any())
      {
        var imagesToDelete = product.ProductImages
            .Where(pi => bangTaiDTO.DeletedImageIds.Contains(pi.Id))
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
