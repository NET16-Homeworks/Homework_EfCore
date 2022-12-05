namespace Homework_EfCore.Exceptions
{
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException(string objName) : base($"{objName} already exists")
        {

        }
    }
}
