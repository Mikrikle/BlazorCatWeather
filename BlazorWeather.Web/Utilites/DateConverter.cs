namespace BlazorWeather.Web.Utilites
{
    public static class DateConverter
    {
        public static DateTime UnixTimeToLocalDateTime(long unixtime)
        {
            DateTime dtDateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixtime).ToLocalTime();
            return dtDateTime;
        }

        public static long DateTimeToUnixTime(DateTime MyDateTime)
        {
            TimeSpan timeSpan = MyDateTime - new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)timeSpan.TotalSeconds;
        }

    }
}
