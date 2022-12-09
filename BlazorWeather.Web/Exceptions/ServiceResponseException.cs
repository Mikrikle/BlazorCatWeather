using System.Net;

namespace BlazorWeather.Web.Exceptions
{
    public class ServiceResponseException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public ServiceResponseException(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
