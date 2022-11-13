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

    public async Task<IActionResult> AddData()
    {
        using var db = new MyDbContext();

        var addedData = new List<Author>
            {
                new Author()
                {
                    FirstName = "Henrik",
                    LastName = "Martirosyan",
                    Country = "Armenia",
                    BirthDate = new DateTimeOffset(new DateTime(1980, 5, 15)),

                    Books = new List<Book>
                    {
                        new Book()
                        {
                            Name = "Topolya",
                            Year = 2014,
                            Users = new List<User>
                            {
                                new User()
                                {
                                    FirstName = "Lyuda",
                                    LastName = "Gnezdilova",
                                    Email = "lyud.gnezd@gmail.com",
                                    BirthDate = new DateTimeOffset(new DateTime(1976, 1, 17))
                                },
                            }
                        }
                    }
                },

                new Author()
                {
                    FirstName = "Aishan",
                    LastName = "Uigurova",
                    Country = "Russia",
                    BirthDate = new DateTimeOffset(new DateTime(1975, 5, 15)),
                    Books = new List<Book>
                    {
                        new Book()
                        {
                            Name = "50 Ottenkov Serogo",
                            Year = 2012,
                            Users = new List<User>
                            {
                                new User()
                                {
                                    FirstName = "Ilya",
                                    LastName = "Ivanov",
                                    Email = "ilya.ivanov@gmail.com",
                                    BirthDate = new DateTimeOffset(new DateTime(1998, 12, 12))
                                }
                            }
                        },
                    }
                },

                new Author()
                {
                    FirstName = "Dmitriy",
                    LastName = "Akulov",
                    Country = "Russia",
                    BirthDate = new DateTimeOffset(new DateTime(1995, 11, 20)),
                    Books = new List<Book>
                    {
                        new Book()
                        {
                            Name = "Oskolki Pamyatsi",
                            Year = 2021,
                            Users = new List<User>
                            {
                                new User()
                                {
                                    FirstName = "Maksim",
                                    LastName = "Piatrou",
                                    Email = "maks.piatrou@mail.ru",
                                    BirthDate = new DateTimeOffset(new DateTime(1988, 7, 1))
                                }
                            }
                        }
                    },
                },

                new Author()
                {
                    FirstName = "Anastasiya",
                    LastName = "Akulova",
                    Country = "Russia",
                    BirthDate = new DateTimeOffset(new DateTime(1990, 11, 20)),
                    Books = new List<Book>
                    {
                            new Book()
                            {
                            Name = "Chaika",
                            Year = 2012,
                            Users = new List<User>
                            {
                                new User()
                                {
                                    FirstName = "Harry",
                                    LastName = "Minov",
                                    Email = "har.minov@gmail.com",
                                    BirthDate = new DateTimeOffset(new DateTime(2000, 8, 1))
                                }
                            }
                            }
                    }
                }
            };

        await db.AddRangeAsync(addedData);
        await db.SaveChangesAsync();

        //await db.Users.AddRangeAsync
        //    (new User()
        //{
        //    FirstName = "Kilana",
        //    LastName = "Makira",
        //    Email = "kil.makira@gmail.com",
        //    BirthDate = new DateTimeOffset(new DateTime(1996, 8, 1))
        //});

        //await db.Books.AddRangeAsync
        //    (new Book()
        //    {
        //        Name = "Marshall",
        //        Year = 1968,
        //        Author = new Author()
        //        {
        //            FirstName = "Sergey",
        //            LastName = "Gagarin",
        //            Country = "Ukraine",
        //            BirthDate = new DateTimeOffset(new DateTime(1932, 1, 19))
        //        },

        await db.SaveChangesAsync();

        return RedirectToAction("GetUsersInfo", "Home");
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersInfo()
    {
        using var db = new MyDbContext();

        var joined = await db.Users.Join(db.Books, u => u.UserId, b => b.BookId,
            (u, b) => new UserInfoModel()
            {
                //почему не выводятся те юзеры у которых нет книг?
                //birthdate если ставить nullable, выскакивает эррор
                UserFullName = string.Concat(u.FirstName," ", u.LastName),
                UserBirthDate = u.BirthDate,
                BookName = b.Name,
                BookYear = b.Year,
                AuthorFullName = String.Concat(b.Author.FirstName," ", b.Author.LastName)
            })
            .ToListAsync();

        //is it necessary to use select method additionally and
        //output selected as well with select method?
            await db.SaveChangesAsync();

        return View(joined);
    }

    public async Task<IActionResult> DeleteUser()
    {
        using var db = new MyDbContext();
        var removedUsers = db.Users.Where(q => q.UserBooks.Any() == false)
            .ToList();

        //выведет ли после удаления? - yep, the data is preserved in the db
        var selected = removedUsers.Select(q => new
        {
            FullName = string.Concat(q.FirstName, " ", q.LastName)
        }).ToList();

        if (removedUsers.Any())
        {
            db.RemoveRange(removedUsers);
            await db.SaveChangesAsync();

            foreach (var user in selected)
            {
                Console.WriteLine($"User {user.FullName} has been removed");
                ViewBag.UserFullName = user.FullName;
                //как красиво передать анонимный объект во вью?
                // у меня некрасиво
            }
        }

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

        var removedUsers = db.Users.Where(q => q.UserId == index).ToList();
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

        var books = db.Books.FirstOrDefault(q => q.Name == bookName);
        var user = db.Users.SingleOrDefault(q => q.Email == userEmail);

        if (books != null || user != null)
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

        if (books != null || user != null)
        {
            var userResult = await db.UserBooks
                .SingleOrDefaultAsync(q => q.UserId == user.UserId);
            var bookResult = await db.UserBooks
                .FirstOrDefaultAsync(q => q.BookId == books.BookId);

                db.UserBooks.Remove(userResult);
                db.UserBooks.Remove(bookResult);

            await db.SaveChangesAsync();
        }

         else
         {          
                throw new ObjectNotFoundException("There are no such users or books");
         }

        return RedirectToAction("GetUsersInfo", "Home");
    }
}
