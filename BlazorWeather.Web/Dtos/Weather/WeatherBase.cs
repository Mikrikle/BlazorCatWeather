using System.Text.Json.Serialization;

namespace BlazorWeather.Web.Dtos
{

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
        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }
        [JsonPropertyName("temp_min")]
        public double TempMin { get; set; }
        [JsonPropertyName("temp_max")]
        public double TempMax { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        [JsonPropertyName("sea_level")]
        public int SeaLevel { get; set; }
        [JsonPropertyName("grnd_level")]
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
