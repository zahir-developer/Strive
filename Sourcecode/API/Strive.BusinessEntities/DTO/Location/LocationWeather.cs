using Cocoon.ORM;
using System;

namespace Strive.BusinessEntities.Model
{
    [OverrideName("tblLocationWeather")]
    public class LocationWeather
    {
        [Column, PrimaryKey, IgnoreOnInsert, IgnoreOnUpdate]
        public int LocationWeatherId { get; set; }

        [Column]
        public int LocationId { get; set; }

        [Column]
        public string LocationApiId { get; set; }

        [Column]
        public string HighTemperature { get; set; }

        [Column]
        public string LowTemperature { get; set; }

        [Column]
        public string RainProbability { get; set; }

        [Column]
        public DateTime? WeatherDate { get; set; }

        [Column]
        public DateTime? CreatedDate { get; set; }
    }
}
