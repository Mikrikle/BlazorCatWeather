using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Services.Contracts;
using BlazorWeather.Web.Utilites;
using System.Net.Http.Json;
using System.Net;

namespace BlazorWeather.Web.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        private const string currentWeatherKey = "Key_Weather_CurrentWeather";
        private const string forecastWeatherKey = "Key_Weather_ForecastWeather";
        private const string currentCityKey = "Key_Weather_CurrentCity";
        private const string appIdKey = "Key_Weather_AppId";

        private string Lang = "ru";
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

        public async Task<ResponseOrError<GeocodingDto>> SearchCity(string city)
        {
            var http_response = await httpClient.GetAsync(
                    $"https://api.openweathermap.org/geo/1.0/direct?q={city}&&limit=1&appid={await getAppId()}"
                    );
            if (!http_response.IsSuccessStatusCode)
                return new(null, http_response.StatusCode, "Get city: Error");
            var response = (await http_response.Content.ReadFromJsonAsync<GeocodingDto[]>())?
                .FirstOrDefault();
            return new(response);
        }

        public async Task SetUserCity(GeocodingDto city)
        {
            await localStorageService.SetItemAsync(currentCityKey, city);
        }

        public async Task<ResponseOrError<GeocodingDto>> GetUserCity()
        {
            var response = await localStorageService.GetItemAsync<GeocodingDto>(currentCityKey);
            if (response == null)
            {
                return new(null, HttpStatusCode.BadRequest, "City not selected");
            }
            return new(response);
        }


        public async Task<ResponseOrError<WeatherCurrentDto>> GetWeather(double lat, double lon)
        {
            var response = await localStorageService.GetItemAsync<WeatherCurrentDto>(currentWeatherKey);
            Console.WriteLine("API WEATHER: get local");
            Coord response_coord = response?.Coord ?? new Coord();
            long DtNow = DateConverter.DateTimeToUnixTime(DateTime.Now.ToUniversalTime());
            if (((int)response_coord.Lat) != (int)lat || (int)response_coord.Lon != (int)lon || DtNow - (response?.Dt ?? 0) > (15 * 60))
            {
                var http_response = await httpClient.GetAsync(
                    $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&lang={Lang}&appid={await getAppId()}"
                    );
                if (!http_response.IsSuccessStatusCode)
                    return new(null, http_response.StatusCode, "Get Waether: Invalid API key");
                Console.WriteLine("API WEATHER: update");
                response = await http_response.Content.ReadFromJsonAsync<WeatherCurrentDto>();
                await localStorageService.SetItemAsync(currentWeatherKey, response);

            }
            return new(response);
        }
        public async Task<ResponseOrError<WeatherCurrentDto>> GetWeather()
        {
            var geo = (await GetUserCity()).Response;
            return await GetWeather(geo?.Lat ?? 0, geo?.Lon ?? 0);
        }


        public async Task<ResponseOrError<WeatherForecastDto>> GetForecast(double lat, double lon)
        {
            var response = await localStorageService.GetItemAsync<WeatherForecastDto>(forecastWeatherKey);
            Console.WriteLine("API FORECAST: get local");
            Coord response_coord = response?.City?.Coord ?? new Coord();
            long DtNow = DateConverter.DateTimeToUnixTime(DateTime.Now.ToUniversalTime());
            if ((int)response_coord.Lat != (int)lat || (int)response_coord.Lon != (int)lon || DtNow - (response?.WeatherList.FirstOrDefault()?.Dt ?? 0) > (60 * 60))
            {
                var http_response = await httpClient.GetAsync(
                    $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&lang={Lang}&appid={await getAppId()}"
                    );
                if (!http_response.IsSuccessStatusCode)
                    return new(null, http_response.StatusCode, "Get Forecast: Invalid API key");
                Console.WriteLine("API FORECAST: update");
                response = await http_response.Content.ReadFromJsonAsync<WeatherForecastDto>();
                await localStorageService.SetItemAsync(forecastWeatherKey, response);
            }

            return new(response);
        }
        public async Task<ResponseOrError<WeatherForecastDto>> GetForecast()
        {
            var geo = (await GetUserCity()).Response;
            return await GetForecast(geo?.Lat ?? 0, geo?.Lon ?? 0);
        }

    }
}
