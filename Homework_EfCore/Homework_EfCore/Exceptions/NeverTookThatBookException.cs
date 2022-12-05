using Homework_EfCore.Models;

namespace Homework_EfCore.Exceptions
{
    public class NeverTookThatBookException : Exception
    {
        public NeverTookThatBookException(string user,string book) : base($"User {user} never took this book {book}")
        {

        }
    }
}
