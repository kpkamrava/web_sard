namespace web_sard.Models
{

    public class ErrorViewModel
    {

        public string RequestId { get; set; }


        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
