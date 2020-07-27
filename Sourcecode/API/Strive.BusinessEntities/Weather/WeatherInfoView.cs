

using System.Collections.Generic;

namespace Strive.BusinessEntities.Weather
{
    public class WeatherInfoView
    {
        public WeatherInfo weatherInfo { get; set; }


    }
    public class LastWeekWeather
    {
        public decimal? Temporature { get; set; }

        public decimal? Rain { get; set; }

        public int RainPercentage { get; set; }

    }
    public class LastMonthWeather
    {
        public decimal? Temporature { get; set; }

        public decimal? Rain { get; set; }

        public int RainPercentage { get; set; }
    }

    public class WeatherInfo
    {
        public decimal? Temporature { get; set; }

        public decimal? Rain { get; set; }

        public int RainPercentage { get; set; }

        public string Icon { get; set; }
        public LastWeekWeather lastWeekWeather { get; set; }
        public LastMonthWeather LastMonthWeather { get; set; }
    }


}