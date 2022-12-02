using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface IWeatherService
    {
        public Task SetAppId(string appId);
        public Task<string> GetAppId();
        public Task<GeocodingDto?> GetCityLocation(string city);
        public Task<GeocodingDto?> GetCityLocation();
        public Task SetCurrentCity(GeocodingDto city);
        public Task SetCurrentCity(string city);
        public Task<WeatherCurrentDto?> GetWeather(double lat, double lon);
        public Task<WeatherCurrentDto?> GetWeather(string city);
        public Task<WeatherCurrentDto?> GetWeather();
        public Task<WeatherForecastDto?> GetForecast(double lat, double lon);
        public Task<WeatherForecastDto?> GetForecast(string city);
        public Task<WeatherForecastDto?> GetForecast();
    }
}
