namespace Homework_EfCore.Models
{
    public class Books
    {
        public int BookId { get; set; }
        public int? AuthorId { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public Authors Author { get; set; }
        public ICollection<UserBooks> UserBooks { get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
