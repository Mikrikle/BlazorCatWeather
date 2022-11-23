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

    public class Coord
    {
        public double Lon { get; set; }
        public double Lat { get; set; }
    }

    public class Weather
    {
        public int Id { get; set; }
        public string Main { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }

    public class Main
    {
        public double Temp { get; set; }
        public double FeelsLike { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int SeaLevel { get; set; }
        public int GrndLevel { get; set; }
    }

    public class Wind
    {
        public double Speed { get; set; }
        public int Deg { get; set; }
        public double Gust { get; set; }
    }

    public class Clouds
    {
        public int All { get; set; }
    }

    public class Rain
    {
        [JsonPropertyName("1h")]
        public double hour { get; set; }
    }

    public class Snow
    {
        [JsonPropertyName("1h")]
        public double hour { get; set; }
    }

    public class Sys
    {
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
    }
}
