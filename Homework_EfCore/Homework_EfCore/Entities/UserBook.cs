namespace Homework_EfCore.Entities
{
    public class UserBook
    {
        public int UserBookId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }

        public Book Book;
        public User User;
    }
}

