using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class LaboratoryEquipmentF1Controller : Controller
  {
    private readonly ILaboratoryEquipmentF1Service _service;
    private const int CategoryId = 31;
    private const int PageSize = 9;

    public LaboratoryEquipmentF1Controller(ILaboratoryEquipmentF1Service service)
    {
      _service = service;
    }

    // LIST
    public async Task<IActionResult> ListLaboratoryEquipmentF1(int page = 1)
    {
      var products = await _service.GetProducts(CategoryId, page, PageSize);
      ViewData["SearchTerm"] = null;
      return View("~/Views/ProductQC/LabEquimentF1/ListLaboratoryEquipmentF1.cshtml", products);
    }

    // SEARCH
    public async Task<IActionResult> Search(string name, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
        return RedirectToAction(nameof(ListLaboratoryEquipmentF1));

      var products = await _service.SearchProductsByNameAsync(name, CategoryId, page, PageSize);
      ViewData["SearchTerm"] = name;
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");
      return View("~/Views/ProductQC/LabEquimentF1/ListLaboratoryEquipmentF1.cshtml", products);
    }

    // CREATE - GET
    public async Task<IActionResult> CreateLaboratoryEquipmentF1()
    {
      var categories = await _service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/LabEquimentF1/CreateLaboratoryEquipmentF1.cshtml");
    }

    // CREATE - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateLaboratoryEquipmentF1(LaboratoryEquipmentF1DTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/LabEquimentF1/CreateLaboratoryEquipmentF1.cshtml", product);
      }

      await HandleImageUpload(product);
      product.CategoryId = CategoryId;
      await _service.AddProductAsync(product);
      return RedirectToAction(nameof(ListLaboratoryEquipmentF1));
    }

    // EDIT - GET
    public async Task<IActionResult> EditLaboratoryEquipmentF1(int id)
    {
      var product = await _service.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      var categories = await _service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/LabEquimentF1/EditLaboratoryEquipmentF1.cshtml", product);
    }

    // EDIT - POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditLaboratoryEquipmentF1(LaboratoryEquipmentF1DTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/LabEquimentF1/EditLaboratoryEquipmentF1.cshtml", product);
      }

      var existing = await _service.GetProductByIdAsync(product.ProductId);
      if (existing == null) return NotFound();

      if (product.imageFile != null && product.imageFile.Length > 0)
        await HandleImageUpload(product);
      else
        product.image = existing.image;

      product.CategoryId = CategoryId;
      await _service.UpdateProductAsync(product);
      return RedirectToAction(nameof(ListLaboratoryEquipmentF1));
    }

    // DELETE
    [HttpPost]
    public async Task<IActionResult> Delete(int productId)
    {
      if (productId <= 0) return BadRequest("Invalid ID.");
      await _service.DeleteProductAsync(productId);
      return Json(new { success = true, message = "Thiết bị đã được xóa!" });
    }

    // SHOW
    public async Task<IActionResult> ShowLaboratoryEquipmentF1(int id)
    {
      var product = await _service.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("~/Views/ProductQC/LabEquimentF1/ShowLaboratoryEquipmentF1.cshtml", product);
    }

    private async Task HandleImageUpload(LaboratoryEquipmentF1DTO product)
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
