using System;

namespace Strive.BusinessEntities.Weather
{
    public class WeatherPrediction
    {
        public int WeatherId { get; set; }

        public int LocationId { get; set; }

        public string Weather { get; set; }

        public string RainProbability { get; set; }

        public string PredictedBusiness { get; set; }

        public string TargetBusiness { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public int WashCount { get; set; }
        
    }
  
}
