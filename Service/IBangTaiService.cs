using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetCoreMvcFull.Service
{
  public interface IBangTaiService
  {
    Task AddProductAsync(BangTaiDTO bangTaiDTO);
    Task<IEnumerable<Category>> GetCategoriesAsync();
    Task<IQueryable<BangTaiDTO>> GetProductsAsync(int categoryId);
    Task<IQueryable<BangTaiDTO>> SearchProductsByNameAsync(string name, int categoryId);
    Task<BangTaiDTO?> GetProductByIdAsync(int productId);
    Task UpdateProductAsync(BangTaiDTO bangTaiDTO);
    Task DeleteProductAsync(int productId);
  }
}
