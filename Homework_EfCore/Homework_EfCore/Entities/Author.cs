namespace Homework_EfCore.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }

        public Book Book { get; set; }
    }
}

