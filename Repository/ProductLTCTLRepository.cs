using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Repository
{
  public class ProductLTCTLRepository : IProductLTCTLRepository
  {
    private readonly ApplicationDbContext _context;

    public ProductLTCTLRepository(ApplicationDbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task CreateProductAsync(LoiThepCTLDTO product)
    {
      if (product == null)
        throw new ArgumentNullException(nameof(product));

      var productEntity = new Product
      {
        mahang = product.mahang,
        name = product.name,
        chieudailoithep = product.chieudailoithep,
        khoangcach2daumoinoiloithep = product.khoangcach2daumoinoiloithep,
        khocaosubo = product.khocaosubo,
        khocaosuketdinh3t = product.khocaosuketdinh3t,
        kholoithep = product.kholoithep,
        kichthuoccuacaosudanmoinoi = product.kichthuoccuacaosudanmoinoi,
        solink = product.solink,
        sosoiloithep = product.sosoiloithep,
        tocdoquan = product.tocdoquan,
        trongluongloithepspinning = product.trongluongloithepspinning,
        dodaycaosubo = product.dodaycaosubo,
        dodaycaosuketdinh3t = product.dodaycaosuketdinh3t,
        CategoryId = product.CategoryId,
        CreatedAt = DateTime.Now
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

    public async Task<LoiThepCTLDTO> GetProductByIdAsync(int productId)
    {
      var product = await _context.Products.FindAsync(productId);
      if (product == null)
        return null;

      return new LoiThepCTLDTO
      {
        ProductId = product.ProductId,
        mahang = product.mahang,
        name = product.name,
        chieudailoithep = product.chieudailoithep,
        khoangcach2daumoinoiloithep = product.khoangcach2daumoinoiloithep,
        khocaosubo = product.khocaosubo,
        khocaosuketdinh3t = product.khocaosuketdinh3t,
        kholoithep = product.kholoithep,
        kichthuoccuacaosudanmoinoi = product.kichthuoccuacaosudanmoinoi,
        solink = product.solink,
        sosoiloithep = product.sosoiloithep,
        tocdoquan = product.tocdoquan,
        trongluongloithepspinning = product.trongluongloithepspinning,
        dodaycaosubo = product.dodaycaosubo,
        dodaycaosuketdinh3t = product.dodaycaosuketdinh3t,
        CategoryId = product.CategoryId,
        CreatedAt = product.CreatedAt,
        UpdatedAt = product.UpdatedAt
      };
    }

    public async Task<IQueryable<LoiThepCTLDTO>> GetProducts(int categoryId)
    {
      return await Task.FromResult(_context.Products
        .Where(p => p.CategoryId == categoryId)
        .Select(p => new LoiThepCTLDTO
        {
          ProductId = p.ProductId,
          mahang = p.mahang,
          name = p.name,
          chieudailoithep = p.chieudailoithep,
          khoangcach2daumoinoiloithep = p.khoangcach2daumoinoiloithep,
          khocaosubo = p.khocaosubo,
          khocaosuketdinh3t = p.khocaosuketdinh3t,
          kholoithep = p.kholoithep,
          kichthuoccuacaosudanmoinoi = p.kichthuoccuacaosudanmoinoi,
          solink = p.solink,
          sosoiloithep = p.sosoiloithep,
          tocdoquan = p.tocdoquan,
          trongluongloithepspinning = p.trongluongloithepspinning,
          dodaycaosubo = p.dodaycaosubo,
          dodaycaosuketdinh3t = p.dodaycaosuketdinh3t,
          CategoryId = p.CategoryId,
          CreatedAt = p.CreatedAt,
          UpdatedAt = p.UpdatedAt
        }));
    }

    public async Task UpdateProductAsync(LoiThepCTLDTO loiThepCTLDTO)
    {
      if (loiThepCTLDTO == null)
        throw new ArgumentNullException(nameof(loiThepCTLDTO));

      var product = await _context.Products.FindAsync(loiThepCTLDTO.ProductId);
      if (product == null)
        throw new InvalidOperationException("Product not found.");

      product.mahang = loiThepCTLDTO.mahang;
      product.name = loiThepCTLDTO.name;
      product.chieudailoithep = loiThepCTLDTO.chieudailoithep;
      product.khoangcach2daumoinoiloithep = loiThepCTLDTO.khoangcach2daumoinoiloithep;
      product.khocaosubo = loiThepCTLDTO.khocaosubo;
      product.khocaosuketdinh3t = loiThepCTLDTO.khocaosuketdinh3t;
      product.kholoithep = loiThepCTLDTO.kholoithep;
      product.kichthuoccuacaosudanmoinoi = loiThepCTLDTO.kichthuoccuacaosudanmoinoi;
      product.solink = loiThepCTLDTO.solink;
      product.sosoiloithep = loiThepCTLDTO.sosoiloithep;
      product.tocdoquan = loiThepCTLDTO.tocdoquan;
      product.trongluongloithepspinning = loiThepCTLDTO.trongluongloithepspinning;
      product.dodaycaosubo = loiThepCTLDTO.dodaycaosubo;
      product.dodaycaosuketdinh3t = loiThepCTLDTO.dodaycaosuketdinh3t;
      product.CategoryId = loiThepCTLDTO.CategoryId;
      product.UpdatedAt = DateTime.Now;

      _context.Products.Update(product);
      await _context.SaveChangesAsync();
    }
  }
}
