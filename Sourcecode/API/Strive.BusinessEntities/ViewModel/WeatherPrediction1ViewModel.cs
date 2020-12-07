using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class WeatherPredictionResultViewModel
    {
            public WeatherPredictions WeatherPredictionToday { get; set; }

            public WeatherPredictions WeatherPredictionLastMonth { get; set; }

            public WeatherPredictions WeatherPredictionLastWeek { get; set; }

            public WeatherPredictions WeatherPredictionLastThirdMonth { get; set; }
    }
}
