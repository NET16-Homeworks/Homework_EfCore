using Homework_EfCore.Database;
using Homework_EfCore.Database.Entities;
using Homework_EfCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Homework_EfCore.Exceptions;

namespace Homework_EfCore.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    //[NonAction]
    public async Task<IActionResult> AddAuthor(string firstname, string lastname, string country, DateTime birthdate)
    {
        using var db = new MyDbContext();

        var addedAuthorData = new List<Author>()
        {
            new Author()
            {
                    FirstName = "Aishan",
                    LastName = "Uigurova",
                    Country = "Russia",
                    BirthDate = new DateTime(1975, 5, 15)
            },
            new Author()
            {
                    FirstName = "Henrik",
                    LastName = "Martirosyan",
                    Country = "Armenia",
                    BirthDate = new DateTime(1980, 5, 15),
            },

            new Author()
            {
                    FirstName = "Dmitriy",
                    LastName = "Akulov",
                    Country = "Russia",
                    BirthDate = new DateTime(1995, 11, 20)
            },

            new Author()
            {
                    FirstName = "Anastasiya",
                    LastName = "Akulova",
                    Country = "Russia",
                    BirthDate = new DateTime(1990, 11, 20)
            },

            new Author()
            {
                    FirstName = "Sergey",
                    LastName = "Gagarin",
                    Country = "Ukraine",
                    BirthDate = new DateTime(1932, 1, 19)
            }
        };

    //call out notification 
        await db.AddRangeAsync(addedAuthorData);
        await db.SaveChangesAsync();
        return RedirectToAction("GetUsersInfo", "Home");
    }

    public async Task<IActionResult> AddUser(string firstname, string lastname, string email, DateTime birthdate)
    {
        using var db = new MyDbContext();
        var addedUsersData = new List<User>()
        {
            new User()
                {
                FirstName = "Yemelya",
                LastName = "Kishkov",
                Email = "yem.kishk@gmail.com",
                BirthDate = new DateTime(1994, 12, 6),
                },

                new User()
                {
                    FirstName = "Lyuda",
                    LastName = "Gnezdilova",
                    Email = "lyud.gnezd@gmail.com",
                    BirthDate = new DateTime(1976, 1, 17)
                },

                new User()
                {
                    FirstName = "Maksim",
                    LastName = "Piatrou",
                    Email = "maks.piatrou@mail.ru",
                    BirthDate = new DateTime(1988, 7, 1)
                },

                new User()
                {
                  FirstName = "Harry",
                  LastName = "Minov",
                  Email = "har.minov@gmail.com",
                  BirthDate = new DateTime(2000, 8, 1)
                },

                new User()
                {
                FirstName = "Kilana",
                LastName = "Makira",
                Email = "kil.makira@gmail.com",
                BirthDate = new DateTime(1996, 8, 1)
                }
        };
        await db.AddRangeAsync(addedUsersData);
        await db.SaveChangesAsync();
        return RedirectToAction("GetUsersInfo", "Home");
    }

    public async Task<IActionResult> AddBooksData()
    {
        using var db = new MyDbContext();
        
        var addedBooksData = new List<Book>()
        {
                new Book()
                {
                    Name = "Topolya",
                    Year = 2014,
                    Author = (await db.Authors.SingleOrDefaultAsync(q => q.FirstName == "Aishan"))
                },

                new Book()
                {
                    Name = "50 Ottenkov Serogo",
                    Year = 2012,
                    Author = (await db.Authors.SingleOrDefaultAsync(q => q.FirstName == "Henrik"))
                },

                new Book()
                {
                    Name = "Oskolki Pamyatsi",
                    Year = 2021,
                    Author = (await db.Authors.SingleOrDefaultAsync(q => q.FirstName == "Dmitriy"))
                },

                new Book()
                {
                    Name = "Chaika",
                    Year = 2012,
                    Author = (await db.Authors.SingleOrDefaultAsync(q => q.FirstName == "Sergey"))
                },

                new Book()
                {
                    Name = "Marshall",
                    Year = 1968,
                    Author = (await db.Authors.SingleOrDefaultAsync(q => q.FirstName == "Anastasiya"))
                }
        };

        await db.AddRangeAsync(addedBooksData);
        await db.SaveChangesAsync();
        return RedirectToAction("GetUsersInfo", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> GetBooksWithAuthors()
    {
        using var db = new MyDbContext();
        var joinedBooksAuthors = await db.Authors.Join(db.Books,
            a => a.AuthorId, b => b.BookId, (a, b) => new AuthorsBookModel()
            {
                AuthorFullName = string.Concat(a.FirstName, " ", a.LastName),
                BookName = b.Name
            }).ToListAsync();

        await db.SaveChangesAsync();

        return View(joinedBooksAuthors);
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersInfo()
    {
        using var db = new MyDbContext();

        var selected = await db.UserBooks.Select(q => new UserInfoModel()
        {
            UserFullName = string.Concat(q.User.FirstName, " ", q.User.LastName),
            UserBirthDate = q.User.BirthDate,
            BookName = q.Book.Name,
            BookYear = q.Book.Year,
            AuthorFullName = String.Concat(q.Book.Author.FirstName, " ", q.Book.Author.LastName)
        }).ToListAsync();

       //почему не выводятся те юзеры у которых нет книг?
        //birthdate если ставить nullable, выскакивает эррор

        await db.SaveChangesAsync();

        return View(selected);
    }

    public async Task<IActionResult> DeleteUser()
    {
        using var db = new MyDbContext();
        var removedUsers = await db.Users.Where(q => q.UserBooks.Any() == false)
            .ToListAsync();

        //выведет ли после удаления? - yep, the data is preserved in the db
        var selected = removedUsers.Select(q => new UserFullNameModel
        {
            FullName = string.Concat(q.FirstName, " ", q.LastName)
        }).ToString();

        if (removedUsers.Any())
        {
            db.RemoveRange(removedUsers);
            await db.SaveChangesAsync();
        }
            //foreach (var user in selected)
            //{
            //    Console.WriteLine($"User {user.FullName} has been removed");
            //    ViewBag.UserFullName += user.FullName;
            //}
        else
        {
            throw new ObjectNotFoundException("There are no such users");
        }

        await db.SaveChangesAsync();

        return View(selected);
    }

    [HttpGet]
    public IActionResult DeleteUsersPerIndex()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUsersPerIndex(int index)
    {
        using var db = new MyDbContext();

        var removedUsers = await db.Users.Where(q => q.UserId == index).ToListAsync();
        db.RemoveRange(removedUsers);
        await db.SaveChangesAsync();

        return RedirectToAction ("GetUsersInfo", "Home");
    }

    [HttpGet]
    public IActionResult GiveBookToUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> GiveBookToUser(string userEmail, string bookName)
    {
        using var db = new MyDbContext();

        var books = await db.Books.FirstOrDefaultAsync(q => q.Name == bookName);
        var user = await db.Users.SingleOrDefaultAsync(q => q.Email == userEmail);

        if (books != null && user != null)
        {
            if (!await db.UserBooks.AnyAsync(q => q.UserId == user.UserId & q.BookId == books.BookId))
            {
                await db.UserBooks.AddAsync(new UserBook()
                {
                    UserId = user.UserId,
                    BookId = books.BookId
                });
                await db.SaveChangesAsync();

            }
        }

        else
        {
            throw new ObjectNotFoundException("There is no such user or book");
        }

        return RedirectToAction("GetUsersInfo", "Home");
    }

    [HttpGet]
    public IActionResult ReturnBookFromUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ReturnBookFromUser(string userEmail, string bookName)
    {
        using var db = new MyDbContext();

        var books = await db.Books.FirstOrDefaultAsync(q => q.Name == bookName);
        var user = await db.Users.SingleOrDefaultAsync(q => q.Email == userEmail);

        if (books != null && user != null)
        {
            var userResult = await db.UserBooks
                .SingleOrDefaultAsync(q => q.UserId == user.UserId && q.BookId == books.BookId);
            //var bookResult = await db.UserBooks
            //    .FirstOrDefaultAsync(q => q.BookId == books.BookId);

                db.UserBooks.Remove(userResult);
                //db.UserBooks.Remove(bookResult);

            await db.SaveChangesAsync();
        }

         else
         {          
                throw new ObjectNotFoundException("There are no such users or books");
         }

        return RedirectToAction("GetUsersInfo", "Home");
    }
}
