using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.Weather;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Strive.ResourceAccess
{
    public class WeatherRal
    {
        private readonly Db _db;

        public WeatherRal(ITenantHelper tenant)
        {
            var dbConnection = tenant.db();
            _db = new Db(dbConnection);
        }
        public WeatherPrediction GetWeatherDetails(int locationId)
        {
            var allPredictions = _db.GetAll<WeatherPrediction>();

            return allPredictions.Where(s => s.LocationId == locationId).FirstOrDefault();
        }

        public bool AddWeather(WeatherPrediction weatherPrediction)
        {

            int WeatherPredictionId = Convert.ToInt32(_db.Insert<WeatherPrediction>(weatherPrediction));

            return WeatherPredictionId > 0;
        }

        public bool UpdateWeather(WeatherPrediction weatherPrediction)
        {

            int WeatherPredictionId = Convert.ToInt32(_db.Update<WeatherPrediction>(weatherPrediction));

            return WeatherPredictionId > 0;
        }

    }
}
