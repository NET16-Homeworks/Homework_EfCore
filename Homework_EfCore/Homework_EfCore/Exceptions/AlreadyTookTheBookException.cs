using Homework_EfCore.Models;

namespace Homework_EfCore.Exceptions
{
    public class AlreadyTookTheBookException : Exception
    {
        public AlreadyTookTheBookException(string email, string book) : base($"User {email} already took this book {book}")
        {

        }
    }
}
