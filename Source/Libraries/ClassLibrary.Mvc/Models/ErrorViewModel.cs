namespace ClassLibrary.Mvc.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; } = string.Empty;
        public int StatusCode { get; set; } = 0;
        public string StatusMessage { get; set; } = string.Empty;
        public System.Exception? Exception { get; set; }
    }
}
