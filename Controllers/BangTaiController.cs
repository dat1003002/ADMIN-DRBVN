using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class BangTaiController : Controller
  {
    private readonly IBangTaiService _bangTaiService;
    private const int PageSize = 9;

    private readonly List<int> AllowedCategoryIds = Enumerable.Range(16, 13).ToList();

    public BangTaiController(IBangTaiService bangTaiService)
    {
      _bangTaiService = bangTaiService;
    }

    public async Task<IActionResult> ListThanhHinh(int categoryId = 16, int page = 1, string searchName = null)
    {
      if (!AllowedCategoryIds.Contains(categoryId))
        return NotFound();

      var query = string.IsNullOrWhiteSpace(searchName)
          ? await _bangTaiService.GetProductsAsync(categoryId)
          : await _bangTaiService.SearchProductsByNameAsync(searchName.Trim(), categoryId);

      var list = await query.OrderBy(p => p.ProductId).ToListAsync();
      var pagedList = list.ToPagedList(page, PageSize);

      ViewBag.SearchName = searchName;
      ViewBag.CurrentPage = page;
      ViewBag.CategoryId = categoryId;

      var categories = await _bangTaiService.GetCategoriesAsync();
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Sản phẩm";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View(pagedList);
    }

    public async Task<IActionResult> ListDunBien(int page = 1, string searchName = null) =>
        await ListByCategory(17, "ListDunBien", page, searchName, "Công Đoạn Đùn Biên");

    public async Task<IActionResult> ListLoiThep(int page = 1, string searchName = null) =>
        await ListByCategory(18, "ListLoiThep", page, searchName, "Công Đoạn Lõi Thép");

    public async Task<IActionResult> ListCatBo(int page = 1, string searchName = null) =>
        await ListByCategory(19, "ListCatBo", page, searchName, "Công Đoạn Cắt Bố");

    public async Task<IActionResult> ListLuuHoa(int page = 1, string searchName = null) =>
        await ListByCategory(20, "ListLuuHoa", page, searchName, "Công Đoạn Lưu Hóa");

    public async Task<IActionResult> ListNgoaiQuan(int page = 1, string searchName = null) =>
        await ListByCategory(21, "ListNgoaiQuan", page, searchName, "Kiểm Tra Ngoại Quan");

    public async Task<IActionResult> ListEndless(int page = 1, string searchName = null) =>
        await ListByCategory(22, "ListEndless", page, searchName, "Công Đoạn Endless");

    public async Task<IActionResult> ListFilter(int page = 1, string searchName = null) =>
        await ListByCategory(23, "ListFilter", page, searchName, "Công Đoạn Filter");

    public async Task<IActionResult> ListGrooving(int page = 1, string searchName = null) =>
        await ListByCategory(24, "ListGrooving", page, searchName, "Công Đoạn Grooving");

    public async Task<IActionResult> ListMoiNoi(int page = 1, string searchName = null) =>
        await ListByCategory(25, "ListMoiNoi", page, searchName, "Công Đoạn Mối Nối");

    public async Task<IActionResult> ListBHDLoiThep(int page = 1, string searchName = null) =>
        await ListByCategory(26, "ListBHDLoiThep", page, searchName, "BHD Hàng Lõi Thép");

    public async Task<IActionResult> ListThietKe(int page = 1, string searchName = null) =>
        await ListByCategory(27, "ListThietKe", page, searchName, "Thiết Kế");

    public async Task<IActionResult> ListKeHoachSanXuat(int page = 1, string searchName = null) =>
        await ListByCategory(28, "ListKeHoachSanXuat", page, searchName, "Kế Hoạch Sản Xuất");

    private async Task<IActionResult> ListByCategory(int categoryId, string viewName, int page, string searchName, string defaultName)
    {
      if (!AllowedCategoryIds.Contains(categoryId))
        return NotFound();

      var query = string.IsNullOrWhiteSpace(searchName)
          ? await _bangTaiService.GetProductsAsync(categoryId)
          : await _bangTaiService.SearchProductsByNameAsync(searchName.Trim(), categoryId);

      var list = await query.OrderBy(p => p.ProductId).ToListAsync();
      var pagedList = list.ToPagedList(page, PageSize);

      ViewBag.SearchName = searchName;
      ViewBag.CurrentPage = page;
      ViewBag.CategoryId = categoryId;

      var categories = await _bangTaiService.GetCategoriesAsync();
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? defaultName;

      await PopulateAllowedCategoryListAsync(categoryId);

      return View(viewName, pagedList);
    }

    public async Task<IActionResult> CreateTH()
    {
      var model = new BangTaiDTO();
      await PopulateAllowedCategoryListAsync(null);
      return View("~/Views/BangTai/CreateTH.cshtml", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateTH(BangTaiDTO bangTaiDTO)
    {
      if (bangTaiDTO.CategoryId == 0 || !AllowedCategoryIds.Contains(bangTaiDTO.CategoryId))
      {
        ModelState.AddModelError("CategoryId", "Vui lòng chọn danh mục hợp lệ.");
      }

      if (ModelState.IsValid)
      {
        await _bangTaiService.AddProductAsync(bangTaiDTO);

        return bangTaiDTO.CategoryId switch
        {
          16 => RedirectToAction(nameof(ListThanhHinh), new { categoryId = bangTaiDTO.CategoryId }),
          17 => RedirectToAction(nameof(ListDunBien)),
          18 => RedirectToAction(nameof(ListLoiThep)),
          19 => RedirectToAction(nameof(ListCatBo)),
          20 => RedirectToAction(nameof(ListLuuHoa)),
          21 => RedirectToAction(nameof(ListNgoaiQuan)),
          22 => RedirectToAction(nameof(ListEndless)),
          23 => RedirectToAction(nameof(ListFilter)),
          24 => RedirectToAction(nameof(ListGrooving)),
          25 => RedirectToAction(nameof(ListMoiNoi)),
          26 => RedirectToAction(nameof(ListBHDLoiThep)),
          27 => RedirectToAction(nameof(ListThietKe)),
          28 => RedirectToAction(nameof(ListKeHoachSanXuat)),
          _ => RedirectToAction(nameof(ListThanhHinh), new { categoryId = bangTaiDTO.CategoryId })
        };
      }

      await PopulateAllowedCategoryListAsync(bangTaiDTO.CategoryId);
      return View("~/Views/BangTai/CreateTH.cshtml", bangTaiDTO);
    }

    public async Task<IActionResult> Edit(int id)
    {
      var product = await _bangTaiService.GetProductByIdAsync(id);
      if (product == null || !AllowedCategoryIds.Contains(product.CategoryId))
        return NotFound();

      await PopulateAllowedCategoryListAsync(product.CategoryId);
      return View("~/Views/BangTai/EditTH.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BangTaiDTO bangTaiDTO)
    {
      if (!AllowedCategoryIds.Contains(bangTaiDTO.CategoryId))
      {
        ModelState.AddModelError("CategoryId", "Danh mục không hợp lệ.");
      }

      if (ModelState.IsValid)
      {
        await _bangTaiService.UpdateProductAsync(bangTaiDTO);
        return RedirectToAction(nameof(ListThanhHinh), new { categoryId = bangTaiDTO.CategoryId });
      }

      await PopulateAllowedCategoryListAsync(bangTaiDTO.CategoryId);
      return View("~/Views/BangTai/EditTH.cshtml", bangTaiDTO);
    }

    public async Task<IActionResult> ShowProduct(int id)
    {
      var product = await _bangTaiService.GetProductByIdAsync(id);
      if (product == null) return NotFound();

      return PartialView("_ProductDetailPartial", product);
    }

    public async Task<IActionResult> ShowModal(int id)
    {
      var product = await _bangTaiService.GetProductByIdAsync(id);
      if (product == null) return NotFound();

      return PartialView("~/Views/BangTai/ModalThanhHinh.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int productId)
    {
      try
      {
        await _bangTaiService.DeleteProductAsync(productId);
        return Json(new { success = true, message = "Sản phẩm đã được xóa thành công!" });
      }
      catch (Exception ex)
      {
        return Json(new { success = false, message = "Lỗi khi xóa sản phẩm: " + ex.Message });
      }
    }

    private async Task PopulateAllowedCategoryListAsync(int? selectedCategoryId)
    {
      var categories = await _bangTaiService.GetCategoriesAsync();

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
