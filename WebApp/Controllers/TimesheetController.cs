using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class TimesheetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index", "Class");
            }

            ViewBag.TimesheetId = id;
            return View();
        }
    }
}
