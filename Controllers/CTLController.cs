using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class CTLController : Controller
  {
    private readonly ICTLService _ctlService;
    private const int PageSize = 9;

    private readonly List<int> AllowedCategoryIds = new List<int> { 46, 47 };

    public CTLController(ICTLService ctlService)
    {
      _ctlService = ctlService;
    }
    public async Task<IActionResult> ListProductStandardCTL(int page = 1, string searchName = null)
    {
      return await GetPagedList(46, page, searchName,
          "~/Views/ProductCTL/ListProductStandardCTL.cshtml",
          "Tiêu Chuẩn Theo Quy Cách");
    }

    public async Task<IActionResult> ListProductStandardShoepad(int page = 1, string searchName = null)
    {
      return await GetPagedList(47, page, searchName,
          "~/Views/ProductCTL/ListProductStandardShoepad.cshtml",
          "Tiêu Chuẩn Shoepad");
    }

    private async Task<IActionResult> GetPagedList(int categoryId, int page, string searchName, string viewPath, string defaultName)
    {
      var query = string.IsNullOrWhiteSpace(searchName)
          ? await _ctlService.GetProductsAsync(categoryId)
          : await _ctlService.SearchProductsByNameAsync(searchName.Trim(), categoryId);

      var list = await query.OrderBy(p => p.ProductId).ToListAsync();
      var pagedList = list.ToPagedList(page, PageSize);

      ViewBag.SearchName = searchName;
      ViewBag.CurrentPage = page;
      ViewBag.CategoryId = categoryId;
      ViewBag.CategoryName = defaultName;

      return View(viewPath, pagedList);
    }
    public async Task<IActionResult> CreateCTL(int categoryId = 0)
    {
      if (categoryId != 46 && categoryId != 47)
      {
        TempData["Error"] = "Không thể xác định danh mục. Vui lòng truy cập từ tab đúng.";
        return RedirectToAction(nameof(ListProductStandardCTL));
      }

      var model = new CTLDTO
      {
        CategoryId = categoryId
      };

      var categoryName = await GetCategoryNameByIdAsync(categoryId);
      ViewBag.CategoryName = categoryName;

      if (categoryName.Contains("Không tồn tại"))
      {
        ViewBag.ErrorMessage = categoryName;
      }

      return View("~/Views/ProductCTL/CreateCTL.cshtml", model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCTL(CTLDTO ctlDTO)
    {
      if (ModelState.IsValid)
      {
        await _ctlService.AddProductAsync(ctlDTO);

        return ctlDTO.CategoryId == 46
            ? RedirectToAction(nameof(ListProductStandardCTL))
            : RedirectToAction(nameof(ListProductStandardShoepad));
      }

      ViewBag.CategoryName = await GetCategoryNameByIdAsync(ctlDTO.CategoryId);
      return View("~/Views/ProductCTL/CreateCTL.cshtml", ctlDTO);
    }

    private async Task<string> GetCategoryNameByIdAsync(int categoryId)
    {
      var categories = await _ctlService.GetCategoriesAsync();
      var category = categories.FirstOrDefault(c => c.CategoryId == categoryId);

      if (category != null)
        return category.CategoryName;

      return "Danh mục không tồn tại, vui lòng kiểm tra lại";
    }

    public async Task<IActionResult> EditCTL(int id)
    {
      try
      {
        Console.WriteLine($"[DEBUG] EditCTL GET - ID: {id}");
        var product = await _ctlService.GetProductByIdAsync(id);

        if (product == null)
        {
          Console.WriteLine($"[ERROR] Product ID {id} not found");
          return NotFound($"Không tìm thấy sản phẩm ID = {id}");
        }

        if (!AllowedCategoryIds.Contains(product.CategoryId))
        {
          Console.WriteLine($"[ERROR] CategoryId {product.CategoryId} not allowed");
          return NotFound("Danh mục không hợp lệ");
        }

        await PopulateAllowedCategoryListAsync(product.CategoryId);
        return View("~/Views/ProductCTL/EditCTL.cshtml", product);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"[ERROR] EditCTL GET Exception: {ex.Message}");
        return StatusCode(500, $"Lỗi server: {ex.Message}");
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditCTL(CTLDTO ctlDTO)
    {
      try
      {
        if (!AllowedCategoryIds.Contains(ctlDTO.CategoryId))
        {
          ModelState.AddModelError("CategoryId", "Danh mục không hợp lệ.");
        }

        if (ModelState.IsValid)
        {
          await _ctlService.UpdateProductAsync(ctlDTO);
          return ctlDTO.CategoryId == 46
              ? RedirectToAction(nameof(ListProductStandardCTL))
              : RedirectToAction(nameof(ListProductStandardShoepad));
        }

        await PopulateAllowedCategoryListAsync(ctlDTO.CategoryId);
        return View("~/Views/ProductCTL/EditCTL.cshtml", ctlDTO);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"[ERROR] EditCTL POST Exception: {ex.Message}");
        ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật.");
        await PopulateAllowedCategoryListAsync(ctlDTO.CategoryId);
        return View("~/Views/ProductCTL/EditCTL.cshtml", ctlDTO);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProductStandardCTL(int productId)
    {
      try
      {
        await _ctlService.DeleteProductAsync(productId);
        return Json(new { success = true, message = "Đã xóa thành công!" });
      }
      catch (Exception ex)
      {
        return Json(new { success = false, message = ex.Message });
      }
    }

    public async Task<IActionResult> ModalCTL(int id)
    {
      var product = await _ctlService.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("~/Views/ProductCTL/ModalCTL.cshtml", product);
    }

    private async Task PopulateAllowedCategoryListAsync(int? selectedCategoryId)
    {
      var categories = await _ctlService.GetCategoriesAsync();
      var allowedItems = categories
          .Where(c => AllowedCategoryIds.Contains(c.CategoryId))
          .OrderBy(c => c.CategoryId)
          .Select(c => new SelectListItem
          {
            Value = c.CategoryId.ToString(),
            Text = c.CategoryName
          })
          .ToList();

      allowedItems.Insert(0, new SelectListItem { Value = "", Text = "-- Chọn danh mục --" });
      ViewBag.CategoryList = new SelectList(allowedItems, "Value", "Text", selectedCategoryId?.ToString() ?? "");
    }
  }
}
