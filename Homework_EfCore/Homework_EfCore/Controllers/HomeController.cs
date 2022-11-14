using Homework_EfCore.Database;
using Microsoft.AspNetCore.Mvc;

namespace Homework_EfCore.Controllers;

public class HomeController : Controller
{
    private DBService dbservice = new DBService();
    public IActionResult Index()
    {
        dbservice.info = message => ViewData["Info"] += message + "\r\n";
        return View();
    }

    public async Task<IActionResult> BorrowedBooks()
    {
        dbservice.info = message => ViewData["Info"] += message + "\r\n";
        return View(await dbservice.ReturnBorrowedBooksData());
    }

    public async Task<IActionResult> RemoveUselessUsers()
    {
        dbservice.info = message => ViewData["Info"] += message + "\r\n";
        await dbservice.RemoveUselessUsers();
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> ReturnBook()
    {
        dbservice.info = message => ViewData["Info"] += message + "\r\n";
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ReturnBook(string userEmail, string bookName)
    {
        dbservice.info = message => ViewData["Info"] += message + "\r\n";
        await dbservice.ReturnBookFromUser(userEmail, bookName);
        return View();
    }
}