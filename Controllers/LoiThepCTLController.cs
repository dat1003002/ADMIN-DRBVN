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
  public class LoiThepCTLController : Controller
  {
    private readonly IProductLTCTLService _productLTCTLService;
    private readonly ILogger<LoiThepCTLController> _logger;
    private const int DefaultCategoryId = 12;
    private const int PageSize = 10;

    public LoiThepCTLController(IProductLTCTLService productLTCTLService, ILogger<LoiThepCTLController> logger)
    {
      _productLTCTLService = productLTCTLService ?? throw new ArgumentNullException(nameof(productLTCTLService));
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> ListLoiThepCTL(int page = 1)
    {
      try
      {
        var products = await _productLTCTLService.GetProducts(DefaultCategoryId) ?? new List<LoiThepCTLDTO>();
        var pagedList = products.ToPagedList(page, PageSize);
        return View("~/Views/ProductCTL/ListLoiThepCTL.cshtml", pagedList);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error retrieving product list for category {CategoryId}", DefaultCategoryId);
        TempData["ErrorMessage"] = "An error occurred while loading the product list.";
        return View("~/Views/ProductCTL/ListLoiThepCTL.cshtml", new List<LoiThepCTLDTO>().ToPagedList(page, PageSize));
      }
    }

    public async Task<IActionResult> CreateProductLT()
    {
      try
      {
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/CreateLoiThepCTL.cshtml", new LoiThepCTLDTO());
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error loading create product form");
        TempData["ErrorMessage"] = "An error occurred while loading the create product form.";
        return View("~/Views/ProductCTL/CreateLoiThepCTL.cshtml", new LoiThepCTLDTO());
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProductLT(LoiThepCTLDTO product)
    {
      if (product == null)
      {
        TempData["ErrorMessage"] = "Invalid data.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/CreateLoiThepCTL.cshtml", new LoiThepCTLDTO());
      }

      if (product.CategoryId != DefaultCategoryId)
      {
        ModelState.AddModelError("CategoryId", "Please select a valid category.");
      }

      if (!ModelState.IsValid)
      {
        TempData["ErrorMessage"] = "Please check the input data.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/CreateLoiThepCTL.cshtml", product);
      }

      try
      {
        await _productLTCTLService.CreateProductAsync(product);
        TempData["SuccessMessage"] = "Product added successfully!";
        return RedirectToAction(nameof(ListLoiThepCTL));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error creating product: {ProductName}", product.name);
        TempData["ErrorMessage"] = "An error occurred while saving the product.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/CreateLoiThepCTL.cshtml", product);
      }
    }

    public async Task<IActionResult> EditProductLT(int id)
    {
      try
      {
        var product = await _productLTCTLService.GetProductByIdAsync(id);
        if (product == null)
        {
          return NotFound();
        }

        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/EditLoiThepCTL.cshtml", product);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error loading edit product form for ID {ProductId}", id);
        TempData["ErrorMessage"] = "An error occurred while loading the edit form.";
        return RedirectToAction(nameof(ListLoiThepCTL));
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProductLT(LoiThepCTLDTO product)
    {
      if (product == null)
      {
        TempData["ErrorMessage"] = "Invalid data.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/EditLoiThepCTL.cshtml", new LoiThepCTLDTO());
      }

      if (product.CategoryId != DefaultCategoryId)
      {
        ModelState.AddModelError("CategoryId", "Please select a valid category.");
      }

      if (!ModelState.IsValid)
      {
        TempData["ErrorMessage"] = "Please check the input data.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/EditLoiThepCTL.cshtml", product);
      }

      try
      {
        await _productLTCTLService.UpdateProductAsync(product);
        TempData["SuccessMessage"] = "Product updated successfully!";
        return RedirectToAction(nameof(ListLoiThepCTL));
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error updating product ID {ProductId}", product.ProductId);
        TempData["ErrorMessage"] = "An error occurred while updating the product.";
        await PopulateCategoriesAsync();
        return View("~/Views/ProductCTL/EditLoiThepCTL.cshtml", product);
      }
    }

    public async Task<IActionResult> ShowProductLTById(int id)
    {
      try
      {
        var product = await _productLTCTLService.GetProductByIdAsync(id);
        if (product == null)
        {
          return NotFound();
        }
        return PartialView("~/Views/ProductCTL/ShowProductLTCTL.cshtml", product);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error displaying product details for ID {ProductId}", id);
        return StatusCode(500, "An error occurred while displaying product details.");
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProductLT(int id)
    {
      try
      {
        var product = await _productLTCTLService.GetProductByIdAsync(id);
        if (product == null)
        {
          return Json(new { success = false, message = "Product not found." });
        }

        await _productLTCTLService.DeleteProductAsync(id);
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
        var categories = await _productLTCTLService.GetCategories() ?? new List<Category>();
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
