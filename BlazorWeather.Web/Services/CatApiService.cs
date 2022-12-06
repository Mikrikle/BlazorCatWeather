using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace BlazorWeather.Web.Services
{
    public class CatApiService : ICatApiService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        private const string CatKey = "Key_CatApi_Cat";
        private const string UpdateTimeKey = "Key_CatApi_UpdateTime";

        public CatApiService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }

        public async Task<ResponseOrError<CatApiImageDto>> GetImage()
        {
            var response = await localStorageService.GetItemAsync<CatApiImageDto>(CatKey);
            var updated = await localStorageService.GetItemAsync<DateTime>(UpdateTimeKey);
            if (response == null || (DateTime.Now.ToUniversalTime() - updated).Hours > 1)
            {
                var http_response = await httpClient.GetAsync("https://api.thecatapi.com/v1/images/search");
                if (!http_response.IsSuccessStatusCode)
                    return new(null, http_response.StatusCode, "CatApi:Error");

                response = (await http_response.Content.ReadFromJsonAsync<CatApiImageDto[]>())?.FirstOrDefault();
                await localStorageService.SetItemAsync(CatKey, response);
                await localStorageService.SetItemAsync(UpdateTimeKey, DateTime.Now.ToUniversalTime());
            }
            return new(response);
        }
    }
}
