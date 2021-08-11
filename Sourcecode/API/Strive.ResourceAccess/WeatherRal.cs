using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.Weather;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Strive.ResourceAccess
{
    public class WeatherRal : RalBase
    {
        private readonly Db _db;

        public WeatherRal(ITenantHelper tenant) : base(tenant)
        {
            var dbConnection = tenant.db();
            _db = new Db(dbConnection);
        }
        public List<WeatherPredictions> GetWeatherDetails(int locationId, DateTime dateTime)
        {

            DateTime lastMonth = dateTime.AddMonths(-1).Date;
            DateTime lastweek = dateTime.AddDays(-7).Date;
            DateTime lastThirdMonth = dateTime.AddMonths(-3).Date;

            _prm.Add("@LocationId", locationId);
            _prm.Add("@date", dateTime);
            _prm.Add("@lastweek", lastweek.ToString("yyyy-MM-dd"));
            _prm.Add("@lastMonth", lastMonth.ToString("yyyy-MM-dd"));
            _prm.Add("@lastThirdMonth", lastThirdMonth.ToString("yyyy-MM-dd"));

            return db.Fetch<WeatherPredictions>(EnumSP.CashRegister.USPGETPASTWEATHERINFO.ToString(), _prm);

        }

        public bool AddWeather(WeatherPrediction weatherPrediction)
        {
            int WeatherPredictionId = dbRepo.InsertPK(weatherPrediction, "VendorId");

            return WeatherPredictionId > 0;
        }

        public bool UpdateWeather(WeatherPrediction weatherPrediction)
        {
            return dbRepo.Update(weatherPrediction);
        }
        public List<ForcastedCarEmployeehoursViewModel> GetForcastedCarEmployeehours(ForecastedRainPercentageDto forecastedRainPercentage)
        {

            DateTime lastMonth = forecastedRainPercentage.Date.AddMonths(-1).Date;
            DateTime lastweek = forecastedRainPercentage.Date.AddDays(-7).Date;
            DateTime lastThirdMonth = forecastedRainPercentage.Date.AddMonths(-3).Date;

            _prm.Add("@LocationId", forecastedRainPercentage.LocationId);
            _prm.Add("@date", forecastedRainPercentage.Date);
            _prm.Add("@lastweek", lastweek.ToString("yyyy-MM-dd"));
            _prm.Add("@lastMonth", lastMonth.ToString("yyyy-MM-dd"));
            _prm.Add("@lastThirdMonth", lastThirdMonth.ToString("yyyy-MM-dd"));

            return db.Fetch<ForcastedCarEmployeehoursViewModel>(EnumSP.CashRegister.USPGETFORCASTEDRAINPERCENTAGE.ToString(), _prm);

        }
    }
}
