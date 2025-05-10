using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using Microsoft.EntityFrameworkCore;

namespace AspnetCoreMvcFull.Repository
{
  public class ProductLTCTLRepository : IProductLTCTLRepository
  {
    private readonly ApplicationDbContext _productLTCTLRepository;

    public ProductLTCTLRepository(ApplicationDbContext productLTCTLRepository)
    {
      _productLTCTLRepository = productLTCTLRepository;
    }

    public async Task CreateProductAsync(LoiThepCTLDTO product)
    {
      var ProductLTCTL = new Product()
      {
        mahang = product.mahang,
        name = product.name,
        aplucdaudunloithep = product.aplucdaudunloithep,
        caosubo = product.caosubo,
        caosuketdinh = product.caosuketdinh,
        caosur514 = product.caosur514,
        chieudailoithep = product.chieudailoithep,
        khoangcach2daumoinoibo = product.khoangcach2daumoinoibo,
        khoangcach2daumoinoiloithep = product.khoangcach2daumoinoiloithep,
        khocaosubo = product.khocaosubo,
        khocaosuketdinh3t = product.khocaosuketdinh3t,
        kholoithep = product.kholoithep,
        kichthuoccuacaosudanmoinoi = product.kichthuoccuacaosudanmoinoi,
        loithepsaukhidun = product.loithepsaukhidun,
        loitheptruockhidun = product.loitheptruockhidun,
        nhietdodaumaydun = product.nhietdodaumaydun,
        nhietdotrucxoan = product.nhietdotrucxoan,
        solink = product.solink,
        sosoiloithep = product.sosoiloithep,
        tocdocolingdrum = product.tocdocolingdrum,
        tocdoduncaosu = product.tocdoduncaosu,
        tocdoquan = product.tocdoquan,
        trongluongloithepspinning = product.trongluongloithepspinning,
        dodaycaosubo = product.dodaycaosubo,
        dodaycaosuketdinh3t = product.dodaycaosuketdinh3t,
        CategoryId = product.CategoryId,
        CreatedAt = DateTime.Now
      };
      await _productLTCTLRepository.Products.AddAsync(ProductLTCTL);
      await _productLTCTLRepository.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int ProductId)
    {
      var product = await _productLTCTLRepository.Products.FindAsync(ProductId);
      if (product != null)
      {
        _productLTCTLRepository.Products.Remove(product);
        await _productLTCTLRepository.SaveChangesAsync();
      }
    }

    public async Task<IEnumerable<Category>> GetCategories()
    {
      return await _productLTCTLRepository.Categories.ToListAsync();
    }

    public async Task<LoiThepCTLDTO> GetProductByIdAsync(int productId)
    {
      var productLTCTL = await _productLTCTLRepository.Products.FindAsync(productId);
      if (productLTCTL == null) return null;
      return new LoiThepCTLDTO
      {
        ProductId = productLTCTL.ProductId,
        mahang = productLTCTL.mahang,
        name = productLTCTL.name,
        aplucdaudunloithep = productLTCTL.aplucdaudunloithep,
        caosubo = productLTCTL.caosubo,
        caosuketdinh = productLTCTL.caosuketdinh,
        caosur514 = productLTCTL.caosur514,
        chieudailoithep = productLTCTL.chieudailoithep,
        khoangcach2daumoinoibo = productLTCTL.khoangcach2daumoinoibo,
        khoangcach2daumoinoiloithep = productLTCTL.khoangcach2daumoinoiloithep,
        khocaosubo = productLTCTL.khocaosubo,
        khocaosuketdinh3t = productLTCTL.khocaosuketdinh3t,
        kholoithep = productLTCTL.kholoithep,
        kichthuoccuacaosudanmoinoi = productLTCTL.kichthuoccuacaosudanmoinoi,
        loithepsaukhidun = productLTCTL.loithepsaukhidun,
        loitheptruockhidun = productLTCTL.loitheptruockhidun,
        nhietdodaumaydun = productLTCTL.nhietdodaumaydun,
        nhietdotrucxoan = productLTCTL.nhietdotrucxoan,
        solink = productLTCTL.solink,
        sosoiloithep = productLTCTL.sosoiloithep,
        tocdocolingdrum = productLTCTL.tocdocolingdrum,
        tocdoduncaosu = productLTCTL.tocdoduncaosu,
        tocdoquan = productLTCTL.tocdoquan,
        trongluongloithepspinning = productLTCTL.trongluongloithepspinning,
        dodaycaosubo = productLTCTL.dodaycaosubo,
        dodaycaosuketdinh3t = productLTCTL.dodaycaosuketdinh3t,
        CategoryId = productLTCTL.CategoryId
      };
    }

    public async Task<IQueryable<LoiThepCTLDTO>> GetProducts(int categoryId)
    {
      var productLTCTL = _productLTCTLRepository.Products
         .Where(p => p.CategoryId == categoryId)
         .Select(p => new LoiThepCTLDTO
         {
           ProductId = p.ProductId,
           mahang = p.mahang,
           name = p.name,
           aplucdaudunloithep = p.aplucdaudunloithep,
           caosubo = p.caosubo,
           caosuketdinh = p.caosuketdinh,
           caosur514 = p.caosur514,
           chieudailoithep = p.chieudailoithep,
           khoangcach2daumoinoibo = p.khoangcach2daumoinoibo,
           khoangcach2daumoinoiloithep = p.khoangcach2daumoinoiloithep,
           khocaosubo = p.khocaosubo,
           khocaosuketdinh3t = p.khocaosuketdinh3t,
           kholoithep = p.kholoithep,
           kichthuoccuacaosudanmoinoi = p.kichthuoccuacaosudanmoinoi,
           loithepsaukhidun = p.loithepsaukhidun,
           loitheptruockhidun = p.loitheptruockhidun,
           nhietdodaumaydun = p.nhietdodaumaydun,
           nhietdotrucxoan = p.nhietdotrucxoan,
           solink = p.solink,
           sosoiloithep = p.sosoiloithep,
           tocdocolingdrum = p.tocdocolingdrum,
           tocdoduncaosu = p.tocdoduncaosu,
           tocdoquan = p.tocdoquan,
           trongluongloithepspinning = p.trongluongloithepspinning,
           dodaycaosubo = p.dodaycaosubo,
           dodaycaosuketdinh3t = p.dodaycaosuketdinh3t,
           CategoryId = p.CategoryId
         });
      return await Task.FromResult(productLTCTL);
    }

    public async Task UpdateProductAsync(LoiThepCTLDTO loiThepCTLDTO)
    {
      var product = await _productLTCTLRepository.Products.FindAsync(loiThepCTLDTO.ProductId);
      if (product != null)
      {
        product.mahang = loiThepCTLDTO.mahang;
        product.name = loiThepCTLDTO.name;
        product.aplucdaudunloithep = loiThepCTLDTO.aplucdaudunloithep;
        product.caosubo = loiThepCTLDTO.caosubo;
        product.caosuketdinh = loiThepCTLDTO.caosuketdinh;
        product.caosur514 = loiThepCTLDTO.caosur514;
        product.chieudailoithep = loiThepCTLDTO.chieudailoithep;
        product.khoangcach2daumoinoibo = loiThepCTLDTO.khoangcach2daumoinoibo;
        product.khoangcach2daumoinoiloithep = loiThepCTLDTO.khoangcach2daumoinoiloithep;
        product.khocaosubo = loiThepCTLDTO.khocaosubo;
        product.khocaosuketdinh3t = loiThepCTLDTO.khocaosuketdinh3t;
        product.kholoithep = loiThepCTLDTO.kholoithep;
        product.kichthuoccuacaosudanmoinoi = loiThepCTLDTO.kichthuoccuacaosudanmoinoi;
        product.loithepsaukhidun = loiThepCTLDTO.loithepsaukhidun;
        product.loitheptruockhidun = loiThepCTLDTO.loitheptruockhidun;
        product.nhietdodaumaydun = loiThepCTLDTO.nhietdodaumaydun;
        product.nhietdotrucxoan = loiThepCTLDTO.nhietdotrucxoan;
        product.solink = loiThepCTLDTO.solink;
        product.sosoiloithep = loiThepCTLDTO.sosoiloithep;
        product.tocdocolingdrum = loiThepCTLDTO.tocdocolingdrum;
        product.tocdoduncaosu = loiThepCTLDTO.tocdoduncaosu;
        product.tocdoquan = loiThepCTLDTO.tocdoquan;
        product.trongluongloithepspinning = loiThepCTLDTO.trongluongloithepspinning;
        product.dodaycaosubo = loiThepCTLDTO.dodaycaosubo;
        product.dodaycaosuketdinh3t = loiThepCTLDTO.dodaycaosuketdinh3t;
        product.CategoryId = loiThepCTLDTO.CategoryId;
        await _productLTCTLRepository.SaveChangesAsync();
      }
    }
  }
}
