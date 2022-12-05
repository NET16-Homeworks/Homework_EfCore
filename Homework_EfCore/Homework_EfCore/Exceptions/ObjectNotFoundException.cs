namespace Homework_EfCore.Exceptions
{
    public class ObjectNotFoundException : Exception
    {
        public ObjectNotFoundException(string objName) : base($"{objName} not found qwe")
        {

        }
    }
}
