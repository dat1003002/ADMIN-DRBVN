using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class TTBangTaiController : Controller
  {
    private readonly ITTBangTaiService _ttBangTaiService;
    private const int PageSize = 9;

    // Cho phép CategoryId từ 27 đến 31
    private readonly List<int> AllowedCategoryIds = Enumerable.Range(29, 5).ToList(); // 27,28,29,30,31

    public TTBangTaiController(ITTBangTaiService ttBangTaiService)
    {
      _ttBangTaiService = ttBangTaiService;
    }

    // Danh sách chung - hỗ trợ categoryId động, mặc định 27 (Sơ Đồ Tổ Chức)
    public IActionResult ListSoDoToChuc(int categoryId = 29, int page = 1, string searchName = null)
    {
      if (!AllowedCategoryIds.Contains(categoryId))
        return NotFound();

      var query = string.IsNullOrWhiteSpace(searchName)
          ? _ttBangTaiService.GetProducts(categoryId)
          : _ttBangTaiService.SearchProductsByName(searchName.Trim(), categoryId);

      var list = query.OrderBy(p => p.ProductId).ToList();
      var pagedList = list.ToPagedList(page, PageSize);

      ViewBag.SearchName = searchName;
      ViewBag.CurrentPage = page;
      ViewBag.CategoryId = categoryId;

      var categories = _ttBangTaiService.GetCategoriesAsync().GetAwaiter().GetResult();
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Bảng Thông Tin";

      PopulateAllowedCategoryListAsync(categoryId).GetAwaiter().GetResult();
      return View(pagedList);
    }

    // Phân Ca (categoryId = 28)
    public IActionResult ListPhanCa(int page = 1, string searchName = null)
    {
      const int categoryId = 30;
      return ListByCategory(categoryId, "ListPhanCa", page, searchName);
    }

    // Thông Báo (categoryId = 29)
    public IActionResult ListThongBao(int page = 1, string searchName = null)
    {
      const int categoryId = 31;
      return ListByCategory(categoryId, "ListThongBao", page, searchName);
    }

    // 5S/An Toàn (categoryId = 30)
    public IActionResult ListAnToan(int page = 1, string searchName = null)
    {
      const int categoryId = 32;
      return ListByCategory(categoryId, "ListAnToan", page, searchName);
    }

    // Thông Báo Tăng Ca (categoryId = 31)
    public IActionResult ListThongBaoTangCa(int page = 1, string searchName = null)
    {
      const int categoryId = 33;
      return ListByCategory(categoryId, "ListThongBaoTangCa", page, searchName);
    }

    // Helper chung cho các action danh sách riêng
    private IActionResult ListByCategory(int categoryId, string viewName, int page, string searchName)
    {
      if (!AllowedCategoryIds.Contains(categoryId))
        return NotFound();

      var query = string.IsNullOrWhiteSpace(searchName)
          ? _ttBangTaiService.GetProducts(categoryId)
          : _ttBangTaiService.SearchProductsByName(searchName.Trim(), categoryId);

      var list = query.OrderBy(p => p.ProductId).ToList();
      var pagedList = list.ToPagedList(page, PageSize);

      ViewBag.SearchName = searchName;
      ViewBag.CurrentPage = page;
      ViewBag.CategoryId = categoryId;

      var categories = _ttBangTaiService.GetCategoriesAsync().GetAwaiter().GetResult();
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Bảng Thông Tin";

      PopulateAllowedCategoryListAsync(categoryId).GetAwaiter().GetResult();
      return View(viewName, pagedList);
    }

    // GET: Thêm mới
    public IActionResult Create()
    {
      var model = new TTBangTaiDTO();
      PopulateAllowedCategoryListAsync(null).GetAwaiter().GetResult();
      return View("~/Views/TTBangTai/Create.cshtml", model);
    }

    // POST: Thêm mới
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TTBangTaiDTO ttBangTaiDTO)
    {
      if (ttBangTaiDTO.CategoryId == 0 || !AllowedCategoryIds.Contains(ttBangTaiDTO.CategoryId))
      {
        ModelState.AddModelError("CategoryId", "Vui lòng chọn danh mục hợp lệ.");
      }

      if (ModelState.IsValid)
      {
        await _ttBangTaiService.AddProductAsync(ttBangTaiDTO);

        return ttBangTaiDTO.CategoryId switch
        {
          27 => RedirectToAction(nameof(ListSoDoToChuc), new { categoryId = ttBangTaiDTO.CategoryId }),
          28 => RedirectToAction(nameof(ListPhanCa)),
          29 => RedirectToAction(nameof(ListThongBao)),
          30 => RedirectToAction(nameof(ListAnToan)),
          31 => RedirectToAction(nameof(ListThongBaoTangCa)),
          _ => RedirectToAction(nameof(ListSoDoToChuc), new { categoryId = ttBangTaiDTO.CategoryId })
        };
      }

      PopulateAllowedCategoryListAsync(ttBangTaiDTO.CategoryId).GetAwaiter().GetResult();
      return View("~/Views/TTBangTai/Create.cshtml", ttBangTaiDTO);
    }

    // GET: Chỉnh sửa
    public async Task<IActionResult> Edit(int id)
    {
      var product = await _ttBangTaiService.GetProductByIdAsync(id);
      if (product == null || !AllowedCategoryIds.Contains(product.CategoryId))
        return NotFound();

      PopulateAllowedCategoryListAsync(product.CategoryId).GetAwaiter().GetResult();
      return View("~/Views/TTBangTai/Edit.cshtml", product);
    }

    // POST: Chỉnh sửa
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TTBangTaiDTO ttBangTaiDTO)
    {
      if (!AllowedCategoryIds.Contains(ttBangTaiDTO.CategoryId))
      {
        ModelState.AddModelError("CategoryId", "Danh mục không hợp lệ.");
      }

      if (ModelState.IsValid)
      {
        await _ttBangTaiService.UpdateProductAsync(ttBangTaiDTO);
        return RedirectToAction(nameof(ListSoDoToChuc), new { categoryId = ttBangTaiDTO.CategoryId });
      }

      PopulateAllowedCategoryListAsync(ttBangTaiDTO.CategoryId).GetAwaiter().GetResult();
      return View("~/Views/TTBangTai/Edit.cshtml", ttBangTaiDTO);
    }

    public async Task<IActionResult> ShowProduct(int id)
    {
      var product = await _ttBangTaiService.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("_ProductDetailPartial", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int productId)
    {
      try
      {
        await _ttBangTaiService.DeleteProductAsync(productId);
        return Json(new { success = true, message = "Đã xóa thành công!" });
      }
      catch (Exception ex)
      {
        return Json(new { success = false, message = "Lỗi khi xóa: " + ex.Message });
      }
    }

    public async Task<IActionResult> ShowModal(int id)
    {
      var product = await _ttBangTaiService.GetProductByIdAsync(id);
      if (product == null) return NotFound();

      return PartialView("~/Views/TTBangTai/ShowModal.cshtml", product);
    }

    private async Task PopulateAllowedCategoryListAsync(int? selectedCategoryId)
    {
      var categories = await _ttBangTaiService.GetCategoriesAsync();
      var allowedItems = categories
          .Where(c => AllowedCategoryIds.Contains(c.CategoryId))
          .OrderBy(c => c.CategoryId)
          .Select(c => new SelectListItem
          {
            Value = c.CategoryId.ToString(),
            Text = c.CategoryName
          })
          .ToList();

      allowedItems.Insert(0, new SelectListItem
      {
        Value = "",
        Text = "-- Chọn danh mục --"
      });

      ViewBag.CategoryList = new SelectList(
          allowedItems,
          "Value",
          "Text",
          selectedCategoryId?.ToString() ?? ""
      );
    }
  }
}
