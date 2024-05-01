namespace ClassLibrary.Mvc.Exceptions
{
    [Serializable]
    public class BadRequestException(string message) : System.Exception(message)
    {
    }
}
