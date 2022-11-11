namespace Homework_EfCore.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public int FirstName { get; set; }
        public int LastName { get; set; }
        public int Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
