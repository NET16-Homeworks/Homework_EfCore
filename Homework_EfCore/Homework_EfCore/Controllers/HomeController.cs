using Microsoft.AspNetCore.Mvc;
using Homework_EfCore.Services;

namespace Homework_EfCore.Controllers;

public class HomeController : Controller
{
    public async Task<IActionResult> Index()
    {
        DbServices dbHelpers = new();
        //await dbHelpers.FillDb();

        return View();
    }
}