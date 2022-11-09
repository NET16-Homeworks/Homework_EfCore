using Homework_EfCore.Database;
using Microsoft.AspNetCore.Mvc;

namespace Homework_EfCore.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        DBService.info = message => ViewData["Info"] += message + "\n";
        return View();
    }

    public async Task<IActionResult> BorrowedBooks()
    {
        DBService.info = message => ViewData["Info"] += message + "\n";
        return View(await DBService.ReturnBorrowedBooksData());
    }

    public async Task<IActionResult> RemoveUselessUsers()
    {
        DBService.info = message => ViewData["Info"] += message + "\n";
        await DBService.RemoveUselessUsers();
        return View();
    }
        [HttpGet]
    public async Task<IActionResult> ReturnBook()
    {
        DBService.info = message => ViewData["Info"] += message + "\n";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ReturnBook(string userEmail, string bookName)
    {
        DBService.info = message => ViewData["Info"] += message + "\n";
        await DBService.ReturnBookFromUser(userEmail, bookName);
        return View();
    }
}