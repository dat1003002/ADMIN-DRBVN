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
            34, 35, 36, 37, 47, 48, 49, 50, 38, 52, 53, 54
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
        => await GetPagedList(47, page, searchName, "~/Views/MS/DRB2/ListBanburyDRB2.cshtml", "TC Banbury DRB2");

    public async Task<IActionResult> ListOpenMillDRB2(int page = 1, string searchName = null)
        => await GetPagedList(48, page, searchName, "~/Views/MS/DRB2/ListOpenMillDRB2.cshtml", "TC Open mill DRB2");

    public async Task<IActionResult> ListWeighingDRB2(int page = 1, string searchName = null)
        => await GetPagedList(49, page, searchName, "~/Views/MS/DRB2/ListWeighingDRB2.cshtml", "TC Weighing DRB2");

    public async Task<IActionResult> ListStorageDRB2(int page = 1, string searchName = null)
        => await GetPagedList(50, page, searchName, "~/Views/MS/DRB2/ListStorageDRB2.cshtml", "TC Storage DRB2");

    public async Task<IActionResult> ListCalender(int page = 1, string searchName = null)
        => await GetPagedList(38, page, searchName, "~/Views/MS/Calender/ListCalender.cshtml", "TC Calender");

    public async Task<IActionResult> ListOpenMillCalender(int page = 1, string searchName = null)
        => await GetPagedList(52, page, searchName, "~/Views/MS/Calender/ListOpenMillCalender.cshtml", "TC Open mill Calender");

    public async Task<IActionResult> ListLinerWinding(int page = 1, string searchName = null)
        => await GetPagedList(53, page, searchName, "~/Views/MS/Calender/ListLinerWinding.cshtml", "TC Liner Winding");

    public async Task<IActionResult> ListReuseRubber(int page = 1, string searchName = null)
        => await GetPagedList(54, page, searchName, "~/Views/MS/Calender/ListReuseRubber.cshtml", "TC Re-use Rubber Line");
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

        return mSDTO.CategoryId switch
        {
          34 => RedirectToAction("ListBanburyDRB1"),
          35 => RedirectToAction("ListOpenMillDRB1"),
          36 => RedirectToAction("ListWeighingDRB1"),
          37 => RedirectToAction("ListStorageDRB1"),
          47 => RedirectToAction("ListBanburyDRB2"),
          48 => RedirectToAction("ListOpenMillDRB2"),
          49 => RedirectToAction("ListWeighingDRB2"),
          50 => RedirectToAction("ListStorageDRB2"),
          38 => RedirectToAction("ListCalender"),
          52 => RedirectToAction("ListOpenMillCalender"),
          53 => RedirectToAction("ListLinerWinding"),
          54 => RedirectToAction("ListReuseRubber"),
          _ => RedirectToAction("ListCalender")
        };
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

        return mSDTO.CategoryId switch
        {
          34 => RedirectToAction("ListBanburyDRB1"),
          35 => RedirectToAction("ListOpenMillDRB1"),
          36 => RedirectToAction("ListWeighingDRB1"),
          37 => RedirectToAction("ListStorageDRB1"),
          47 => RedirectToAction("ListBanburyDRB2"),
          48 => RedirectToAction("ListOpenMillDRB2"),
          49 => RedirectToAction("ListWeighingDRB2"),
          50 => RedirectToAction("ListStorageDRB2"),
          38 => RedirectToAction("ListCalender"),
          52 => RedirectToAction("ListOpenMillCalender"),
          53 => RedirectToAction("ListLinerWinding"),
          54 => RedirectToAction("ListReuseRubber"),
          _ => RedirectToAction("ListCalender")
        };
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
