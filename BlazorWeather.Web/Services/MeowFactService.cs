using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Services.Contracts;
using System.Net.Http.Json;

namespace BlazorWeather.Web.Services
{
    public class MeowFactService : IMeowFactService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IHttpDtoService httpDtoService;

        private const string FactKey = "Key_MeowFact_Fact";
        private const string UpdateTimeKey = "Key_MeowFact_UpdateTime";

        private string Lang = "rus";

        public MeowFactService(ILocalStorageService localStorageService, IHttpDtoService httpDtoService)
        {
            this.localStorageService = localStorageService;
            this.httpDtoService = httpDtoService;
        }

        public async Task<MeowFactDto> GetFact()
        {
            var response = await localStorageService.GetItemAsync<MeowFactDto>(FactKey);
            var updated = await localStorageService.GetItemAsync<DateTime>(UpdateTimeKey);
            if (response == null 
                || (DateTime.Now.ToUniversalTime() - updated) > TimeSpan.FromHours(6))
            {
                response = await httpDtoService.GetAsync<MeowFactDto>(
                    $"https://meowfacts.herokuapp.com/?lang={Lang}");
                await localStorageService.SetItemAsync(FactKey, response);
                await localStorageService.SetItemAsync(UpdateTimeKey, DateTime.Now.ToUniversalTime());
            }
            return response;
        }
    }
}
