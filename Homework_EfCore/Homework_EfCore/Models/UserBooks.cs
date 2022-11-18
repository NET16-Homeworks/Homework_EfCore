namespace Homework_EfCore.Models
{
    public class UserBooks
    {
        public int UserBooksId { get; set; }
        public int? UserId { get; set; }
        public int? BookId { get; set; }
        public Users User { get; set; }
        public Books Book { get; set; }
    }
}