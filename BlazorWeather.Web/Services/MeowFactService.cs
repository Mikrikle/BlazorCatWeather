using BlazorWeather.Web.Dtos;
using BlazorWeather.Web.Services.Contracts;
using System.Net.Http.Json;
using static BlazorWeather.Web.Shared.MeowFact;
using static System.Net.WebRequestMethods;

namespace BlazorWeather.Web.Services
{
    public class MeowFactService : IMeowFactService
    {
        private readonly HttpClient httpClient;

        public MeowFactService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<MeowFactDto?> GetFact()
        {
            var response = await httpClient.GetFromJsonAsync<MeowFactDto?>("sample-data/meow-fact.json");
            return response;
        }
    }
}
