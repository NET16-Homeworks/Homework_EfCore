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
    public async Task<IActionResult> AddAuthor(string firstname, string lastname, string country, DateTime birthdate)
    {
        using var db = new MyDbContext();

        var addedAuthorData = new List<Author>()
        {
            new Author()
            {
                    FirstName = "Dmitriy",
                    LastName = "Danenkov",
                    Country = "Belarus",
                    BirthDate = new DateTime(1999, 3, 9)
            },
            new Author()
            {
                    FirstName = "Dariya",
                    LastName = "Lomeiko",
                    Country = "Belarus",
                    BirthDate = new DateTime(2003, 10, 3),
            },

            new Author()
            {
                    FirstName = "Uladzimir",
                    LastName = "Strukau",
                    Country = "Belarus",
                    BirthDate = new DateTime(1992, 12, 01)
            },

            new Author()
            {
                    FirstName = "Hanna",
                    LastName = "Anreevna",
                    Country = "Moldova",
                    BirthDate = new DateTime(1991, 12, 20)
            },

            new Author()
            {
                    FirstName = "Sergey",
                    LastName = "Lobanovskiy",
                    Country = "Turkey",
                    BirthDate = new DateTime(1955, 3, 7)
            }
        };
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
                FirstName = "Mitya",
                LastName = "Valenok",
                Email = "VALENOCHEK@gmail.com",
                BirthDate = new DateTime(1995, 11, 7),
                },

                new User()
                {
                    FirstName = "Ararat",
                    LastName = "Keschan",
                    Email = "ARARAT@gmail.com",
                    BirthDate = new DateTime(1976, 5, 15)
                },

                new User()
                {
                    FirstName = "Maksim",
                    LastName = "Piatrou",
                    Email = "maks.piatrou@mail.ru",
                    BirthDate = new DateTime(1981, 7, 22)
                },

                new User()
                {
                  FirstName = "Harry",
                  LastName = "Potter",
                  Email = "Poter@gmail.com",
                  BirthDate = new DateTime(1999, 8, 3)
                },

                new User()
                {
                FirstName = "Hermiona",
                LastName = "Grayne",
                Email = "Hermiona@gmail.com",
                BirthDate = new DateTime(1994, 2, 1)
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
                    Name = "Boring Tornado",
                    Year = 2017,
                    Author = (await db.Authors.SingleOrDefaultAsync(q => q.FirstName == "Dmitriy"))
                },

                new Book()
                {
                    Name = "Bungalo",
                    Year = 2011,
                    Author = (await db.Authors.SingleOrDefaultAsync(q => q.FirstName == "Dariya"))
                },

                new Book()
                {
                    Name = "Lepka pelmeshek",
                    Year = 2009,
                    Author = (await db.Authors.SingleOrDefaultAsync(q => q.FirstName == "Sergey"))
                },

                new Book()
                {
                    Name = "Lastochka",
                    Year = 2012,
                    Author = (await db.Authors.SingleOrDefaultAsync(q => q.FirstName == "Sergey"))
                },

                new Book()
                {
                    Name = "WW 2",
                    Year = 1968,
                    Author = (await db.Authors.SingleOrDefaultAsync(q => q.FirstName == "Uladzimir"))
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
                AuthorFullName = string.Concat(a.FirstName, "+", a.LastName),
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
            UserFullName = string.Concat(q.User.FirstName, "+", q.User.LastName),
            UserBirthDate = q.User.BirthDate,
            BookName = q.Book.Name,
            BookYear = q.Book.Year,
            AuthorFullName = String.Concat(q.Book.Author.FirstName, "+", q.Book.Author.LastName)
        }).ToListAsync();
        await db.SaveChangesAsync();
        return View(selected);
    }

    public async Task<IActionResult> DeleteUser()
    {
        using var db = new MyDbContext();
        var removedUsers = await db.Users.Where(q => q.UserBooks.Any() == false)
            .ToListAsync();
        var choosen = removedUsers.Select(q => new UserFullNameModel
        {
            FullName = string.Concat(q.FirstName, " ", q.LastName)
        }).ToString();

        if (removedUsers.Any())
        {
            db.RemoveRange(removedUsers);
            await db.SaveChangesAsync();
        }
        else
        {
            throw new ObjectException("Zero Users is HERE!");
        }

        await db.SaveChangesAsync();

        return View(choosen);
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
        using var database = new MyDbContext();

        var books = await database.Books.FirstOrDefaultAsync(q => q.Name == bookName);
        var user = await database.Users.SingleOrDefaultAsync(q => q.Email == userEmail);

        if (books != null && user != null)
        {
            if (!await database.UserBooks.AnyAsync(q => q.UserId == user.UserId & q.BookId == books.BookId))
            {
                await database.UserBooks.AddAsync(new UserBook()
                {
                    UserId = user.UserId,
                    BookId = books.BookId
                });
                await database.SaveChangesAsync();

            }
        }
        else
        {
            throw new ObjectException("NO USERS/BOOKS IS HERE");
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
                db.UserBooks.Remove(userResult);
            await db.SaveChangesAsync();
        }
         else
         {          
                throw new ObjectException("NO USERS/BOOKS IS HERE");
         }

        return RedirectToAction("GetUsersInfo", "Home");
    }
}
