using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Weather;
using Strive.BusinessEntities.Weather;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Weather
{
    public interface IWeatherBpl
    {
        Result GetWeatherPrediction(int locationId, DateTime date);

        Result AddWeatherPrediction(WeatherDTO weatherPrediction);

        Task<WeatherView> GetWeather(string baseUrl, string apiKey, string apiMethod, int locationId);

        void SaveWeathertoDB_AutoProcess();

        Result GetForcastedCarEmployeehours(ForecastedRainPercentageDto forecastedRainPercentage);

    }
}
