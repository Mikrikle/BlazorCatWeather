namespace BlazorWeather.Web.Dtos
{
    public class GeocodingDto
    {
        public string Name { get; set; }
        public Dictionary<string, string>? Local_names { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
    }
}
