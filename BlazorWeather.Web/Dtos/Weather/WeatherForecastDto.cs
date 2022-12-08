using System.Text.Json.Serialization;

namespace BlazorWeather.Web.Dtos
{
    public class WeatherForecastDto
    {
        [JsonPropertyName("list")]
        public List<WeatherList> WeatherList { get; set; } = new();
        public City City { get; set; }
    }

    public class WeatherList
    {
        public List<Weather> Weather { get; set; }
        public Main Main { get; set; }
        public int Visibility { get; set; }
        public Wind Wind { get; set; }
        public Rain? Rain { get; set; }
        public Snow? Snow { get; set; }
        public Clouds Clouds { get; set; }
        public int Dt { get; set; }
        public double Pop { get; set; }
        public string DtTxt { get; set; }

    }
}
