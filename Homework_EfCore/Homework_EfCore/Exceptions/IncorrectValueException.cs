namespace Homework_EfCore.Exceptions
{
    public class IncorrectValueException : Exception
    {
        public IncorrectValueException(string value) : base($"Incorrect value - {value}")
        {

        }
    }
}
