using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class ProductCvGCCtroller : Controller
  {
    private readonly IProductCvGCService _productService;

    public ProductCvGCCtroller(IProductCvGCService productService)
    {
      _productService = productService;
    }

    public async Task<IActionResult> ListCvGCMHE(int page = 1, string searchName = null)
    {
      const int categoryId = 4;
      const int pageSize = 10;

      IPagedList<ProductGCMHEDTO> products;

      if (!string.IsNullOrEmpty(searchName))
      {
        products = await _productService.SearchProductsByNameAsync(searchName, categoryId, page, pageSize);
        ViewData["SearchTerm"] = searchName;
      }
      else
      {
        products = await _productService.GetProducts(categoryId, page, pageSize);
      }

      return View("~/Views/ProductMhe/ListCvGCMHE.cshtml", products);
    }

    public async Task<IActionResult> CreateProductGC()
    {
      var categories = await _productService.GetCategories();
      var filterCreatecategori = categories.Where(c => c.CategoryId == 4).ToList();

      ViewBag.CategoryList = new SelectList(filterCreatecategori, "CategoryId", "CategoryName");

      return View("~/Views/ProductMhe/CreateProductGC.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProductGC(ProductGCMHEDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _productService.GetCategories();
        var filterCreatecategori = categories.Where(c => c.CategoryId == 4).ToList();

        ViewBag.CategoryList = new SelectList(filterCreatecategori, "CategoryId", "CategoryName");

        return View("~/Views/ProductMhe/CreateProductGC.cshtml", product);
      }

      if (product.imageFile != null && product.imageFile.Length > 0)
      {
        var fileName = Path.GetFileName(product.imageFile.FileName);
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

        if (!Directory.Exists(directoryPath))
        {
          Directory.CreateDirectory(directoryPath);
        }

        var filePath = Path.Combine(directoryPath, fileName);

        if (System.IO.File.Exists(filePath))
        {
          string newFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + Guid.NewGuid() + Path.GetExtension(fileName);
          filePath = Path.Combine(directoryPath, newFileName);
          product.image = newFileName;
        }
        else
        {
          product.image = fileName;
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await product.imageFile.CopyToAsync(stream);
        }
      }

      await _productService.AddProductAsync(product);

      return RedirectToAction("ListCvGCMHE");
    }

    public async Task<IActionResult> EditProductGCMHE(int id)
    {
      var product = await _productService.GetProductByIdAsync(id);

      if (product == null)
      {
        return NotFound();
      }

      var categories = await _productService.GetCategories();
      var filterCreatecategori = categories.Where(c => c.CategoryId == 4).ToList();

      ViewBag.CategoryList = new SelectList(filterCreatecategori, "CategoryId", "CategoryName", product.CategoryId);

      return View("~/Views/ProductMhe/EditProductGCMHE.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProductGCMHE(ProductGCMHEDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _productService.GetCategories();
        var filterCreatecategori = categories.Where(c => c.CategoryId == 4).ToList();

        ViewBag.CategoryList = new SelectList(filterCreatecategori, "CategoryId", "CategoryName", product.CategoryId);

        return View("~/Views/ProductMhe/EditProductGCMHE.cshtml", product);
      }

      var existingProduct = await _productService.GetProductByIdAsync(product.ProductId);

      if (existingProduct == null)
      {
        return NotFound();
      }

      if (product.imageFile != null && product.imageFile.Length > 0)
      {
        if (!string.IsNullOrEmpty(existingProduct.image))
        {
          var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", existingProduct.image);

          if (System.IO.File.Exists(oldFilePath))
          {
            System.IO.File.Delete(oldFilePath);
          }
        }

        var fileName = Path.GetFileName(product.imageFile.FileName);
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

        if (!Directory.Exists(directoryPath))
        {
          Directory.CreateDirectory(directoryPath);
        }

        var filePath = Path.Combine(directoryPath, fileName);

        if (System.IO.File.Exists(filePath))
        {
          string newFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + Guid.NewGuid() + Path.GetExtension(fileName);
          filePath = Path.Combine(directoryPath, newFileName);
          product.image = newFileName;
        }
        else
        {
          product.image = fileName;
        }

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await product.imageFile.CopyToAsync(stream);
        }
      }
      else
      {
        product.image = existingProduct.image;
      }

      await _productService.UpdateProductAsync(product);

      return RedirectToAction("ListCvGCMHE");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProductCVGC(int ProductId)
    {
      if (ProductId <= 0)
      {
        return BadRequest("Invalid Product ID.");
      }

      await _productService.DeleteProductAsync(ProductId);

      return Ok();
    }

    public async Task<IActionResult> ShowProductGcById(int id)
    {
      var product = await _productService.GetProductByIdAsync(id);

      if (product == null)
      {
        return NotFound();
      }

      return PartialView("~/Views/ProductMhe/ProductGCMHEModal.cshtml", product);
    }
  }
}
