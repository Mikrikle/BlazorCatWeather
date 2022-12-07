using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface IWeatherService
    {
        public Task SetAppId(string appId);
        public Task<string> GetAppId();

        public Task<ResponseOrError<GeocodingDto>> SearchCity(string city);

        public Task SetUserCity(GeocodingDto city);
        public Task<ResponseOrError<GeocodingDto>> GetUserCity();

        public Task<ResponseOrError<WeatherCurrentDto>> GetWeather(double lat, double lon);
        public Task<ResponseOrError<WeatherCurrentDto>> GetWeather();

        public Task<ResponseOrError<WeatherForecastDto>> GetForecast(double lat, double lon);
        public Task<ResponseOrError<WeatherForecastDto>> GetForecast();
    }
}
