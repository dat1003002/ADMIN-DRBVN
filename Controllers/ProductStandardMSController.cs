using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class ProductStandardMSController : Controller
  {
    private readonly IProductStandardMSService _service;
    private const int CategoryId = 41;
    private const int PageSize = 9;

    public ProductStandardMSController(IProductStandardMSService service)
    {
      _service = service;
    }

    /// <summary>
    /// Hiển thị danh sách sản phẩm chuẩn MS với phân trang.
    /// </summary>
    public async Task<IActionResult> ListProductStandardMS(int page = 1)
    {
      var products = await _service.GetProducts(CategoryId, page, PageSize);
      ViewData["SearchTerm"] = null;
      return View("~/Views/ProductQC/Product Standard/ListProductStandardMS.cshtml", products);
    }

    /// <summary>
    /// Tìm kiếm sản phẩm chuẩn MS theo tên với phân trang.
    /// </summary>
    public async Task<IActionResult> SearchProductStandardMS(string name, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
        return RedirectToAction(nameof(ListProductStandardMS));

      var products = await _service.SearchProductsByNameAsync(name, CategoryId, page, PageSize);
      ViewData["SearchTerm"] = name;
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");
      return View("~/Views/ProductQC/Product Standard/ListProductStandardMS.cshtml", products);
    }

    /// <summary>
    /// Hiển thị form tạo mới sản phẩm chuẩn MS.
    /// </summary>
    public async Task<IActionResult> CreateProductStandardMS()
    {
      var categories = await _service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/Product Standard/CreateProductStandardMS.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    /// <summary>
    /// Xử lý POST tạo mới sản phẩm chuẩn MS.
    /// </summary>
    public async Task<IActionResult> CreateProductStandardMS(ProductStandardMSDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/Product Standard/CreateProductStandardMS.cshtml", product);
      }

      await HandleImageUpload(product);
      product.CategoryId = CategoryId;
      await _service.AddProductAsync(product);
      return RedirectToAction(nameof(ListProductStandardMS));
    }

    /// <summary>
    /// Hiển thị form chỉnh sửa sản phẩm chuẩn MS.
    /// </summary>
    public async Task<IActionResult> EditProductStandardMS(int id)
    {
      var product = await _service.GetProductByIdAsync(id);
      if (product == null) return NotFound();

      var categories = await _service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/Product Standard/EditProductStandardMS.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    /// <summary>
    /// Xử lý POST chỉnh sửa sản phẩm chuẩn MS.
    /// </summary>
    public async Task<IActionResult> EditProductStandardMS(ProductStandardMSDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/Product Standard/EditProductStandardMS.cshtml", product);
      }

      var existing = await _service.GetProductByIdAsync(product.ProductId);
      if (existing == null) return NotFound();

      if (product.imageFile != null && product.imageFile.Length > 0)
        await HandleImageUpload(product);
      else
        product.image = existing.image;

      product.CategoryId = CategoryId;
      await _service.UpdateProductAsync(product);
      return RedirectToAction(nameof(ListProductStandardMS));
    }

    [HttpPost]
    /// <summary>
    /// Xóa sản phẩm chuẩn MS.
    /// </summary>
    public async Task<IActionResult> DeleteProductStandardMS(int productId)
    {
      if (productId <= 0) return BadRequest("Invalid ID.");
      await _service.DeleteProductAsync(productId);
      return Json(new { success = true, message = "MS đã được xóa!" });
    }

    /// <summary>
    /// Hiển thị chi tiết sản phẩm chuẩn MS (partial view).
    /// </summary>
    public async Task<IActionResult> ShowProductStandardMS(int id)
    {
      var product = await _service.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("~/Views/ProductQC/Product Standard/ShowProductStandardMS.cshtml", product);
    }

    private async Task HandleImageUpload(ProductStandardMSDTO product)
    {
      if (product.imageFile == null || product.imageFile.Length == 0) return;

      var fileName = Path.GetFileName(product.imageFile.FileName);
      var dir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
      Directory.CreateDirectory(dir);
      var filePath = Path.Combine(dir, fileName);

      if (System.IO.File.Exists(filePath))
      {
        fileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        filePath = Path.Combine(dir, fileName);
      }

      product.image = fileName;
      using var stream = new FileStream(filePath, FileMode.Create);
      await product.imageFile.CopyToAsync(stream);
    }
  }
}
