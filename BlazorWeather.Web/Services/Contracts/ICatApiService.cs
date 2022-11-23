using BlazorWeather.Web.Dtos;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface ICatApiService
    {
        public Task<CatApiImageDto?> GetImage();
    }
}
