using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class AttendanceController : Controller
    {
        public IActionResult Manage()
        {
            return View();
        }
    }
}
