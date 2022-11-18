namespace Homework_EfCore.Models
{
    public class Authors
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Books> Books { get; set; }
    }
}
