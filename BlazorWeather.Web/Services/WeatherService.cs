using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Services.Contracts;
using BlazorWeather.Web.Utilites;
using System.Net.Http.Json;

namespace BlazorWeather.Web.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        private const string currentWeatherKey = "Key_Weather_currentWeather";
        private const string forecastWeatherKey = "Key_Weather_forecastWeather";

        public WeatherService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }

        //api.openweathermap.org/data/2.5/weather?q={city}&appid={API key}
        public async Task<GeocodingDto?> GetCityLocation(string city)
        {
            string cityKey = "Key_Weather_City_" + city;
            var response = await localStorageService.GetItemAsync<GeocodingDto>(cityKey);
            if (response == null)
            {
                response = (await httpClient.GetFromJsonAsync<GeocodingDto[]?>("sample-data/geocoding-api.json"))?.FirstOrDefault();
                await localStorageService.SetItemAsync(cityKey, response);
            }

            return response;
        }

        //api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&lang=ru&appid={API key}
        public async Task<WeatherCurrentDto?> GetWeather(double lat, double lon)
        {
            var response = await localStorageService.GetItemAsync<WeatherCurrentDto>(currentWeatherKey);

            if ((DateTime.Now - DateConverter.UnixTimeToLocalDateTime(response?.Dt ?? 0)).TotalMinutes > 15)
            {
                response = await httpClient.GetFromJsonAsync<WeatherCurrentDto?>("sample-data/current-weather-data.json");
                await localStorageService.SetItemAsync(currentWeatherKey, response);
            }
            return response;
        }
        public async Task<WeatherCurrentDto?> GetWeather(string city)
        {
            var geo = await GetCityLocation(city);
            return await GetWeather(geo?.Lat ?? 0, geo?.Lon ?? 0);
        }

        //api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&lang=ru&appid={API key}
        public async Task<WeatherForecastDto?> GetForecast(double lat, double lon)
        {
            var response = await localStorageService.GetItemAsync<WeatherForecastDto>(forecastWeatherKey);

            if ((DateTime.Now - DateConverter.UnixTimeToLocalDateTime(response?.List.FirstOrDefault()?.Dt ?? 0)).TotalHours > 1)
            {
                response = await httpClient.GetFromJsonAsync<WeatherForecastDto?>("sample-data/weather-forecast.json");
                await localStorageService.SetItemAsync(forecastWeatherKey, response);
            }

            return response;
        }
        public async Task<WeatherForecastDto?> GetForecast(string city)
        {
            var geo = await GetCityLocation(city);
            return await GetForecast(geo?.Lat ?? 0, geo?.Lon ?? 0);
        }

    }
}
