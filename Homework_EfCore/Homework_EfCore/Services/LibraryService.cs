using Homework_EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Homework_EfCore.Services
{
    public static class LibraryService
    {
        public static async Task AddUser(MyDBContext context,Users user)
        {
            await context.Users.AddAsync(user);
            context.SaveChanges();
        }
        public static async Task<List<Users>> GetUsers(MyDBContext context)
        {
            var users = await context.Users.ToListAsync();
            return users;
        }
        public static async Task AddBook(MyDBContext context, Books book, string authorFirstName, string authorLastName)
        {
            var author = await context.Authors.Where(q => q.FirstName == authorFirstName && q.LastName == authorLastName).SingleOrDefaultAsync();
            book.AuthorId = author.AuthorId;
            await context.Books.AddAsync(book);
            context.SaveChanges();
        }
        public static async Task<List<Books>> GetBooks(MyDBContext context)
        {
            var books = await context.Books.ToListAsync();
            return books;
        }
        public static async Task AddAuthor(MyDBContext context,Authors author)
        {
            await context.Authors.AddAsync(author);
            context.SaveChanges();
        }
        public static async Task<List<Authors>> GetAuthors(MyDBContext context)
        {
            var authors = await context.Authors.ToListAsync();
            return authors;
        }
        public static async Task TakeBook(MyDBContext context, Books book, string userEmail, string authorFirstName, string authorLastName)
        {
            var userBooks = new UserBooks();
            userBooks.UserId = context.Users.Single(q => q.Email == userEmail).UserId;
            var authorIdOfCurrentBook = context.Authors.Single(q => (q.FirstName == authorFirstName) && (q.LastName == authorLastName)).AuthorId;
            userBooks.BookId = context.Books.Where(q => (q.Name == book.Name) && (q.AuthorId == authorIdOfCurrentBook) && (q.Year == book.Year)).Single().BookId;
            await context.UserBooks.AddAsync(userBooks);
            context.SaveChanges();
        }
        public static async Task<List<UserBooksInfo>> GetUserBooks(MyDBContext context)
        {
            var listWithUserBooksInfo = await context.UserBooks.Select(q => new UserBooksInfo
            {
                UserFullName = q.User.FirstName + " " + q.User.LastName,
                BookName = q.Book.Name,
                UserBirthDate = q.User.BirthDate,
                AuthorFullName = q.Book.Author.FirstName + " " + q.Book.Author.LastName,
                BookYear= q.Book.Year,
            }).ToListAsync();
            return listWithUserBooksInfo;
        }
        public static async Task<List<string>> DeleteUsersWithoutBooks(MyDBContext context)
        {
            var fullNameOFDeletedUser = new List<string>();
            var listWithUserBooks = await context.UserBooks.ToListAsync();
            var listWithUsers = await context.Users.ToListAsync();
            foreach (var user in listWithUsers)
            {
                var listWithDeletedUsers = listWithUserBooks.Where(q => q.UserId == user.UserId);
                if (listWithDeletedUsers.IsNullOrEmpty())
                {
                    fullNameOFDeletedUser.Add(user.FirstName + " " + user.LastName);
                    context.Users.Remove(user);
                }
            }
            context.SaveChanges();
            return fullNameOFDeletedUser;
        }
        public static async Task ReturnBook(MyDBContext context, string userEmail, string bookName)
        {
            var listWithUserBooks = await context.UserBooks.Include(q => q.User).Include(q => q.Book).ToListAsync();
            var deletedUser = await context.UserBooks.Where(q => q.User.Email == userEmail && q.Book.Name == bookName).SingleAsync();
            context.UserBooks.Remove(deletedUser);
            context.SaveChanges();
        }
    }
}
