using System.Globalization;
using Blazored.LocalStorage;
using Blazored.Modal;
using BlazorWeather.Web;
using BlazorWeather.Web.Services;
using BlazorWeather.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddLocalization();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredModal();

builder.Services.AddScoped<IHttpDtoService, HttpDtoService>();
builder.Services.AddScoped<IMeowFactService, MeowFactService>();
builder.Services.AddScoped<ICatApiService, CatApiService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();

await builder.Build().RunAsync();
