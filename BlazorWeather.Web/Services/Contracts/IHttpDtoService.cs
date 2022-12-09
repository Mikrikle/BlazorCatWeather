using BlazorWeather.Web.Exceptions;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface IHttpDtoService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        /// <exception cref="ServiceResponseException"></exception>
        public Task<T> GetAsync<T>(string uri);
    }
}
