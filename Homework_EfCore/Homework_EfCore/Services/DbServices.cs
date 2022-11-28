using Homework_EfCore.Entities;
using Homework_EfCore.Contexts;
using Homework_EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Homework_EfCore.Services
{
    public class DbServices
    {
        public async Task FillDb()
        {
            using (MyDbContext context = new())
            {
                await context.AddRangeAsync(
                new User
                {
                    FirstName = "Random User #1 FirstName",
                    LastName = "Random User #1 LastName",
                    Email = "Random User #1 Email",
                    BirthDate = new DateTime(2003, 3, 3),
                    UserBooks = new List<UserBook>() {
                        new UserBook()
                        {
                            Book = new Book()
                                {
                                Name = "Random Book #1 Name",
                                Year = 1920,
                                Author = new Author()
                                {
                                        FirstName = "Random Author #1 FirstName",
                                        LastName = "Random Author #1 LastName",
                                        Country = "Random Author #1 Country",
                                        BirthDate = new DateTime(1891, 1, 1)
                                }
                            }
                        },
                         new UserBook()
                        {
                            Book = new Book()
                            {
                                Name = "Random Book #2 Name",
                                Year = 1921,
                                Author = new Author()
                                {
                                        FirstName = "Random Author #2 FirstName",
                                        LastName = "Random Author #2 LastName",
                                        Country = "Random Author #2 Country",
                                        BirthDate = new DateTime(1890, 2, 2)
                                }
                            }
                        },
                    },
                },
                new User
                {
                    FirstName = "Random User #2 FirstName",
                    LastName = "Random User #2 LastName",
                    Email = "Random User #2 Email",
                    BirthDate = new DateTime(1999, 5, 5),
                    UserBooks = new List<UserBook>() {
                        new UserBook()
                        {
                            Book = new Book()
                            {
                                Name = "Random Book #3 Name",
                                Year = 1923,
                                Author = new Author()
                                {
                                        FirstName = "Random Author #3 FirstName",
                                        LastName = "Random Author #3 LastName",
                                        Country = "Random Author #3 Country",
                                        BirthDate = new DateTime(1888, 3, 3)
                                }
                            }
                        },
                         new UserBook()
                        {
                            Book =  new Book()
                            {
                                Name = "Random Book #4 Name",
                                Year = 1911,
                                Author = new Author()
                                {
                                        FirstName = "Random Author #4 FirstName",
                                        LastName = "Random Author #4 LastName",
                                        Country = "Random Author #4 Country",
                                        BirthDate = new DateTime(1890, 4, 4)
                                }
                            }
                        },
                    }
                },
                new User()
                {
                    FirstName = "Random User #3 FirstName",
                    LastName = "Random User #3 LastName",
                    BirthDate = new DateTime(2000, 1, 2),
                    Email = "Random User #3 Email",
                },
                new User()
                {
                    FirstName = "Random User #4 FirstName",
                    LastName = "Random User #4 LastName",
                    BirthDate = new DateTime(1989, 3, 5),
                    Email = "Random User #4 Email",
                }
                );
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<UserBookViewModel>> GetUsersBooksList()
        {
            using (MyDbContext context = new())
            {
                return await context.UserBooks.AsNoTracking().Select(userBook => new UserBookViewModel()
                {
                    UserFullName = String.Concat(userBook.User.FirstName, " ", userBook.User.LastName),
                    UserBirthDate = userBook.User.BirthDate,
                    BookName = userBook.Book.Name,
                    AuthorFullName = String.Concat(userBook.Book.Author.FirstName, " ", userBook.Book.Author.LastName),
                    BookYear = userBook.Book.Year
                }).ToListAsync();
            }
        }

        public async Task<List<UserViewModel>> RemoveUsersWithoutBooks()
        {
            List<UserViewModel> removedUsersList = new();

            using (MyDbContext context = new())
            {	
                var usersWithoutBooks = await context.Users.Where(user => user.UserBooks.Count == 0).ToListAsync();

                removedUsersList = usersWithoutBooks.Select(user => new UserViewModel()
                {
                    FullName = String.Concat(user.FirstName, " ", user.LastName)
                }).ToList();

                context.RemoveRange(usersWithoutBooks);

                await context.SaveChangesAsync();
            }
            
            return removedUsersList;
        }

        public async Task ReturnBook(UserBookReturnViewModel returnedBook)
        {
            using (MyDbContext context = new())
            {
                var book = await context.UserBooks.Where(userBook => userBook.User.Email == returnedBook.UserEmail && userBook.Book.Name == returnedBook.BookName).FirstOrDefaultAsync();
                
                if(book != null)
                {
                    context.UserBooks.Remove(book);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
