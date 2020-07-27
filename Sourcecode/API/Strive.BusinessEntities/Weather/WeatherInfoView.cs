

using System.Collections.Generic;

namespace Strive.BusinessEntities.Weather
{
    public class WeatherInfo
    {
        public string Temporature { get; set; }

        public string RainPercentage { get; set; }

    }
 
    public class WeatherView
    {
        public WeatherInfo CurrentWeather { get; set; }
        public WeatherInfo LastWeekWeather { get; set; }
        public WeatherInfo LastMonthWeather { get; set; }
    }


}