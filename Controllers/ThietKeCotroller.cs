using Microsoft.AspNetCore.Mvc;

namespace AspnetCoreMvcFull.Controllers
{
  public class ThietKeController : Controller 
  {
    public IActionResult ListThietKe()
    {
      return View("~/Views/BangTai/ListThietKe.cshtml");
    }

  }
}
