using Microsoft.AspNetCore.Mvc;

namespace Homework_EfCore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}