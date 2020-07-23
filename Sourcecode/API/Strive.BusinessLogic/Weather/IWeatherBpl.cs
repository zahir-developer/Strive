using Strive.BusinessEntities;
using Strive.BusinessEntities.Weather;
using Strive.Common;
using System;
using System.Collections.Generic;

namespace Strive.BusinessLogic.Weather
{
    public interface IWeatherBpl
    {
        Result GetWeatherPrediction(int locationId, DateTime date);

        Result AddWeatherPrediction(WeatherPrediction weatherPrediction);

    }
}
