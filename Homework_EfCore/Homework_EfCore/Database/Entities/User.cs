namespace Homework_EfCore.Database.Entities
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public DateTimeOffset BirthDate { get; set; }

        public ICollection<Book> Books { get; set; }

        public ICollection<UserBook> UserBooks { get; set; }
    }
}
