﻿using BlazorWeather.Web.Exceptions;
using BlazorWeather.Web.Dtos;

namespace BlazorWeather.Web.Services.Contracts
{
    public interface ICatApiService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ServiceResponseException"></exception>
        public Task<CatApiImageDto> GetImage();
    }
}
