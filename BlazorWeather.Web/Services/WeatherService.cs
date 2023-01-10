using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Services.Contracts;
using BlazorWeather.Web.Utilites;
using System.Net.Http.Json;
using System.Net;
using BlazorWeather.Web.Exceptions;
using System.Globalization;

namespace BlazorWeather.Web.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IHttpDtoService httpDtoService;

        private const string currentWeatherKey = "Key_Weather_CurrentWeather";
        private const string forecastWeatherKey = "Key_Weather_ForecastWeather";
        private const string currentCityKey = "Key_Weather_CurrentCity";
        private const string appIdKey = "Key_Weather_AppId";

        private string Lang = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        private async Task<string> getAppId() => await localStorageService.GetItemAsync<string>(appIdKey);

        public WeatherService(ILocalStorageService localStorageService, IHttpDtoService httpDtoService)
        {
            this.localStorageService = localStorageService;
            this.httpDtoService = httpDtoService;
        }

        public async Task SetAppId(string appId)
        {
            await localStorageService.SetItemAsync(appIdKey, appId);
        }

        public async Task<string> GetAppId()
        {
            return await localStorageService.GetItemAsync<string>(appIdKey);
        }

        public async Task<GeocodingDto> SearchCity(string city)
        {
            var response = (await httpDtoService.GetAsync<GeocodingDto[]>(
                $"https://api.openweathermap.org/geo/1.0/direct?q={city}&limit=1&appid={await getAppId()}"))
                .FirstOrDefault();
            if (response == null)
                throw new ServiceResponseException("Not Found", HttpStatusCode.NotFound);
            return response;
        }

        public async Task<GeocodingDto[]> SearchCities(string city)
        {
            var response = await httpDtoService.GetAsync<GeocodingDto[]>(
                $"https://api.openweathermap.org/geo/1.0/direct?q={city}&limit=5&appid={await getAppId()}");
            if (response == null)
                throw new ServiceResponseException("Not Found", HttpStatusCode.NotFound);
            return response;
        }

        public async Task SetUserCity(GeocodingDto city)
        {
            await localStorageService.SetItemAsync(currentCityKey, city);
        }

        public async Task<GeocodingDto> GetUserCity()
        {
            var response = await localStorageService.GetItemAsync<GeocodingDto>(currentCityKey);
            if (response == null)
                throw new ServiceResponseException("Not Found", HttpStatusCode.NotFound);
            return response;
        }


        public async Task<WeatherCurrentDto> GetWeather(double lat, double lon)
        {
            var response = await localStorageService.GetItemAsync<WeatherCurrentDto>(currentWeatherKey);
            Coord response_coord = response?.Coord ?? new Coord();
            long DtNow = DateConverter.DateTimeToUnixTime(DateTime.Now.ToUniversalTime());
            if (response == null
                || ((int)response_coord.Lat) != (int)lat || (int)response_coord.Lon != (int)lon
                || DtNow - (response.Dt) > (15 * 60))
            {
                response = await httpDtoService.GetAsync<WeatherCurrentDto>(
                $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&units=metric&lang={Lang}&appid={await getAppId()}");
                await localStorageService.SetItemAsync(currentWeatherKey, response);
            }
            return response;
        }
        public async Task<WeatherCurrentDto> GetWeather()
        {
            try
            {
                var geo = await GetUserCity();
                return await GetWeather(geo.Lat, geo.Lon);
            }
            catch
            {
                return await GetWeather(0, 0);
            }
        }


        public async Task<WeatherForecastDto> GetForecast(double lat, double lon)
        {
            var response = await localStorageService.GetItemAsync<WeatherForecastDto>(forecastWeatherKey);
            Coord response_coord = response?.City?.Coord ?? new Coord();
            long DtNow = DateConverter.DateTimeToUnixTime(DateTime.Now.ToUniversalTime());
            if (response == null
                || (int)response_coord.Lat != (int)lat || (int)response_coord.Lon != (int)lon
                || DtNow - (response.WeatherList.FirstOrDefault()?.Dt ?? 0) > (60 * 60))
            {
                response = await httpDtoService.GetAsync<WeatherForecastDto>(
                $"https://api.openweathermap.org/data/2.5/forecast?lat={lat}&lon={lon}&units=metric&lang={Lang}&appid={await getAppId()}");
                await localStorageService.SetItemAsync(forecastWeatherKey, response);

            }
            return response;
        }
        public async Task<WeatherForecastDto> GetForecast()
        {
            try
            {
                var geo = await GetUserCity();
                return await GetForecast(geo.Lat, geo.Lon);
            }
            catch
            {
                return await GetForecast(0, 0);
            }
        }

    }
}
