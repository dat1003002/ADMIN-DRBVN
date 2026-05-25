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

    // Khai báo danh sách Category được phép (đây là dòng bị thiếu)
    private readonly List<int> AllowedCategoryIds = new List<int> { 39, 40 };

    public CTLController(ICTLService ctlService)
    {
      _ctlService = ctlService;
    }

    // ====================== LIST ======================
    public async Task<IActionResult> ListProductStandardCTL(int page = 1, string searchName = null)
    {
      return await GetPagedList(39, page, searchName,
          "~/Views/ProductCTL/ListProductStandardCTL.cshtml",
          "Tiêu Chuẩn Theo Quy Cách");
    }

    public async Task<IActionResult> ListProductStandardShoepad(int page = 1, string searchName = null)
    {
      return await GetPagedList(40, page, searchName,
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

    // ====================== CREATE ======================
    // ====================== CREATE ======================
    public IActionResult CreateCTL(int categoryId = 39)
    {
      var model = new CTLDTO
      {
        CategoryId = categoryId   // Tự động set danh mục theo tab
      };
      return View("~/Views/ProductCTL/CreateCTL.cshtml", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCTL(CTLDTO ctlDTO)
    {
      if (ModelState.IsValid)
      {
        await _ctlService.AddProductAsync(ctlDTO);

        // Redirect về đúng tab sau khi thêm thành công
        return ctlDTO.CategoryId == 39
            ? RedirectToAction(nameof(ListProductStandardCTL))
            : RedirectToAction(nameof(ListProductStandardShoepad));
      }

      // Nếu lỗi validate, trả về form và giữ CategoryId
      return View("~/Views/ProductCTL/CreateCTL.cshtml", ctlDTO);
    }

    // ====================== EDIT - GET ======================
    public async Task<IActionResult> EditCTL(int id)
    {
      try
      {
        Console.WriteLine($"[DEBUG] EditCTL GET - ID: {id}");

        var product = await _ctlService.GetProductByIdAsync(id);

        if (product == null)
        {
          Console.WriteLine($"[ERROR] Product ID {id} not found in database");
          return NotFound($"Không tìm thấy sản phẩm ID = {id}");
        }

        if (!AllowedCategoryIds.Contains(product.CategoryId))
        {
          Console.WriteLine($"[ERROR] CategoryId {product.CategoryId} is not allowed");
          return NotFound("Danh mục không hợp lệ");
        }

        // Load dropdown danh mục (rất quan trọng)
        await PopulateAllowedCategoryListAsync(product.CategoryId);

        Console.WriteLine($"[DEBUG] Success - Returning Edit view for ID {id}");
        return View("~/Views/ProductCTL/EditCTL.cshtml", product);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"[ERROR] EditCTL GET Exception: {ex.Message}");
        Console.WriteLine($"StackTrace: {ex.StackTrace}");
        return StatusCode(500, $"Lỗi server: {ex.Message}");
      }
    }

    // ====================== EDIT - POST ======================
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

          return ctlDTO.CategoryId == 39
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

    // ====================== DELETE ======================
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

    // ====================== SHOW MODAL ======================
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
