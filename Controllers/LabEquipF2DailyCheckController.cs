using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class LabEquipF2DailyCheckController : Controller
  {
    private readonly ILabEquipF2DailyCheckService _service;
    private const int CategoryId = 30;
    private const int PageSize = 9;

    public LabEquipF2DailyCheckController(ILabEquipF2DailyCheckService service)
    {
      _service = service;
    }

    public async Task<IActionResult> ListDailyCheckF2(int page = 1)
    {
      var products = await _service.GetProducts(CategoryId, page, PageSize);
      ViewData["SearchTerm"] = null;
      return View("~/Views/ProductQC/LabEquimentF2/ListDailyCheckF2.cshtml", products);
    }

    public async Task<IActionResult> Search(string name, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
        return RedirectToAction(nameof(ListDailyCheckF2));

      var products = await _service.SearchProductsByNameAsync(name, CategoryId, page, PageSize);
      ViewData["SearchTerm"] = name;
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");
      return View("~/Views/ProductQC/LabEquimentF2/ListDailyCheckF2.cshtml", products);
    }

    public async Task<IActionResult> CreateDailyCheckF2()
    {
      var categories = await _service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/LabEquimentF2/CreateDailyCheckF2.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateDailyCheckF2(LabEquipF2DailyCheckDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/LabEquimentF2/CreateDailyCheckF2.cshtml", product);
      }

      await HandleImageUpload(product);
      product.CategoryId = CategoryId;
      await _service.AddProductAsync(product);
      return RedirectToAction(nameof(ListDailyCheckF2));
    }

    public async Task<IActionResult> EditDailyCheckF2(int id)
    {
      var product = await _service.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      var categories = await _service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/LabEquimentF2/EditDailyCheckF2.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditDailyCheckF2(LabEquipF2DailyCheckDTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/LabEquimentF2/EditDailyCheckF2.cshtml", product);
      }

      var existing = await _service.GetProductByIdAsync(product.ProductId);
      if (existing == null) return NotFound();

      if (product.imageFile != null && product.imageFile.Length > 0)
        await HandleImageUpload(product);
      else
        product.image = existing.image;

      product.CategoryId = CategoryId;
      await _service.UpdateProductAsync(product);
      return RedirectToAction(nameof(ListDailyCheckF2));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int productId)
    {
      if (productId <= 0) return BadRequest("Invalid ID.");
      await _service.DeleteProductAsync(productId);
      return Json(new { success = true, message = "Tiêu chuẩn kiểm tra đã được xóa!" });
    }

    public async Task<IActionResult> ShowDailyCheckF2(int id)
    {
      var product = await _service.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("~/Views/ProductQC/LabEquimentF2/ShowDailyCheckF2.cshtml", product);
    }

    private async Task HandleImageUpload(LabEquipF2DailyCheckDTO product)
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
