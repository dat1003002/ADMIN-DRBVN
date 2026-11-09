using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class MainMSController : Controller
  {
    private readonly IMSService _msService;
    private const int PageSize = 9;

    private readonly List<int> AllowedCategoryIds = new List<int>
        {
            34, 35, 36, 37, 39, 40, 41, 42, 38, 43, 44, 45,
            48, 49, 50, 51, 52, 53, 54, 55, 56, 57
        };

    private const string InfoDRB2View = "~/Views/MS/DRB2/ListThongTinDRB2.cshtml";

    private IActionResult RedirectToList(int categoryId) => categoryId switch
    {
      34 => RedirectToAction("ListBanburyDRB1"),
      35 => RedirectToAction("ListOpenMillDRB1"),
      36 => RedirectToAction("ListWeighingDRB1"),
      37 => RedirectToAction("ListStorageDRB1"),
      39 => RedirectToAction("ListBanburyDRB2"),
      40 => RedirectToAction("ListOpenMillDRB2"),
      41 => RedirectToAction("ListWeighingDRB2"),
      42 => RedirectToAction("ListStorageDRB2"),
      38 => RedirectToAction("ListCalender"),
      43 => RedirectToAction("ListOpenMillCalender"),
      44 => RedirectToAction("ListLinerWinding"),
      45 => RedirectToAction("ListReuseRubber"),
      48 => RedirectToAction("ListPhieuATHCDRB2"),
      49 => RedirectToAction("List5SDRB2"),
      50 => RedirectToAction("ListAnToanDRB2"),
      51 => RedirectToAction("ListSoDoToChucDRB2"),
      52 => RedirectToAction("ListThongBaoDRB2"),
      53 => RedirectToAction("ListVanHanhMayDRB2"),
      54 => RedirectToAction("ListChuyenCanDRB2"),
      55 => RedirectToAction("ListPheThaiDRB2"),
      56 => RedirectToAction("ListNangXuatDRB2"),
      57 => RedirectToAction("ListHinhAnhNguyenLieuDRB2"),
      _ => RedirectToAction("ListCalender")
    };

    public MainMSController(IMSService msService)
    {
      _msService = msService;
    }

    private async Task<IActionResult> GetPagedList(int categoryId, int page, string searchName, string viewPath, string pageTitle)
    {
      if (!AllowedCategoryIds.Contains(categoryId))
        return NotFound("Danh mục không hợp lệ.");

      var query = string.IsNullOrWhiteSpace(searchName)
          ? await _msService.GetProductsAsync(categoryId)
          : await _msService.SearchProductsByNameAsync(searchName.Trim(), categoryId);

      var list = await query.OrderBy(p => p.ProductId).ToListAsync();
      var pagedList = list.ToPagedList(page, PageSize);

      ViewBag.SearchName = searchName;
      ViewBag.CurrentPage = page;
      ViewBag.CategoryId = categoryId;
      ViewBag.Title = pageTitle;

      return View(viewPath, pagedList);
    }
    public async Task<IActionResult> ListBanburyDRB1(int page = 1, string searchName = null)
        => await GetPagedList(34, page, searchName, "~/Views/MS/DRB1/ListBanburyDRB1.cshtml", "TC Banbury DRB1");

    public async Task<IActionResult> ListOpenMillDRB1(int page = 1, string searchName = null)
        => await GetPagedList(35, page, searchName, "~/Views/MS/DRB1/ListOpenMillDRB1.cshtml", "TC Open mill DRB1");

    public async Task<IActionResult> ListWeighingDRB1(int page = 1, string searchName = null)
        => await GetPagedList(36, page, searchName, "~/Views/MS/DRB1/ListWeighingDRB1.cshtml", "TC Weighing DRB1");

    public async Task<IActionResult> ListStorageDRB1(int page = 1, string searchName = null)
        => await GetPagedList(37, page, searchName, "~/Views/MS/DRB1/ListStorageDRB1.cshtml", "TC Storage DRB1");

    public async Task<IActionResult> ListBanburyDRB2(int page = 1, string searchName = null)
        => await GetPagedList(39, page, searchName, "~/Views/MS/DRB2/ListBanburyDRB2.cshtml", "TC Banbury DRB2");

    public async Task<IActionResult> ListOpenMillDRB2(int page = 1, string searchName = null)
        => await GetPagedList(40, page, searchName, "~/Views/MS/DRB2/ListOpenMillDRB2.cshtml", "TC Open mill DRB2");

    public async Task<IActionResult> ListWeighingDRB2(int page = 1, string searchName = null)
        => await GetPagedList(41, page, searchName, "~/Views/MS/DRB2/ListWeighingDRB2.cshtml", "TC Weighing DRB2");

    public async Task<IActionResult> ListStorageDRB2(int page = 1, string searchName = null)
        => await GetPagedList(42, page, searchName, "~/Views/MS/DRB2/ListStorageDRB2.cshtml", "TC Storage DRB2");

    public async Task<IActionResult> ListPhieuATHCDRB2(int page = 1, string searchName = null)
        => await GetPagedList(48, page, searchName, InfoDRB2View, "Phiếu an toàn hóa chất DRB2");

    public async Task<IActionResult> List5SDRB2(int page = 1, string searchName = null)
        => await GetPagedList(49, page, searchName, InfoDRB2View, "5S Banbury DRB2");

    public async Task<IActionResult> ListAnToanDRB2(int page = 1, string searchName = null)
        => await GetPagedList(50, page, searchName, InfoDRB2View, "An Toàn Banbury DRB2");

    public async Task<IActionResult> ListSoDoToChucDRB2(int page = 1, string searchName = null)
        => await GetPagedList(51, page, searchName, InfoDRB2View, "Sơ đồ tổ chức Banbury DRB2");

    public async Task<IActionResult> ListThongBaoDRB2(int page = 1, string searchName = null)
        => await GetPagedList(52, page, searchName, InfoDRB2View, "Thông báo quan trọng Banbury DRB2");

    public async Task<IActionResult> ListVanHanhMayDRB2(int page = 1, string searchName = null)
        => await GetPagedList(53, page, searchName, InfoDRB2View, "Tỷ lệ vận hành máy Banbury DRB2");

    public async Task<IActionResult> ListChuyenCanDRB2(int page = 1, string searchName = null)
        => await GetPagedList(54, page, searchName, InfoDRB2View, "Tỷ lệ chuyên cần Hỗ Trợ Sản Xuất DRB2");

    public async Task<IActionResult> ListPheThaiDRB2(int page = 1, string searchName = null)
        => await GetPagedList(55, page, searchName, InfoDRB2View, "Tỷ lệ phế thải Hỗ Trợ Sản Xuất DRB2");

    public async Task<IActionResult> ListNangXuatDRB2(int page = 1, string searchName = null)
        => await GetPagedList(56, page, searchName, InfoDRB2View, "Năng xuất người/ giờ Banbury DRB2");

    public async Task<IActionResult> ListHinhAnhNguyenLieuDRB2(int page = 1, string searchName = null)
        => await GetPagedList(57, page, searchName, InfoDRB2View, "Hình ảnh nguyên liệu DRB2");

    public async Task<IActionResult> ListCalender(int page = 1, string searchName = null)
        => await GetPagedList(38, page, searchName, "~/Views/MS/Calender/ListCalender.cshtml", "TC Calender");

    public async Task<IActionResult> ListOpenMillCalender(int page = 1, string searchName = null)
        => await GetPagedList(43, page, searchName, "~/Views/MS/Calender/ListOpenMillCalender.cshtml", "TC Open mill Calender");

    public async Task<IActionResult> ListLinerWinding(int page = 1, string searchName = null)
        => await GetPagedList(44, page, searchName, "~/Views/MS/Calender/ListLinerWinding.cshtml", "TC Liner Winding");

    public async Task<IActionResult> ListReuseRubber(int page = 1, string searchName = null)
        => await GetPagedList(45, page, searchName, "~/Views/MS/Calender/ListReuseRubber.cshtml", "TC Re-use Rubber Line");
    public async Task<IActionResult> MainCreateMS(int categoryId)
    {
      if (!AllowedCategoryIds.Contains(categoryId))
      {
        TempData["Error"] = "Danh mục không hợp lệ.";
        return RedirectToAction("ListCalender");
      }

      var categoryName = await GetCategoryNameAsync(categoryId);
      if (categoryName.Contains("Không tồn tại"))
      {
        TempData["Error"] = categoryName;
        return RedirectToAction("ListCalender");
      }

      var model = new MSDTO { CategoryId = categoryId };
      ViewBag.CategoryName = categoryName;

      return View("~/Views/MS/MainCreateMS.cshtml", model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MainCreateMS(MSDTO mSDTO)
    {
      if (ModelState.IsValid)
      {
        await _msService.AddProductAsync(mSDTO);
        return RedirectToList(mSDTO.CategoryId);
      }

      ViewBag.CategoryName = await GetCategoryNameAsync(mSDTO.CategoryId);
      return View("~/Views/MS/MainCreateMS.cshtml", mSDTO);
    }

    public async Task<IActionResult> MainEditMS(int id)
    {
      var product = await _msService.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      if (!AllowedCategoryIds.Contains(product.CategoryId)) return NotFound();

      ViewBag.CategoryName = await GetCategoryNameAsync(product.CategoryId);
      return View("~/Views/MS/MainEditMS.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MainEditMS(MSDTO mSDTO)
    {
      if (ModelState.IsValid)
      {
        await _msService.UpdateProductAsync(mSDTO);
        return RedirectToList(mSDTO.CategoryId);
      }

      ViewBag.CategoryName = await GetCategoryNameAsync(mSDTO.CategoryId);
      return View("~/Views/MS/MainEditMS.cshtml", mSDTO);
    }

    public async Task<IActionResult> MainModalMS(int id)
    {
      var product = await _msService.GetProductByIdAsync(id);
      if (product == null) return NotFound();
      return PartialView("~/Views/MS/MainModalMS.cshtml", product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteMS(int productId)
    {
      try
      {
        await _msService.DeleteProductAsync(productId);
        return Json(new { success = true, message = "Đã xóa thành công!" });
      }
      catch (Exception ex)
      {
        return Json(new { success = false, message = ex.Message });
      }
    }

    private async Task<string> GetCategoryNameAsync(int categoryId)
    {
      var categories = await _msService.GetCategoriesAsync();
      var cat = categories.FirstOrDefault(c => c.CategoryId == categoryId);
      return cat?.CategoryName ?? $"Danh mục {categoryId} (Không tồn tại)";
    }
  }
}
