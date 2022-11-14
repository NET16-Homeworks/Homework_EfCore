using Homework_EfCore.Models;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Homework_EfCore.Database
{
    public class DBService
    {
        public Action<string> info;

        public async Task AddUser(string firstName, string lastName, string email, DateTime birthDate)
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

        public async Task AddAuthor(string firstName, string lastName, string country, DateTime birthDate)
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

        public async Task AddBook(string name, int year, string authorLastName)
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

        private async Task<Author> _ReturnAuthorByName(string lastName)
        {
            using (MyDBContext db = new MyDBContext())
            {
                return await db.Authors.AsNoTracking().SingleOrDefaultAsync(q => q.LastName == lastName);
            }
        }

        public async Task AddData()
        {
            await AddUser("Ilya", "Maximov", "IlyaMaximov@Email.com", new DateTime(2002, 11, 21));
            await AddUser("Andrei", "Polevoi", "ANDRUHA@Email.com", new DateTime(2004, 05, 05));
            await AddUser("Vlad", "Pulyak", "VladPulyak@Email.com", new DateTime(2003, 01, 25));
            await AddAuthor("Alexander", "Pushkin", "Russia", new DateTime(1799, 05, 26));
            await AddAuthor("Nikolaj", "Gogol", "Ukraine", new DateTime(1809, 03, 25));
            await AddAuthor("Lev", "Tolstoi", "Russia", new DateTime(1828, 09, 09));
            await AddBook("Kapitanskaya dochka", 1836, "Pushkin");
            await AddBook("Skazka o tsare Saltane", 1831, "Pushkin");
            await AddBook("Vij", 1833, "Gogol");
            await AddBook("Mertvie dushi", 1842, "Gogol");
            await AddBook("Mumu", 1854, "Tolstoi");
            await GiveBookToUser("IlyaMaximov@Email.com", "Kapitanskaya dochka");

        }

        public async Task GiveBookToUser(string userEmail, string bookName)
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

        public async Task ReturnBookFromUser(string userEmail, string bookName)
        {
            using (MyDBContext db = new MyDBContext())
            {
                var user = await db.Users.SingleOrDefaultAsync(q => q.Email == userEmail);
                var book = await db.Books.SingleOrDefaultAsync(q => q.Name == bookName);

                if (user == null)
                {
                    info?.Invoke($"User with email <{userEmail}> is not exist");
                    return;
                }
                if (book == null)
                {
                    info?.Invoke($"Book with name <{bookName}> is not exist");
                    return;
                }

                var data = await db.UserBooks.SingleOrDefaultAsync(q => q.UserId == user.UserId && q.BookId == book.BookId);

                if (data == null)
                {
                    info?.Invoke($"User {user.Email} never took the book {book.Name}");
                    return;
                }

                db.UserBooks.Remove(data);
                await db.SaveChangesAsync();
                info?.Invoke($"User {user.Email} returned the book {book.Name}");
            }
        }

        public async Task RemoveUselessUsers()
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

        public async Task<List<BorrowedBooksViewModel>> ReturnBorrowedBooksData()
        {
            using (MyDBContext db = new MyDBContext())
            {
                return await db.UserBooks.Select(row => new BorrowedBooksViewModel
                {
                    UserFullName = row.User.FirstName + " " + row.User.LastName,
                    UserBirthDate = row.User.BirthDate,
                    AuthorFullName = row.Book.Author.FirstName + " " + row.Book.Author.LastName,
                    BookName = row.Book.Name,
                    BookYear = row.Book.Year
                }).ToListAsync();
            }
        }
    }
}