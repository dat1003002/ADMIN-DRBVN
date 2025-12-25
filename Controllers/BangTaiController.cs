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
    private readonly List<int> AllowedCategoryIds = Enumerable.Range(16, 10).ToList();

    public BangTaiController(IBangTaiService bangTaiService)
    {
      _bangTaiService = bangTaiService;
    }

    // Danh sách sản phẩm - categoryId động, mặc định 16 để tương thích cũ
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

      // Để hiển thị menu chuyển danh mục nếu cần trong View
      await PopulateAllowedCategoryListAsync(categoryId);

      return View(pagedList);
    }
    // Danh sách sản phẩm - Công Đoạn Lưu Hóa (categoryId = 21)
    public async Task<IActionResult> ListLuuHoa(int page = 1, string searchName = null)
    {
      const int categoryId = 20;
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
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Công Đoạn Lưu Hóa";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View("ListLuuHoa", pagedList); // View: Views/BangTai/ListLuuHoa.cshtml
    }

    // Danh sách sản phẩm - Công Đoạn Cắt Bố (categoryId = 19)
    public async Task<IActionResult> ListCatBo(int page = 1, string searchName = null)
    {
      const int categoryId = 19;
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
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Công Đoạn Cắt Bố";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View("ListCatBo", pagedList); // View: Views/BangTai/ListCatBo.cshtml
    }
    // Danh sách sản phẩm - Công Đoạn Đùn Biên (categoryId = 17)
    public async Task<IActionResult> ListDunBien(int page = 1, string searchName = null)
    {
      const int categoryId = 17;
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
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Công Đoạn Đùn Biên";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View("ListDunBien", pagedList); // View: Views/BangTai/ListDunBien.cshtml
    }

    // Danh sách sản phẩm - Công Đoạn Lõi Thép (categoryId = 18)
    public async Task<IActionResult> ListLoiThep(int page = 1, string searchName = null)
    {
      const int categoryId = 18;
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
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Công Đoạn Lõi Thép";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View("ListLoiThep", pagedList); // View: Views/BangTai/ListLoiThep.cshtml
    }
    // Danh sách sản phẩm - Kiểm Tra Ngoại Quan (categoryId = 21)
    public async Task<IActionResult> ListNgoaiQuan(int page = 1, string searchName = null)
    {
      const int categoryId = 21;
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
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Kiểm Tra Ngoại Quan";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View("ListNgoaiQuan", pagedList); // View: Views/BangTai/ListNgoaiQuan.cshtml
    }

    // Danh sách sản phẩm - Công Đoạn Endless (categoryId = 22)
    public async Task<IActionResult> ListEndless(int page = 1, string searchName = null)
    {
      const int categoryId = 22;
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
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Công Đoạn Endless";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View("ListEndless", pagedList); // View: Views/BangTai/ListEndless.cshtml
    }

    // Danh sách sản phẩm - Công Đoạn Filter (categoryId = 23)
    public async Task<IActionResult> ListFilter(int page = 1, string searchName = null)
    {
      const int categoryId = 23;
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
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Công Đoạn Filter";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View("ListFilter", pagedList); // View: Views/BangTai/ListFilter.cshtml
    }

    // Danh sách sản phẩm - Công Đoạn Grooving (categoryId = 24)
    public async Task<IActionResult> ListGrooving(int page = 1, string searchName = null)
    {
      const int categoryId = 24;
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
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Công Đoạn Grooving";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View("ListGrooving", pagedList); // View: Views/BangTai/ListGrooving.cshtml
    }

  
    public async Task<IActionResult> ListMoiNoi(int page = 1, string searchName = null)
    {
      const int categoryId = 25;
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
      ViewBag.CategoryName = categories.FirstOrDefault(c => c.CategoryId == categoryId)?.CategoryName ?? "Công Đoạn Mối Nối";

      await PopulateAllowedCategoryListAsync(categoryId);

      return View("ListMoiNoi", pagedList); // View: Views/BangTai/ListMoiNoi.cshtml
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

        switch (bangTaiDTO.CategoryId)
        {
            case 16:
                return RedirectToAction(nameof(ListThanhHinh), new { categoryId = bangTaiDTO.CategoryId });
            case 17:
                return RedirectToAction(nameof(ListDunBien));
            case 18:
                return RedirectToAction(nameof(ListLoiThep));
            case 19:
                return RedirectToAction(nameof(ListCatBo));
            case 20:
                return RedirectToAction(nameof(ListLuuHoa));
            case 21:
                return RedirectToAction(nameof(ListNgoaiQuan));
            case 22:
                return RedirectToAction(nameof(ListEndless));
            case 23:
                return RedirectToAction(nameof(ListFilter));
            case 24:
                return RedirectToAction(nameof(ListGrooving));
            case 25:
                return RedirectToAction(nameof(ListMoiNoi));
            default:
                return RedirectToAction(nameof(ListThanhHinh), new { categoryId = bangTaiDTO.CategoryId });
        }
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

    // POST: Sửa
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

    // Partial chi tiết sản phẩm
    public async Task<IActionResult> ShowProduct(int id)
    {
      var product = await _bangTaiService.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("_ProductDetailPartial", product);
    }

    // Xóa sản phẩm (AJAX)
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

    // Modal chi tiết
    public async Task<IActionResult> ShowModal(int id)
    {
      var product = await _bangTaiService.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return PartialView("~/Views/BangTai/ModalThanhHinh.cshtml", product);
    }

    // Helper: Tạo dropdown chỉ chứa danh mục 16-25 + placeholder
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

      // Thêm placeholder buộc người dùng chọn
      allowedItems.Insert(0, new SelectListItem
      {
        Value = "",
        Text = "-- Chọn danh mục --"
      });

      ViewBag.CategoryList = new SelectList(
          allowedItems,
          "Value",
          "Text",
          selectedCategoryId?.ToString() ?? ""  // ĐÃ SỬA LỖI TẠI ĐÂY
      );
    }
  }
}
