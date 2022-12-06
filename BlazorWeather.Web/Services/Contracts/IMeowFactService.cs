using BlazorWeather.Web.Dtos;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface IMeowFactService
    {
        public Task<ResponseOrError<MeowFactDto>> GetFact();
    }
}
