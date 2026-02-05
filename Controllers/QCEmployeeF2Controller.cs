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
  public class QCEmployeeF2Controller : Controller
  {
    private readonly IQCEmployeeF2Service _qcEmployeeF2Service;
    private const int CategoryId = 27; // QC Employee F#2 category
    private const int PageSize = 9;

    public QCEmployeeF2Controller(IQCEmployeeF2Service qcEmployeeF2Service)
    {
      _qcEmployeeF2Service = qcEmployeeF2Service;
    }

    // GET: /QCEmployeeF2/QCEmployeeF2
    public async Task<IActionResult> QCEmployeeF2(int page = 1)
    {
      var products = await _qcEmployeeF2Service.GetProducts(CategoryId, page, PageSize);
      ViewData["SearchTerm"] = null; // Xóa SearchTerm khi hiển thị danh sách đầy đủ
      return View("~/Views/ProductQC/ListQCEmployeeF2.cshtml", products);
    }

    // GET: /QCEmployeeF2/SearchQCEmployeeF2
    public async Task<IActionResult> SearchQCEmployeeF2(string name, int page = 1)
    {
      if (string.IsNullOrEmpty(name))
      {
        return RedirectToAction(nameof(QCEmployeeF2));
      }
      var products = await _qcEmployeeF2Service.SearchProductsByNameAsync(name, CategoryId, page, PageSize);
      ViewData["SearchTerm"] = name; // Thêm ViewData để view sử dụng cho input và pagination
      TempData["SearchTerm"] = name;
      TempData.Keep("SearchTerm");
      return View("~/Views/ProductQC/ListQCEmployeeF2.cshtml", products);
    }

    // GET: /QCEmployeeF2/CreateQCEmployeeF2
    public async Task<IActionResult> CreateQCEmployeeF2()
    {
      var categories = await _qcEmployeeF2Service.GetCategories();
      var filteredCategories = categories.Where(c => c.CategoryId == CategoryId).ToList();
      ViewBag.CategoryList = new SelectList(filteredCategories, "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/CreateQCEmployeeF2.cshtml");
    }

    // POST: /QCEmployeeF2/CreateQCEmployeeF2
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateQCEmployeeF2(QCEmployeeF2DTO product)
    {
      if (ModelState.IsValid)
      {
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
        await _qcEmployeeF2Service.AddProductAsync(product);
        return RedirectToAction(nameof(QCEmployeeF2));
      }

      var categories = await _qcEmployeeF2Service.GetCategories();
      var filteredCategories = categories.Where(c => c.CategoryId == CategoryId).ToList();
      ViewBag.CategoryList = new SelectList(filteredCategories, "CategoryId", "CategoryName");
      return View("~/Views/ProductQC/CreateQCEmployeeF2.cshtml", product);
    }

    // GET: /QCEmployeeF2/EditQCEmployeeF2
    public async Task<IActionResult> EditQCEmployeeF2(int id)
    {
      var product = await _qcEmployeeF2Service.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }

      var categories = await _qcEmployeeF2Service.GetCategories();
      var filteredCategories = categories.Where(c => c.CategoryId == CategoryId).ToList();
      ViewBag.CategoryList = new SelectList(filteredCategories, "CategoryId", "CategoryName", product.CategoryId);
      return View("~/Views/ProductQC/EditQCEmployeeF2.cshtml", product);
    }

    // POST: /QCEmployeeF2/EditQCEmployeeF2
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditQCEmployeeF2(QCEmployeeF2DTO product)
    {
      if (ModelState.IsValid)
      {
        var existingProduct = await _qcEmployeeF2Service.GetProductByIdAsync(product.ProductId);
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

        await _qcEmployeeF2Service.UpdateProductAsync(product);
        return RedirectToAction(nameof(QCEmployeeF2));
      }

      var categories = await _qcEmployeeF2Service.GetCategories();
      var filteredCategories = categories.Where(c => c.CategoryId == CategoryId).ToList();
      ViewBag.CategoryList = new SelectList(filteredCategories, "CategoryId", "CategoryName", product.CategoryId);
      return View("~/Views/ProductQC/EditQCEmployeeF2.cshtml", product);
    }

    // POST: /QCEmployeeF2/DeleteQCEmployeeF2
    [HttpPost]
    public async Task<IActionResult> DeleteQCEmployeeF2(int productId)
    {
      if (productId <= 0)
      {
        return BadRequest("Invalid Product ID.");
      }
      await _qcEmployeeF2Service.DeleteProductAsync(productId);
      return Json(new { success = true, message = "Hướng dẫn đã được xóa thành công!" });
    }

    // GET: /QCEmployeeF2/ShowQCEmployeeF2
    public async Task<IActionResult> ShowQCEmployeeF2(int id)
    {
      var product = await _qcEmployeeF2Service.GetProductByIdAsync(id);
      if (product == null)
      {
        return NotFound();
      }
      return PartialView("~/Views/ProductQC/ShowQCEmployeeF2.cshtml", product);
    }
  }
}
