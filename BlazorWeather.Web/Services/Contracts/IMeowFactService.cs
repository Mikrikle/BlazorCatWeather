using BlazorWeather.Web.Exceptions;
using BlazorWeather.Web.Dtos;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface IMeowFactService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ServiceResponseException"></exception>
        public Task<MeowFactDto> GetFact();
    }
}
