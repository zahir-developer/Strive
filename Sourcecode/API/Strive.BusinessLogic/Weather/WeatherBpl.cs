using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Weather;
using Strive.BusinessLogic.Weather;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Net;

namespace Strive.BusinessLogic
{
    public class WeatherBpl : Strivebase, IWeatherBpl
    {
        readonly ITenantHelper _tenant;
        readonly JObject _resultContent = new JObject();
        Result _result;
        public WeatherBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(cache)
        {
            _tenant = tenantHelper;
        }

        public Result GetWeatherPrediction(int locationId, DateTime dateTime)
        {
            try
            {
                var weather = new WeatherRal(_tenant).GetWeatherDetails(locationId, dateTime);
                if (weather != null)
                    _resultContent.Add(weather.WithName("WeatherPrediction"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }


        public Result AddWeatherPrediction(WeatherPrediction weatherPrediction)
        {
            try
            {
                bool result = false;
                if (weatherPrediction.WeatherId == 0)
                {
                    result = new WeatherRal(_tenant).AddWeather(weatherPrediction);
                }
                else
                {
                    result = new WeatherRal(_tenant).UpdateWeather(weatherPrediction);
                }
                _resultContent.Add(result.WithName("Status"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return _result;
        }
    }
}
