using Homework_EfCore.Entities;

namespace Homework_EfCore.Extentions
{
    public static class UserExtentions
    {
        public static void Update(this User user, User model)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.BirthDate = model.BirthDate;
        }
    }
}
