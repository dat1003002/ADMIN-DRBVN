using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Repository
{
  public class LuuHoaMHERepository : ILuuHoaMHERepository
  {
    private readonly ApplicationDbContext _luuhoaRepository;

    public LuuHoaMHERepository(ApplicationDbContext luuhoaMHERepository)
    {
      _luuhoaRepository = luuhoaMHERepository;
    }

    public async Task AddProductAsync(LuuHoaMHEDTO luuHoaMHEDTO)
    {
      var product = new Product
      {
        name = luuHoaMHEDTO.name,
        image = luuHoaMHEDTO.image,
        CategoryId = luuHoaMHEDTO.CategoryId,
      };

      await _luuhoaRepository.Products.AddAsync(product);
      await _luuhoaRepository.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int ProductId)
    {
      var product = await _luuhoaRepository.Products.FindAsync(ProductId);
      if (product != null)
      {
        _luuhoaRepository.Products.Remove(product);
        await _luuhoaRepository.SaveChangesAsync();
      }
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _luuhoaRepository.Categories.ToListAsync();
    }

    public async Task<LuuHoaMHEDTO> GetProductByIdAsync(int productId)
    {
      return await _luuhoaRepository.Products
          .Where(p => p.ProductId == productId)
          .Select(p => new LuuHoaMHEDTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId,
          })
          .FirstOrDefaultAsync();
    }

    public async Task<IQueryable<LuuHoaMHEDTO>> GetProducts(int categoryId)
    {
      var products = _luuhoaRepository.Products
          .Where(p => p.CategoryId == categoryId)
          .Select(p => new LuuHoaMHEDTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId,
          });

      return await Task.FromResult(products);
    }
    public Task<IQueryable<LuuHoaMHEDTO>> SearchProductsByNameAsync(string name, int categoryId)
    {
      var products = _luuhoaRepository.Products
          .Where(p => EF.Functions.Like(p.name, $"%{name}%") && p.CategoryId == categoryId)
          .Select(p => new LuuHoaMHEDTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId
          });

      return Task.FromResult(products);
    }
    public async Task<IEnumerable<LuuHoaMHEDTO>> SearchProductsByNameAsync(string name, int categoryId, int page, int pageSize)
    {
      var products = await _luuhoaRepository.Products
          .Where(p => EF.Functions.Like(p.name, $"%{name}%") && p.CategoryId == categoryId)
          .OrderBy(p => p.name)
          .Skip((page - 1) * pageSize)
          .Take(pageSize)
          .Select(p => new LuuHoaMHEDTO
          {
            ProductId = p.ProductId,
            name = p.name,
            image = p.image,
            CategoryId = p.CategoryId
          })
          .ToListAsync();

      return products;
    }

    public async Task UpdateProductAsync(LuuHoaMHEDTO product)
    {
      var editProduct = await _luuhoaRepository.Products.FindAsync(product.ProductId);
      if (editProduct != null)
      {
        editProduct.name = product.name;
        editProduct.image = product.image;
        editProduct.CategoryId = product.CategoryId;
        await _luuhoaRepository.SaveChangesAsync();
      }
    }
  }
}
