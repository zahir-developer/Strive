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

        public Result GetWeatherPrediction(int locationId)
        {
            try
            {
                var product = new WeatherRal(_tenant).GetWeatherDetails(locationId);
                _resultContent.Add(product.WithName("WeatherPrediction"));
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
                var list = new WeatherRal(_tenant).AddWeather(weatherPrediction);
                _resultContent.Add(list.WithName("WeatherPrediction"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }

            return _result;
        }

       
        public Result UpdateWeatherPrediction(WeatherPrediction weatherPrediction)
        {
            try
            {
                var list = new WeatherRal(_tenant).UpdateWeather(weatherPrediction);
                _resultContent.Add(list.WithName("Success"));
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
