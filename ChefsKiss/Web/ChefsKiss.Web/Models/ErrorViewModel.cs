namespace ChefsKiss.Web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public int StatusCode { get; set; }

        public string Message { get; set; } = "An error occurred while processing your request";

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}
