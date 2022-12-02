using System.Globalization;
using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Services.Contracts;
using BlazorWeather.Web.Utilites;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace BlazorWeather.Web.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        private const string currentWeatherKey = "Key_Weather_CurrentWeather";
        private const string forecastWeatherKey = "Key_Weather_ForecastWeather";
        private const string currentCityKey = "Key_Weather_CurrentCity";
        private const string cityKeyPartial = "Key_Weather_City_";
        private const string appIdKey = "Key_Weather_AppId";

        private string Lang => CultureInfo.CurrentCulture.Parent.ToString();
        private async Task<string> getAppId() => await localStorageService.GetItemAsync<string>(appIdKey);

        public WeatherService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }

        public async Task SetAppId(string appId)
        {
            await localStorageService.SetItemAsync(appIdKey, appId);
        }

        public async Task<string> GetAppId()
        {
            return await localStorageService.GetItemAsync<string>(appIdKey);
        }

        public async Task<GeocodingDto?> GetCityLocation(string city)
        {
            string cityKey = cityKeyPartial + city;
            var response = await localStorageService.GetItemAsync<GeocodingDto>(cityKey);
            if (response == null)
            {
                var http_response = await httpClient.GetAsync(
                    $"https://api.openweathermap.org/geo/1.0/direct?q={city}&&limit=1&appid={await getAppId()}"
                    );
                if (http_response.IsSuccessStatusCode)
                {
                    response = (await http_response.Content.ReadFromJsonAsync<GeocodingDto[]>())?
                        .FirstOrDefault();
                    await localStorageService.SetItemAsync(cityKey, response);
                }
            }

            return response;
        }
        public async Task<GeocodingDto?> GetCityLocation()
        {
            var response = await localStorageService.GetItemAsync<GeocodingDto>(currentCityKey);
            if (response == null)
            {
                response = await GetCityLocation("Moscow");
                await localStorageService.SetItemAsync(currentCityKey, response);
            }
            return response;
        }

        public async Task SetCurrentCity(GeocodingDto city)
        {
            await localStorageService.SetItemAsync(currentCityKey, city);
        }
        public async Task SetCurrentCity(string city)
        {
            await localStorageService.SetItemAsync(currentCityKey, await GetCityLocation(city));
        }

        public async Task<WeatherCurrentDto?> GetWeather(double lat, double lon)
        {
            var response = await localStorageService.GetItemAsync<WeatherCurrentDto>(currentWeatherKey);
            Console.WriteLine("API WEATHER: get local");
            long DtNow = DateConverter.DateTimeToUnixTime(DateTime.Now.ToUniversalTime());
            if (DtNow - (response?.Dt ?? 0) > (15 * 60))
            {
                var http_response = await httpClient.GetAsync(
                    $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&lang={Lang}&appid={await getAppId()}"
                    );
                if (http_response.IsSuccessStatusCode)
                {
                    Console.WriteLine("API WEATHER: update");
                    response = await http_response.Content.ReadFromJsonAsync<WeatherCurrentDto>();
                    await localStorageService.SetItemAsync(currentWeatherKey, response);
                }
                else
                {
                    Console.WriteLine($"API WEATHER: code {http_response.StatusCode}");
                }
            }
            return response;
        }
        public async Task<WeatherCurrentDto?> GetWeather(string city)
        {
            var geo = await GetCityLocation(city);
            return await GetWeather(geo?.Lat ?? 0, geo?.Lon ?? 0);
        }
        public async Task<WeatherCurrentDto?> GetWeather()
        {
            var geo = await GetCityLocation();
            return await GetWeather(geo?.Lat ?? 0, geo?.Lon ?? 0);
        }


        public async Task<WeatherForecastDto?> GetForecast(double lat, double lon)
        {
            var response = await localStorageService.GetItemAsync<WeatherForecastDto>(forecastWeatherKey);
            Console.WriteLine("API FORECAST: get local");
            long DtNow = DateConverter.DateTimeToUnixTime(DateTime.Now.ToUniversalTime());
            if (DtNow - (response?.List.FirstOrDefault()?.Dt ?? 0) > (60 * 60))
            {
                var http_response = await httpClient.GetAsync(
                    $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&lang={Lang}&appid={await getAppId()}"
                    );
                if (http_response.IsSuccessStatusCode)
                {
                    Console.WriteLine("API FORECAST: update");
                    response = await http_response.Content.ReadFromJsonAsync<WeatherForecastDto>();
                    await localStorageService.SetItemAsync(forecastWeatherKey, response);
                }
                else
                {
                    Console.WriteLine($"API FORECAST: code {http_response.StatusCode}");
                }
            }

            return response;
        }
        public async Task<WeatherForecastDto?> GetForecast(string city)
        {
            var geo = await GetCityLocation(city);
            return await GetForecast(geo?.Lat ?? 0, geo?.Lon ?? 0);
        }
        public async Task<WeatherForecastDto?> GetForecast()
        {
            var geo = await GetCityLocation();
            return await GetForecast(geo?.Lat ?? 0, geo?.Lon ?? 0);
        }

    }
}
