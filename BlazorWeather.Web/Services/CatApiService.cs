using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Services.Contracts;
using System.Net.Http.Json;

namespace BlazorWeather.Web.Services
{
    public class CatApiService : ICatApiService
    {
        private readonly HttpClient httpClient;

        public CatApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CatApiImageDto?> GetImage()
        {
            var response = await httpClient.GetFromJsonAsync<CatApiImageDto[]?>("sample-data/cat-api.json");
            return response?.FirstOrDefault();
        }
    }
}
