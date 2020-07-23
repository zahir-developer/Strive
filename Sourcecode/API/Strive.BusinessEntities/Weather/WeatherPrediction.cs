using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Weather
{
    [Table("tblWeatherPrediction")]
    public class WeatherPrediction
    {
        [Key]
        public int WeatherId { get; set; }

        public int LocationId { get; set; }

        public string Weather { get; set; }

        public string RainProbability { get; set; }

        public string PredictedBusiness { get; set; }

        public string TargetBusiness { get; set; }

        public DateTime CreatedDate { get; set; }
        
    }
}
