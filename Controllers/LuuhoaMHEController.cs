using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace AspnetCoreMvcFull.Controllers
{
  public class LuuhoaMHEController : Controller
  {
    private readonly ILuuHoaMHEService _luuHoaMHEService;
    private readonly string _imageFolderPath;

    public LuuhoaMHEController(ILuuHoaMHEService luuHoaMHEService)
    {
      _luuHoaMHEService = luuHoaMHEService;
      _imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
      if (!Directory.Exists(_imageFolderPath))
      {
        Directory.CreateDirectory(_imageFolderPath);
      }
    }

    public async Task<IActionResult> ListLuuhoa(int page = 1, string searchName = null)
    {
      const int categoryId = 15;
      const int pageSize = 9;

      var products = string.IsNullOrEmpty(searchName)
          ? await _luuHoaMHEService.GetProducts(categoryId, page, pageSize)
          : await _luuHoaMHEService.SearchProductsByNameAsync(searchName, categoryId, page, pageSize);

      return View("~/Views/ProductMhe/ListLuuhoa.cshtml", products);
    }

    public async Task<IActionResult> CreateLHMHE()
    {
      await LoadCategories();
      return View("~/Views/ProductMhe/CreateLHMHE.cshtml");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateLHMHE(LuuHoaMHEDTO model)
    {
      if (string.IsNullOrWhiteSpace(model.name) && model.PdfFile != null)
      {
        model.name = Path.GetFileNameWithoutExtension(model.PdfFile.FileName);
      }

      try
      {
        string savedImageName = await _luuHoaMHEService.ProcessAndSaveFileAsync(model.PdfFile, model.imageFile, _imageFolderPath);
        model.image = savedImageName;
      }
      catch (InvalidOperationException ex)
      {
        ModelState.AddModelError("", ex.Message);
        await LoadCategories();
        return View("~/Views/ProductMhe/CreateLHMHE.cshtml", model);
      }
      catch (Exception ex)
      {
        ModelState.AddModelError("", "Lỗi xử lý file: " + ex.Message);
        await LoadCategories();
        return View("~/Views/ProductMhe/CreateLHMHE.cshtml", model);
      }

      model.CategoryId = 15;

      if (ModelState.IsValid)
      {
        await _luuHoaMHEService.AddProductAsync(model);
        return RedirectToAction(nameof(ListLuuhoa));
      }

      await LoadCategories();
      return View("~/Views/ProductMhe/CreateLHMHE.cshtml", model);
    }

    private async Task LoadCategories()
    {
      var cats = await _luuHoaMHEService.GetCategories();
      ViewBag.CategoryList = new SelectList(cats.Where(c => c.CategoryId == 15), "CategoryId", "CategoryName");
    }

    public async Task<IActionResult> EditLHMHE(int id)
    {
      var product = await _luuHoaMHEService.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      await LoadCategories();
      return View("~/Views/ProductMhe/EditLHMHE.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditLHMHE(LuuHoaMHEDTO model)
    {
      if (!ModelState.IsValid)
      {
        await LoadCategories();
        return View("~/Views/ProductMhe/EditLHMHE.cshtml", model);
      }

      var existing = await _luuHoaMHEService.GetProductByIdAsync(model.ProductId);
      if (existing == null) return NotFound();

      try
      {
        // Xử lý thay thế file (nếu có upload mới)
        string newImageName = await _luuHoaMHEService.ProcessAndSaveReplacementFileAsync(
            model.PdfFile,
            model.imageFile,
            _imageFolderPath,
            existing.image);

        model.image = newImageName;
        model.CategoryId = 15;

        await _luuHoaMHEService.UpdateProductAsync(model);

        return RedirectToAction(nameof(ListLuuhoa));
      }
      catch (InvalidOperationException ex)
      {
        ModelState.AddModelError("", ex.Message);
        await LoadCategories();
        return View("~/Views/ProductMhe/EditLHMHE.cshtml", model);
      }
      catch (Exception ex)
      {
        ModelState.AddModelError("", "Lỗi xử lý file: " + ex.Message);
        await LoadCategories();
        return View("~/Views/ProductMhe/EditLHMHE.cshtml", model);
      }
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProductLHMHE(int ProductId)
    {
      if (ProductId <= 0) return BadRequest();

      await _luuHoaMHEService.DeleteProductAsync(ProductId);
      return Ok();
    }

    public async Task<IActionResult> showProductLHMHE(int id)
    {
      var product = await _luuHoaMHEService.GetProductByIdAsync(id);
      if (product == null) return NotFound();

      return PartialView("~/Views/ProductMhe/ModalLuuHoaMHE.cshtml", product);
    }
  }
}
