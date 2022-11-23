using BlazorWeather.Web.Dtos;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface IWeatherService
    {
        public Task<GeocodingDto?> GetCityLocation(string city);
        public Task<CurrentWeatherDto?> GetWeather(double lat, double lon);
    }
}
