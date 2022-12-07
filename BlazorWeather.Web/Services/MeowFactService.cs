using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Services.Contracts;
using System.Net.Http.Json;
using System.Security.Cryptography;
using static BlazorWeather.Web.Shared.MeowFact;
using static System.Net.WebRequestMethods;

namespace BlazorWeather.Web.Services
{
    public class MeowFactService : IMeowFactService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorageService;

        private const string FactKey = "Key_MeowFact_Fact";
        private const string UpdateTimeKey = "Key_MeowFact_UpdateTime";

        private string Lang = "rus";

        public MeowFactService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this.localStorageService = localStorageService;
        }

        public async Task<ResponseOrError<MeowFactDto>> GetFact()
        {

            var response = await localStorageService.GetItemAsync<MeowFactDto>(FactKey);
            var updated = await localStorageService.GetItemAsync<DateTime>(UpdateTimeKey);
            if (response == null || (DateTime.Now.ToUniversalTime() - updated).Hours > 12)
            {
                var http_response = await httpClient.GetAsync($"https://meowfacts.herokuapp.com/?lang={Lang}");
                if (!http_response.IsSuccessStatusCode)
                    return new(null, http_response.StatusCode, "CatApi:Error");

                response = await http_response.Content.ReadFromJsonAsync<MeowFactDto>();
                await localStorageService.SetItemAsync(FactKey, response);
                await localStorageService.SetItemAsync(UpdateTimeKey, DateTime.Now.ToUniversalTime());
            }
            return new(response);
        }
    }
}
