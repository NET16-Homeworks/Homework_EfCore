namespace Homework_EfCore.Models
{
    public class UserInfoModel
    {
        public string UserFullName { get; set; }

        public DateTimeOffset UserBirthDate { get; set; }

        public string ?BookName { get; set; }

        public int ?BookYear { get; set; }

        public string ?AuthorFullName { get; set; }

        public override string ToString()
        {
            return $"{UserFullName}, {UserBirthDate:d}, books: {BookName} dated {BookYear}, {AuthorFullName}";
        }
    }
}
