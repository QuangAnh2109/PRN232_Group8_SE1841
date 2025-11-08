using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

