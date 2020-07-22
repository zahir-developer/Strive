using Strive.BusinessEntities;
using Strive.BusinessEntities.Weather;
using Strive.Common;
using System.Collections.Generic;

namespace Strive.BusinessLogic.Weather
{
    public interface IWeatherBpl
    {
        Result GetWeatherPrediction(int locationId);

        Result AddWeatherPrediction(WeatherPrediction weatherPrediction);

        Result UpdateWeatherPrediction(WeatherPrediction weatherPrediction);
    }
}
