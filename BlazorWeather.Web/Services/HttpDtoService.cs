using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Exceptions;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using BlazorWeather.Web.Services.Contracts;

namespace BlazorWeather.Web.Services
{
    public class HttpDtoService : IHttpDtoService
    {

        private readonly HttpClient httpClient;

        public HttpDtoService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var http_response = await httpClient.GetAsync(uri);
                if(http_response.StatusCode == HttpStatusCode.Unauthorized)
                    throw new ServiceResponseException("Unauthorized", http_response.StatusCode);
                else if (!http_response.IsSuccessStatusCode)
                    throw new ServiceResponseException(await http_response.Content.ReadAsStringAsync(), http_response.StatusCode);
                T? result = await http_response.Content.ReadFromJsonAsync<T>();
                if (result == null)
                    throw new ServiceResponseException(await http_response.Content.ReadAsStringAsync(), http_response.StatusCode);
                return result;
            }
            catch (ServiceResponseException e)
            {
                throw new ServiceResponseException(e.Message, e.StatusCode);
            }
            catch (UriFormatException e)
            {
                throw new ServiceResponseException(e.Message, HttpStatusCode.BadRequest);
            }
            catch (Exception e)
            {
                throw new ServiceResponseException(e.Message, HttpStatusCode.InternalServerError);
            }
        }
    }
}
