using AspnetCoreMvcFull.ModelDTO.Product;
using AspnetCoreMvcFull.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Threading.Tasks;
using X.PagedList;

namespace AspnetCoreMvcFull.Controllers
{
  public class QCEmployeeF1Controller : Controller
  {
    private readonly IQCEmployeeF1Service _qcEmployeeF1Service;
    private const int CategoryId = 26; // QC Employee F#1 category
    private const int PageSize = 9;

    public QCEmployeeF1Controller(IQCEmployeeF1Service qcEmployeeF1Service)
    {
      _qcEmployeeF1Service = qcEmployeeF1Service;
    }

    // GET: /QCEmployeeF1/QCEmployeeF1
    public async Task<IActionResult> QCEmployeeF1(int page = 1)
    {
      var products = await _qcEmployeeF1Service.GetProducts(CategoryId, page, PageSize);
      ViewData["SearchTerm"] = null; // Xóa SearchTerm khi hiển thị danh sách đầy đủ (thay TempData để khớp view)
      return View("~/Views/ProductQC/ListQCEmployeeF1.cshtml", products);
    }

    // GET: /QCEmployeeF1/SearchQCEmployeeF1
    public async Task<IActionResult> SearchQCEmployeeF1(string name, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
      {
        return RedirectToAction(nameof(QCEmployeeF1));
      }
      var products = await _qcEmployeeF1Service.SearchProductsByNameAsync(name, CategoryId, page, PageSize);
      ViewData["SearchTerm"] = name; // Thêm ViewData để view sử dụng cho input và pagination (sửa lỗi chính)
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");
      return View("~/Views/ProductQC/ListQCEmployeeF1.cshtml", products);
    }

    // GET: /QCEmployeeF1/CreateQCEmployeeF1
    public async Task<IActionResult> CreateQCEmployeeF1()
    {
      var categories = await _qcEmployeeF1Service.GetCategories();
      var filteredCategories = categories.Where(c => c.CategoryId == CategoryId).ToList();
      ViewBag.CategoryList = new SelectList(filteredCategories, "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/CreateQCEmployeeF1.cshtml");
    }

    // POST: /QCEmployeeF1/CreateQCEmployeeF1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateQCEmployeeF1(QCEmployeeF1DTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _qcEmployeeF1Service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/CreateQCEmployeeF1.cshtml", product);
      }

      if (product.imageFile != null && product.imageFile.Length > 0)
      {
        var fileName = Path.GetFileName(product.imageFile.FileName);
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        if (!Directory.Exists(directoryPath))
        {
          Directory.CreateDirectory(directoryPath);
        }
        var filePath = Path.Combine(directoryPath, fileName);
        if (System.IO.File.Exists(filePath))
        {
          string newFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + Guid.NewGuid() + Path.GetExtension(fileName);
          filePath = Path.Combine(directoryPath, newFileName);
          product.image = newFileName;
        }
        else
        {
          product.image = fileName;
        }
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await product.imageFile.CopyToAsync(stream);
        }
      }

      await _qcEmployeeF1Service.AddProductAsync(product);
      return RedirectToAction(nameof(QCEmployeeF1));
    }

    // GET: /QCEmployeeF1/EditQCEmployeeF1
    public async Task<IActionResult> EditQCEmployeeF1(int id)
    {
      var product = await _qcEmployeeF1Service.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      var categories = await _qcEmployeeF1Service.GetCategories();
      ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/EditQCEmployeeF1.cshtml", product);
    }

    // POST: /QCEmployeeF1/EditQCEmployeeF1
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditQCEmployeeF1(QCEmployeeF1DTO product)
    {
      if (!ModelState.IsValid)
      {
        var categories = await _qcEmployeeF1Service.GetCategories();
        ViewBag.CategoryList = new SelectList(categories.Where(c => c.CategoryId == CategoryId), "CategoryId", "CategoryName");
        return View("~/Views/ProductQC/EditQCEmployeeF1.cshtml", product);
      }

      var existingProduct = await _qcEmployeeF1Service.GetProductByIdAsync(product.ProductId);
      if (existingProduct == null)
      {
        return NotFound();
      }

      if (product.imageFile != null && product.imageFile.Length > 0)
      {
        var fileName = Path.GetFileName(product.imageFile.FileName);
        var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
        if (!Directory.Exists(directoryPath))
        {
          Directory.CreateDirectory(directoryPath);
        }
        var filePath = Path.Combine(directoryPath, fileName);
        if (System.IO.File.Exists(filePath))
        {
          string newFileName = Path.GetFileNameWithoutExtension(fileName) + "_" + Guid.NewGuid() + Path.GetExtension(fileName);
          filePath = Path.Combine(directoryPath, newFileName);
          product.image = newFileName;
        }
        else
        {
          product.image = fileName;
        }
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
          await product.imageFile.CopyToAsync(stream);
        }
      }
      else
      {
        product.image = existingProduct.image;
      }

      await _qcEmployeeF1Service.UpdateProductAsync(product);
      return RedirectToAction(nameof(QCEmployeeF1));
    }

    // POST: /QCEmployeeF1/DeleteQCEmployeeF1
    [HttpPost]
    public async Task<IActionResult> DeleteQCEmployeeF1(int productId)
    {
      if (productId <= 0)
      {
        return BadRequest("Invalid Product ID.");
      }
      await _qcEmployeeF1Service.DeleteProductAsync(productId);
      return Json(new { success = true, message = "Hướng dẫn đã được xóa thành công!" });
    }

    // GET: /QCEmployeeF1/ShowQCEmployeeF1
    public async Task<IActionResult> ShowQCEmployeeF1(int id)
    {
      var product = await _qcEmployeeF1Service.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return PartialView("~/Views/ProductQC/ShowQCEmployeeF1.cshtml", product);
    }
  }
}
