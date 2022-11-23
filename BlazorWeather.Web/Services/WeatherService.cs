using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Services.Contracts;
using System.Net.Http.Json;

namespace BlazorWeather.Web.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient httpClient;

        public WeatherService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<GeocodingDto?> GetCityLocation(string city)
        {
            //api.openweathermap.org/data/2.5/weather?q={city}&appid={API key}
            var response = await httpClient.GetFromJsonAsync<GeocodingDto[]?>("sample-data/geocoding-api.json");
            return response?.FirstOrDefault();
        }

        public async Task<WeatherCurrentDto?> GetWeather(double lat, double lon)
        {
            //api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&lang=ru&appid={API key}
            var response = await httpClient.GetFromJsonAsync<WeatherCurrentDto?>("sample-data/current-weather-data.json");
            return response;
        }

        public async Task<WeatherForecastDto?> GetForecast(double lat, double lon)
        {
            //api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&lang=ru&appid={API key}
            var response = await httpClient.GetFromJsonAsync<WeatherForecastDto?>("sample-data/weather-forecast.json");
            return response;
        }

    }
}
