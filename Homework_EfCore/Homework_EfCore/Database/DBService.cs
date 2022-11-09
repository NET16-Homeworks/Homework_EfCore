using Homework_EfCore.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Homework_EfCore.Database
{
    public static class DBService
    {
        public delegate void DBInfo(string info);
        public static DBInfo info;

        public async static Task AddUser(string firstName, string lastName, string email, DateTime birthDate)
        {
            using (MyDBContext db = new MyDBContext())
            {

                await db.Users.AddAsync(new User()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    BirthDate = birthDate
                });
                await db.SaveChangesAsync();
                info?.Invoke($"Added new user {email} into table Users");
            }
        }

        public async static Task AddAuthor(string firstName, string lastName, string country, DateTime birthDate)
        {
            using (MyDBContext db = new MyDBContext())
            {
                await db.Authors.AddAsync(new Author()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Country = country,
                    BirthDate = birthDate
                });
                await db.SaveChangesAsync();
                info?.Invoke($"Added new author {firstName} {lastName} into table Authors");
            }
        }

        public async static Task AddBook(string name, int year, string authorLastName)
        {
            using (MyDBContext db = new MyDBContext())
            {
                var author = await _ReturnAuthorByName(authorLastName);
                if (author != null)
                {

                    await db.Books.AddAsync(new Book()
                    {
                        Author = author,
                        Name = name,
                        Year = year
                    });
                    info?.Invoke($"Added new book {name} into table Books");
                }
                await db.SaveChangesAsync();
            }
        }

        private static async Task<Author> _ReturnAuthorByName(string lastName)
        {
            using (MyDBContext db = new MyDBContext())
            {
                return await db.Authors.AsNoTracking().SingleOrDefaultAsync(q => q.LastName == lastName);
            }
        }

        public static async Task AddData()
        {
            await DBService.AddUser("Ilya", "Maximov", "IlyaMaximov@Email.com", new DateTime(2002, 11, 21));
            await DBService.AddUser("Andrei", "Polevoi", "ANDRUHA@Email.com", new DateTime(2004, 05, 05));
            await DBService.AddUser("Vlad", "Pulyak", "VladPulyak@Email.com", new DateTime(2003, 01, 25));
            await DBService.AddAuthor("Alexander", "Pushkin", "Russia", new DateTime(1799, 05, 26));
            await DBService.AddAuthor("Nikolaj", "Gogol", "Ukraine", new DateTime(1809, 03, 25));
            await DBService.AddAuthor("Lev", "Tolstoi", "Russia", new DateTime(1828, 09, 09));
            await DBService.AddBook("Kapitanskaya dochka", 1836, "Pushkin");
            await DBService.AddBook("Skazka o tsare Saltane", 1831, "Pushkin");
            await DBService.AddBook("Vij", 1833, "Gogol");
            await DBService.AddBook("Mertvie dushi", 1842, "Gogol");
            await DBService.AddBook("Mumu", 1854, "Tolstoi");
            await DBService.GiveBookToUser("IlyaMaximov@Email.com", "Kapitanskaya dochka");

        }

        public static async Task GiveBookToUser(string userEmail, string bookName)
        {
            using (MyDBContext db = new MyDBContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(q => q.Email == userEmail);
                var book = await db.Books.SingleOrDefaultAsync(q => q.Name == bookName);

                if (!await db.UserBooks.AnyAsync(q => q.UserId == user.UserId && q.BookId == book.BookId))
                {
                    await db.UserBooks.AddAsync(new UserBooks()
                    {
                        UserId = user.UserId,
                        BookId = book.BookId,
                    });
                    await db.SaveChangesAsync();
                    info?.Invoke($"User {user.Email} took the book {book.Name}");
                }
                else
                {
                    info?.Invoke($"User {user.Email} already take this book {book.Name}");
                }
            }
        }

        public static async Task ReturnBookFromUser(string userEmail, string bookName)
        {
            using (MyDBContext db = new MyDBContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(q => q.Email == userEmail);
                var book = await db.Books.SingleOrDefaultAsync(q => q.Name == bookName);
                if (user != null && book != null)
                {
                    var data = await db.UserBooks.SingleOrDefaultAsync(q => q.UserId == user.UserId && q.BookId == book.BookId);

                    if (data != null)
                    {
                        db.UserBooks.Remove(data);
                        await db.SaveChangesAsync();
                        info?.Invoke($"User {user.Email} returned the book {book.Name}");
                    }
                    else
                    {
                        info?.Invoke($"User {user.Email} never took the book {book.Name}");
                    }
                }
                if (user == null)
                {
                    info?.Invoke($"User with email <{userEmail}> is not exist");
                }
                if (book == null)
                {
                    info?.Invoke($"Book with name <{bookName}> is not exist");
                }
            }
        }

        public static async Task RemoveUselessUsers()
        {
            using (MyDBContext db = new MyDBContext())
            {
                var users = db.Users.Where(q => !q.Books.Any());
                if (users.Any())
                {
                    string message = "Deleted users:";
                    db.RemoveRange(users);
                    foreach (var user in users)
                    {
                        message += $"\n{user.FirstName} {user.LastName}";
                    }
                    await db.SaveChangesAsync();
                    info?.Invoke(message);
                }
                else
                {
                    info?.Invoke("No users was removed");
                }
            }
        }

        public static async Task<List<BorrowedBooksViewModel>> ReturnBorrowedBooksData()
        {
            using (MyDBContext db = new MyDBContext())
            {
                List<BorrowedBooksViewModel> borrowed = new List<BorrowedBooksViewModel>();
                var userBooks = db.UserBooks.AsNoTracking().Select(q => new
                {
                    UserId = q.UserId,
                    BookId = q.BookId
                });
                foreach (var item in userBooks)
                {
                    using (MyDBContext dbforeach = new MyDBContext())
                    {
                        var user = await dbforeach.Users.AsNoTracking().SingleAsync(q => q.UserId == item.UserId);
                        var book = await dbforeach.Books.AsNoTracking().SingleAsync(q => q.BookId == item.BookId);
                        var author = await dbforeach.Authors.AsNoTracking().SingleAsync(q => q.AuthorId == book.AuthorId);
                        BorrowedBooksViewModel borrowedBooks = new BorrowedBooksViewModel()
                        {
                            UserFullName = user.FirstName + " " + user.LastName,
                            UserBirthDate = user.BirthDate,
                            AuthorFullName = author.FirstName + " " + author.LastName,
                            BookName = book.Name,
                            BookYear = book.Year
                        };
                        borrowed.Add(borrowedBooks);
                    }
                }
                return borrowed;
            }
        }
    }
}
