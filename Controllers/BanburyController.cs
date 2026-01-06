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
  public class BanburyController : Controller
  {
    private readonly IMSService _msService;
    private const int PageSize = 9;
    private readonly List<int> AllowedCategoryIds = new List<int> { 34, 35, 36, 37, 38 };

    public BanburyController(IMSService msService)
    {
      _msService = msService;
    }
    public async Task<IActionResult> ListBanbury(int categoryId = 34, int page = 1, string searchName = null)
    {
      if (!AllowedCategoryIds.Contains(categoryId))
        return NotFound();

      var query = string.IsNullOrWhiteSpace(searchName)
          ? await _msService.GetProductsAsync(categoryId)
          : await _msService.SearchProductsByNameAsync(searchName.Trim(), categoryId);

      var list = await query.OrderBy(p => p.ProductId).ToListAsync();
      var pagedList = list.ToPagedList(page, PageSize);

      ViewBag.SearchName = searchName;
      ViewBag.CurrentPage = page;
      ViewBag.CategoryId = categoryId;

      var categories = await _msService.GetCategoriesAsync();
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Sản phẩm";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View(pagedList);
    }

    public async Task<IActionResult> ListOpenmill(int page = 1, string searchName = null) =>
        await ListByCategory(35, "ListOpenmill", page, searchName, "Openmill");

    public async Task<IActionResult> ListWeighing(int page = 1, string searchName = null) =>
        await ListByCategory(36, "ListWeighing", page, searchName, "Weighing");

    public async Task<IActionResult> ListStorage(int page = 1, string searchName = null) =>
        await ListByCategory(37, "ListStorage", page, searchName, "Storage");

    public async Task<IActionResult> ListCalender(int page = 1, string searchName = null) =>
        await ListByCategory(38, "ListCalender", page, searchName, "Calender");

    private async Task<IActionResult> ListByCategory(int categoryId, string viewName, int page, string searchName, string defaultName)
    {
      if (!AllowedCategoryIds.Contains(categoryId))
        return NotFound();

      var query = string.IsNullOrWhiteSpace(searchName)
          ? await _msService.GetProductsAsync(categoryId)
          : await _msService.SearchProductsByNameAsync(searchName.Trim(), categoryId);

      var list = await query.OrderBy(p => p.ProductId).ToListAsync();
      var pagedList = list.ToPagedList(page, PageSize);

      ViewBag.SearchName = searchName;
      ViewBag.CurrentPage = page;
      ViewBag.CategoryId = categoryId;

      var categories = await _msService.GetCategoriesAsync();
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? defaultName;

      await PopulateAllowedCategoryListAsync(categoryId);

      return View(viewName, pagedList);
    }

    public async Task<IActionResult> Create()
    {
      var model = new MSDTO();
      await PopulateAllowedCategoryListAsync(null);
      return View("~/Views/Banbury/Create.cshtml", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MSDTO mSDTO)
    {
      if (mSDTO.CategoryId == 0 || !AllowedCategoryIds.Contains(mSDTO.CategoryId))
      {
        ModelState.AddModelError("CategoryId", "Vui lòng chọn danh mục hợp lệ.");
      }

      if (ModelState.IsValid)
      {
        await _msService.AddProductAsync(mSDTO);

        return mSDTO.CategoryId switch
        {
          34 => RedirectToAction(nameof(ListBanbury), new { categoryId = mSDTO.CategoryId }),
          35 => RedirectToAction(nameof(ListOpenmill)),
          36 => RedirectToAction(nameof(ListWeighing)),
          37 => RedirectToAction(nameof(ListStorage)),
          38 => RedirectToAction(nameof(ListCalender)),
          _ => RedirectToAction(nameof(ListBanbury), new { categoryId = mSDTO.CategoryId })
        };
      }

      await PopulateAllowedCategoryListAsync(mSDTO.CategoryId);
      return View("~/Views/Banbury/Create.cshtml", mSDTO);
    }

    public async Task<IActionResult> Edit(int id)
    {
      var product = await _msService.GetProductByIdAsync(id);
      if (product == null || !AllowedCategoryIds.Contains(product.CategoryId))
        return NotFound();

      await PopulateAllowedCategoryListAsync(product.CategoryId);
      return View("~/Views/Banbury/Edit.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MSDTO mSDTO)
    {
      if (!AllowedCategoryIds.Contains(mSDTO.CategoryId))
      {
        ModelState.AddModelError("CategoryId", "Danh mục không hợp lệ.");
      }

      if (ModelState.IsValid)
      {
        await _msService.UpdateProductAsync(mSDTO);
        return RedirectToAction(nameof(ListBanbury), new { categoryId = mSDTO.CategoryId });
      }

      await PopulateAllowedCategoryListAsync(mSDTO.CategoryId);
      return View("~/Views/Banbury/Edit.cshtml", mSDTO);
    }

    public async Task<IActionResult> ShowProduct(int id)
    {
      var product = await _msService.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("_ProductDetailPartial", product);
    }

    public async Task<IActionResult> ShowModal(int id)
    {
      var product = await _msService.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("~/Views/Banbury/ShowModal.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int productId)
    {
      try
      {
        await _msService.DeleteProductAsync(productId);
        return Json(new { success = true, message = "Sản phẩm đã được xóa thành công!" });
      }
      catch (Exception ex)
      {
        return Json(new { success = false, message = "Lỗi khi xóa sản phẩm: " + ex.Message });
      }
    }

    private async Task PopulateAllowedCategoryListAsync(int? selectedCategoryId)
    {
      var categories = await _msService.GetCategoriesAsync();
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

      ViewBag.CategoryList = new SelectList(allowedItems, "Value", "Text", selectedCategoryId?.ToString() ?? "");
    }
  }
}
