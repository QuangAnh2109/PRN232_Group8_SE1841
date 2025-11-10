using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}