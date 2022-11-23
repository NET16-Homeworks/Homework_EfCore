namespace Homework_EfCore.Database.Entities
{
    public class Book
    {
        public int BookId { get; set; }

        public int AuthorId { get; set; }

        public string Name { get; set; } 
        
        public int Year { get; set; }
     
        public Author Author { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<UserBook> UserBooks { get; set; }
    }
}
