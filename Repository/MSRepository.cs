using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public class MSRepository : IMSRepository
  {
    private readonly ApplicationDbContext _context;

    public MSRepository(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task AddProductAsync(MSDTO mSDTO)
    {
      var product = new Product
      {
        name = mSDTO.Name,
        CategoryId = mSDTO.CategoryId,
        ProductImages = new List<ProductImage>()
      };

      _context.Products.Add(product);
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Category>> GetCategoriesAsync()
    {
      return await _context.Categories.ToListAsync();
    }

    public IQueryable<MSDTO> GetProducts(int categoryId)
    {
      return _context.Products
          .Where(p => p.CategoryId == categoryId)
          .Select(p => new MSDTO
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

    public IQueryable<MSDTO> SearchProductsByName(string name, int categoryId)
    {
      return _context.Products
          .Where(p => p.CategoryId == categoryId && p.name.Contains(name))
          .Select(p => new MSDTO
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

    public async Task<MSDTO?> GetProductByIdAsync(int productId)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == productId);

      if (product == null) return null;

      return new MSDTO
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

    public async Task UpdateProductAsync(MSDTO mSDTO)
    {
      var product = await _context.Products
          .Include(p => p.ProductImages)
          .FirstOrDefaultAsync(p => p.ProductId == mSDTO.ProductId);

      if (product == null) return;

      product.name = mSDTO.Name;
      product.CategoryId = mSDTO.CategoryId;

      if (mSDTO.DeletedImageIds.Any())
      {
        var imagesToDelete = product.ProductImages
            .Where(pi => mSDTO.DeletedImageIds.Contains(pi.Id))
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
