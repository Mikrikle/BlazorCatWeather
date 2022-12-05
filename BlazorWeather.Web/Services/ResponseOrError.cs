using System.Net;

namespace BlazorWeather.Web.Services
{
    public class ResponseOrError<T> where T : class
    {
        public ResponseOrError(T? response, HttpStatusCode statusCode = HttpStatusCode.OK, string error = "")
        {
            Response = response;
            Error = error;
            StatusCode = statusCode;
        }
        public bool IsSuccess => (Response != null) && string.IsNullOrEmpty(Error);
        public T? Response { get; }
        public string Error { get; }
        public HttpStatusCode StatusCode { get; }
    }
}
