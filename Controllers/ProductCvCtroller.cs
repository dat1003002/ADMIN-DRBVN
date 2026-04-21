using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class ProductCvController : Controller
  {
    private readonly IProductCvService _productCvService;

    public ProductCvController(IProductCvService productCvService)
    {
      _productCvService = productCvService;
    }

    public async Task<IActionResult> ListTieuChuanCV(int page = 1, string searchName = null)
    {
      const int categoryId = 2;
      const int pageSize = 10;

      IPagedList<ProductDTO> products;

      if (!string.IsNullOrWhiteSpace(searchName))
      {
        products = await _productCvService.SearchProductsByNameAsync(searchName, categoryId, page, pageSize);
        ViewData["SearchTerm"] = searchName;
      }
      else
      {
        products = await _productCvService.GetProducts(categoryId, page, pageSize);
      }

      return View("~/Views/ProductMhe/ListTieuChuanCV.cshtml", products);
    }

    [HttpPost]
    public IActionResult Search(string name)
    {
      return RedirectToAction("ListTieuChuanCV", new { searchName = name });
    }

    public async Task<IActionResult> CreateProductCV()
    {
      var categories = await _productCvService.GetCategories();
      var filtercategori = categories.Where(c => c.CategoryId == 2).ToList();
      ViewBag.CategoryList = new SelectList(filtercategori, "CategoryId", "CategoryName");
      return View("~/Views/ProductMhe/CreateProductCV.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProductCV(ProductDTO productDTO)
    {
      if (ModelState.IsValid)
      {
        if (productDTO.imageFile != null && productDTO.imageFile.Length > 0)
        {
          var fileName = Path.GetFileName(productDTO.imageFile.FileName);
          var directoryPath = Path.Combine("wwwroot/images");

          if (!Directory.Exists(directoryPath))
          {
            Directory.CreateDirectory(directoryPath);
          }

          var filePath = Path.Combine(directoryPath, fileName);

          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            await productDTO.imageFile.CopyToAsync(stream);
          }

          productDTO.image = fileName;
        }

        await _productCvService.AddProductAsync(productDTO);
        return RedirectToAction("ListTieuChuanCV");
      }

      var categories = await _productCvService.GetCategories();
      ViewBag.CategoryList = new SelectList(categories, "CategoryId", "CategoryName", productDTO.CategoryId);
      return View("~/Views/ProductMhe/CreateProductCV.cshtml", productDTO);
    }

    public async Task<IActionResult> EditProductCV(int id)
    {
      var product = await _productCvService.GetProductByIdAsync(id);

      if (product == null)
      {
        return NotFound();
      }

      var categories = await _productCvService.GetCategories();
      var filterEditcategori = categories.Where(c => c.CategoryId == 2).ToList();
      ViewBag.CategoryList = new SelectList(filterEditcategori, "CategoryId", "CategoryName", product.CategoryId);

      return View("~/Views/ProductMhe/EditProductCV.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProductCV(ProductDTO productDTO)
    {
      if (ModelState.IsValid)
      {
        var existingProduct = await _productCvService.GetProductByIdAsync(productDTO.ProductId);

        if (existingProduct == null)
        {
          return NotFound();
        }

        if (productDTO.imageFile != null && productDTO.imageFile.Length > 0)
        {
          var oldFilePath = Path.Combine("wwwroot/images", existingProduct.image);

          if (System.IO.File.Exists(oldFilePath))
          {
            System.IO.File.Delete(oldFilePath);
          }

          var fileName = Path.GetFileName(productDTO.imageFile.FileName);
          var filePath = Path.Combine("wwwroot/images", fileName);

          using (var stream = new FileStream(filePath, FileMode.Create))
          {
            await productDTO.imageFile.CopyToAsync(stream);
          }

          productDTO.image = fileName;
        }
        else
        {
          productDTO.image = existingProduct.image;
        }

        await _productCvService.UpdateProductAsync(productDTO);
        return RedirectToAction("ListTieuChuanCV");
      }

      var categories = await _productCvService.GetCategories();
      ViewBag.CategoryList = new SelectList(categories, "CategoryId", "CategoryName", productDTO.CategoryId);

      return View("~/Views/ProductMhe/EditProductCV.cshtml", productDTO);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProductCV(int ProductId)
    {
      if (ProductId <= 0)
      {
        return BadRequest("Invalid Product ID.");
      }

      await _productCvService.DeleteProductAsync(ProductId);
      return Ok();
    }

    public async Task<IActionResult> ShowProductCvById(int id)
    {
      var product = await _productCvService.GetProductByIdAsync(id);

      if (product == null)
      {
        return NotFound();
      }

      return PartialView("~/Views/ProductMhe/ProductModalCv.cshtml", product);
    }
  }
}
