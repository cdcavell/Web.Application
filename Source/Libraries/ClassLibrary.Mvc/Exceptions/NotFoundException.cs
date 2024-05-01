namespace ClassLibrary.Mvc.Exceptions
{
    [Serializable]
    public class NotFoundException(string message) : System.Exception(message)
    {
    }
}
