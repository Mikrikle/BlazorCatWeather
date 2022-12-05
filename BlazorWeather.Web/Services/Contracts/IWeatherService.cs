using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface IWeatherService
    {
        public Task SetAppId(string appId);
        public Task<string> GetAppId();

        public Task<ResponseOrError<GeocodingDto>> GetCityLocation(string city);
        public Task<ResponseOrError<GeocodingDto>> GetCityLocation();

        public Task SetCurrentCity(GeocodingDto city);
        public Task SetCurrentCity(string city);

        public Task<ResponseOrError<WeatherCurrentDto>> GetWeather(double lat, double lon);
        public Task<ResponseOrError<WeatherCurrentDto>> GetWeather(string city);
        public Task<ResponseOrError<WeatherCurrentDto>> GetWeather();

        public Task<ResponseOrError<WeatherForecastDto>> GetForecast(double lat, double lon);
        public Task<ResponseOrError<WeatherForecastDto>> GetForecast(string city);
        public Task<ResponseOrError<WeatherForecastDto>> GetForecast();
    }
}
