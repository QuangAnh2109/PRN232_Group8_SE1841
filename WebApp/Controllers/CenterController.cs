using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class CenterController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}