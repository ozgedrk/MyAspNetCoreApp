using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp.Web.Controllers
{
    public class OrnekController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.name = "Ahmet";

            TempData["surname"] = "Yildiz";

            return View();


            //ViewBag.name = "ASP Net Core";
            //ViewBag.person = new { Id = 1, name = "Ozge", age = 23 };
            //ViewData["names"] = new List<string>() { "Ozge","Zelal","Melike"};
            //ViewData["age"] = 30;
        }

        public IActionResult Index2()
        {
            
            var surName = TempData["surname"];

            return View();
        }
        public IActionResult Index3()
        {
            return RedirectToAction("Index", "Ornek");
        }
        public IActionResult ParametreView(int id)
        {
            return RedirectToAction("JsonResultParametre", "Ornek", new { id = id });
        }

        public IActionResult JsonResultParametre(int id)
        {
            return Json(new { Id = id });
        }
        public IActionResult JsonResult()
        {
            return Json(new { id = 1, name = "Ozge", price = 100 });
        }
    }
}
