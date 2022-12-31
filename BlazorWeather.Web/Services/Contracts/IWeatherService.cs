using BlazorWeather.Web.Exceptions;
using BlazorWeather.Web.Dtos;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface IWeatherService
    {
        public Task SetAppId(string appId);
        public Task<string> GetAppId();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        /// <exception cref="ServiceResponseException"></exception>
        public Task<GeocodingDto> SearchCity(string city);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="city"></param>
        /// <returns></returns>
        /// <exception cref="ServiceResponseException"></exception>
        public Task<GeocodingDto[]> SearchCities(string city);

        public Task SetUserCity(GeocodingDto city);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ServiceResponseException"></exception>
        public Task<GeocodingDto> GetUserCity();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        /// <exception cref="ServiceResponseException"></exception>
        public Task<WeatherCurrentDto> GetWeather(double lat, double lon);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ServiceResponseException"></exception>
        public Task<WeatherCurrentDto> GetWeather();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        /// <exception cref="ServiceResponseException"></exception>
        public Task<WeatherForecastDto> GetForecast(double lat, double lon);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ServiceResponseException"></exception>
        public Task<WeatherForecastDto> GetForecast();
    }
}
