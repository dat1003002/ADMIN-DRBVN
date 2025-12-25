using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Repository
{
  public interface IBangTaiRepository
  {
    Task AddProductAsync(BangTaiDTO bangTaiDTO);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    IQueryable<BangTaiDTO> GetProducts(int categoryId);
    IQueryable<BangTaiDTO> SearchProductsByName(string name, int categoryId);
    Task<BangTaiDTO?> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(BangTaiDTO bangTaiDTO);
    Task DeleteProductAsync(int productId);
  }
}
