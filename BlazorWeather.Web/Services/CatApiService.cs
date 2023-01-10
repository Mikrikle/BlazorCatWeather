using Blazored.LocalStorage;
using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Exceptions;
using BlazorWeather.Web.Services.Contracts;
using System.Net;
using System.Net.Http.Json;

namespace BlazorWeather.Web.Services
{
    public class CatApiService : ICatApiService
    {
        private readonly IHttpDtoService httpDtoService;
        private readonly ILocalStorageService localStorageService;

        private const string CatKey = "Key_CatApi_Cat";
        private const string UpdateTimeKey = "Key_CatApi_UpdateTime";

        public CatApiService(ILocalStorageService localStorageService, IHttpDtoService httpDtoService)
        {
            this.localStorageService = localStorageService;
            this.httpDtoService = httpDtoService;
        }

        public async Task<CatApiImageDto> GetImage()
        {
            var response = await localStorageService.GetItemAsync<CatApiImageDto>(CatKey);
            var updated = await localStorageService.GetItemAsync<DateTime>(UpdateTimeKey);
            if (response == null 
                || (DateTime.Now.ToUniversalTime() - updated) > TimeSpan.FromHours(6))
            {

                response = (await httpDtoService.GetAsync<CatApiImageDto[]>("https://api.thecatapi.com/v1/images/search"))
                    .FirstOrDefault();
                if (response == null)
                    throw new ServiceResponseException("Not Found", HttpStatusCode.NotFound);
                await localStorageService.SetItemAsync(CatKey, response);
                await localStorageService.SetItemAsync(UpdateTimeKey, DateTime.Now.ToUniversalTime());
            }
            return response;
        }
    }
}
