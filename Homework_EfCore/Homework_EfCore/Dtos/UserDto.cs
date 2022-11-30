using Homework_EfCore.Entities;

namespace Homework_EfCore.Dtos
{
    public class UserDto
    {
        public UserDto(User user)
        {
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            BirthDate = user.BirthDate;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}