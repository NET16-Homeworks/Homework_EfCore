namespace Homework_EfCore.Database.Entities
{
    public class UserBook
    {
        public int UserId { get; set; }
        public int BookId { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
    }
}
