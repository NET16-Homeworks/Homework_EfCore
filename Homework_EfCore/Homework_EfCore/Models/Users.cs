namespace Homework_EfCore.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<UserBooks> UserBooks { get; set; }
        public ICollection<Books> Books { get; set; }
    }
}
