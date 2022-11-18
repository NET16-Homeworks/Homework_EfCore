using Homework_EfCore.Models;
using Homework_EfCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Homework_EfCore.Controllers
{
    public class LibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(Users user)
        {
            using var context = new MyDBContext();
            await LibraryService.AddUser(context, user);
            return RedirectToAction("GetUsers");
        }
        public async Task<IActionResult> GetUsers()
        {
            using var context = new MyDBContext();
            var users = await LibraryService.GetUsers(context);
            return View(users);
        }
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(Books book, string authorFirstName, string authorLastName)
        {
            using var context = new MyDBContext();
            await LibraryService.AddBook(context, book, authorFirstName, authorLastName);
            return RedirectToAction("GetBooks");
        }
        public async Task<IActionResult> GetBooks()
        {
            using var context = new MyDBContext();
            var books = await LibraryService.GetBooks(context);
            return View(books);
        }
        public async Task<IActionResult> GetAuthors()
        {
            using var context = new MyDBContext();
            var authors = await LibraryService.GetAuthors(context);
            return View(authors);
        }
        [HttpGet]
        public IActionResult AddAuthor()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAuthor(Authors author)
        {
            using var context = new MyDBContext();
            await LibraryService.AddAuthor(context, author);
            return RedirectToAction("GetAuthors");
        }
        [HttpPost]
        public async Task<IActionResult> TakeBook(Books book, string userEmail, string authorFirstName, string authorLastName)
        {
            using var context = new MyDBContext();
            await LibraryService.TakeBook(context, book, userEmail, authorFirstName, authorLastName);
            return RedirectToAction("GetUserBooks");
        }
        [HttpGet]
        public IActionResult TakeBook()
        {
            return View();
        }
        public async Task<IActionResult> GetUserBooks()
        {
            using var context = new MyDBContext();
            var listWithUserBooksInfo = await LibraryService.GetUserBooks(context);
            return View(listWithUserBooksInfo);
        }

        public async Task<IActionResult> DeleteUsersWithoutBooks()
        {
            using var context = new MyDBContext();
            var fullNameOFDeletedUser = await LibraryService.DeleteUsersWithoutBooks(context);
            if (fullNameOFDeletedUser.IsNullOrEmpty())
            {
                return RedirectToAction("GetUsers");
            }
            return View("DeleteUsersWithoutBooks", fullNameOFDeletedUser);
        }
        [HttpGet]
        public IActionResult ReturnBook()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ReturnBook(string userEmail, string bookName)
        {
            var context = new MyDBContext();
            await LibraryService.ReturnBook(context, userEmail, bookName);
            return RedirectToAction("GetUserBooks");
        }

    }
}