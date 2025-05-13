using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Models;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class QuyCachCaoSuCTLController : Controller
  {
    private readonly IQuyCachCSCTLService _quyCachCSCTLService;
    private readonly ILogger<QuyCachCaoSuCTLController> _logger;
    private const int DefaultCategoryId = 13;
    private const int PageSize = 10;

    public QuyCachCaoSuCTLController(IQuyCachCSCTLService quyCachCSCTLService, ILogger<QuyCachCaoSuCTLController> logger)
    {
      _quyCachCSCTLService = quyCachCSCTLService ?? throw new ArgumentNullException(nameof(quyCachCSCTLService));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> ListQuyCachCSDCTL(int page = 1)
    {
      try
      {
        var products = await _quyCachCSCTLService.GetProducts(DefaultCategoryId) ?? new List<QuyCachCaoSuCTLDTO>();
        var pagedList = products.ToPagedList(page, PageSize);
        return View("~/Views/ProductCTL/ListQuyCachCSDCTL.cshtml", pagedList);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error retrieving product list for category {CategoryId}", DefaultCategoryId);
        TempData["ErrorMessage"] = "An error occurred while loading the product list.";
        return View("~/Views/ProductCTL/ListQuyCachCSDCTL.cshtml", new List<QuyCachCaoSuCTLDTO>().ToPagedList(page, PageSize));
      }
    }
    public async Task<IActionResult> CreateProduct()
    {
      try
      {
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/CreateQuyCachCSDCTL.cshtml", new QuyCachCaoSuCTLDTO { CategoryId = DefaultCategoryId });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Lỗi khi tải form tạo sản phẩm");
        TempData["ErrorMessage"] = "Đã xảy ra lỗi khi tải form tạo sản phẩm.";
        return View("~/Views/ProductCTL/CreateQuyCachCSDCTL.cshtml", new QuyCachCaoSuCTLDTO());
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(QuyCachCaoSuCTLDTO product)
    {
      if (product == null)
      {
        TempData["ErrorMessage"] = "Dữ liệu không hợp lệ.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/CreateQuyCachCSDCTL.cshtml", new QuyCachCaoSuCTLDTO());
      }

      try
      {
        await _quyCachCSCTLService.CreateProductAsync(product);
        TempData["SuccessMessage"] = "Thêm sản phẩm thành công!";
        return RedirectToAction(nameof(ListQuyCachCSDCTL));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Lỗi khi tạo sản phẩm: {ProductName}", product.name);
        TempData["ErrorMessage"] = "Đã xảy ra lỗi khi lưu sản phẩm.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/CreateQuyCachCSDCTL.cshtml", product);
      }
    }
    public async Task<IActionResult> EditProduct(int id)
    {
      try
      {
        var product = await _quyCachCSCTLService.GetProductByIdAsync(id);
        if (product == null)
        {
          return NotFound();
        }

        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/EditQuyCachCSDCTL.cshtml", product);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error loading edit product form for ID {ProductId}", id);
        TempData["ErrorMessage"] = "An error occurred while loading the edit form.";
        return RedirectToAction(nameof(ListQuyCachCSDCTL));
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(QuyCachCaoSuCTLDTO product)
    {
      if (product == null)
      {
        TempData["ErrorMessage"] = "Invalid data.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/EditQuyCachCSDCTL.cshtml", new QuyCachCaoSuCTLDTO());
      }

      if (product.CategoryId != DefaultCategoryId)
      {
        ModelState.AddModelError("CategoryId", "Please select a valid category.");
      }

      if (!ModelState.IsValid)
      {
        TempData["ErrorMessage"] = "Please check the input data.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/EditQuyCachCSDCTL.cshtml", product);
      }

      try
      {
        await _quyCachCSCTLService.UpdateProductAsync(product);
        TempData["SuccessMessage"] = "Product updated successfully!";
        return RedirectToAction(nameof(ListQuyCachCSDCTL));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error updating product ID {ProductId}", product.ProductId);
        TempData["ErrorMessage"] = "An error occurred while updating the product.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/EditQuyCachCSDCTL.cshtml", product);
      }
    }

    public async Task<IActionResult> ShowProductById(int id)
    {
      try
      {
        var product = await _quyCachCSCTLService.GetProductByIdAsync(id);
        if (product == null)
        {
          return NotFound();
        }
        return PartialView("~/Views/ProductCTL/ShowQuyCachCSDCTL.cshtml", product);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error displaying product details for ID {ProductId}", id);
        return StatusCode(500, "An error occurred while displaying product details.");
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(int id)
    {
      try
      {
        var product = await _quyCachCSCTLService.GetProductByIdAsync(id);
        if (product == null)
        {
          return Json(new { success = false, message = "Product not found." });
        }

        await _quyCachCSCTLService.DeleteProductAsync(id);
        return Json(new { success = true, message = "Product deleted successfully!" });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error deleting product ID {ProductId}", id);
        return Json(new { success = false, message = "An error occurred while deleting the product." });
      }
    }

    private async Task PopulateCategoriesAsync()
    {
      try
      {
        var categories = await _quyCachCSCTLService.GetCategories() ?? new List<Category>();
        var filteredCategories = categories.Where(c => c.CategoryId == DefaultCategoryId).ToList();
        ViewBag.CategoryList = new SelectList(filteredCategories, "CategoryId", "CategoryName");
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error retrieving category list");
        ViewBag.CategoryList = new SelectList(new List<Category>(), "CategoryId", "CategoryName");
        TempData["ErrorMessage"] = "Unable to load category list.";
      }
    }
  }
}
