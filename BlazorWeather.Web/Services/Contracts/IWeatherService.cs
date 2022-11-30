using BlazorWeather.Web.Dtos;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface IWeatherService
    {
        public Task<GeocodingDto?> GetCityLocation(string city);
        public Task<WeatherCurrentDto?> GetWeather(double lat, double lon);
        public Task<WeatherCurrentDto?> GetWeather(string city);
        public Task<WeatherForecastDto?> GetForecast(double lat, double lon);
        public Task<WeatherForecastDto?> GetForecast(string city);
    }
}
