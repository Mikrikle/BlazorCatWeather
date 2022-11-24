using System.Text.Json.Serialization;

namespace BlazorWeather.Web.Dtos
{
    public class WeatherCurrentDto
    {
        public Coord Coord { get; set; }
        public List<Weather> Weather { get; set; } = new();
        public Main Main { get; set; }
        public int Visibility { get; set; }
        public Wind? Wind { get; set; }
        public Rain? Rain { get; set; }
        public Snow? Snow { get; set; }
        public Clouds? Clouds { get; set; }
        public int Dt { get; set; }
        public Sys Sys { get; set; }
        public int Timezone { get; set; }
    }
}
