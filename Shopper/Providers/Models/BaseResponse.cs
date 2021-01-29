using System.Net;

namespace Shopper.Providers.Models
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
