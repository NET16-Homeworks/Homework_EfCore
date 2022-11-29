using Homework_EfCore.Services;
using Homework_EfCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace Homework_EfCore.Controllers
{
    public class UsersBooksController : Controller
    {
        private DbServices _dbServices = new();
        public async Task<IActionResult> Index()
        {
            var usersBooks = await _dbServices.GetUsersBooksList();

            return View(usersBooks);
        }
        public async Task<IActionResult> RemoveUsersWithoutBooks()
        {
            var removedUsers = await _dbServices.RemoveUsersWithoutBooks();
            
            return View(removedUsers);
        }
        [HttpGet]
        public IActionResult ReturnBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ReturnBook(UserBookReturnViewModel returnedBook)
        {
            await _dbServices.ReturnBook(returnedBook);

            return RedirectToAction("Index", "UsersBooks");
        }

        public async Task FillDb()
        {
            await _dbServices.FillDb();
        }
    }
}
