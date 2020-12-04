using Dapper;
using Strive.BusinessEntities;
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
    public class WeatherRal:RalBase
    {
        private readonly Db _db;

        public WeatherRal(ITenantHelper tenant):base(tenant)
        {
            var dbConnection = tenant.db();
            _db = new Db(dbConnection);
        }
        public List<WeatherPredictions> GetWeatherDetails(int locationId, DateTime dateTime)
        {

            DateTime lastMonth = dateTime.AddMonths(-1);
            DateTime lastweek = dateTime.AddDays(-7);
            DateTime lastThirdMonth = dateTime.AddMonths(-3);

            _prm.Add("@LocationId", locationId);
            _prm.Add("@date", dateTime);
            _prm.Add("@lastMonth", lastMonth);
            _prm.Add("@lastweek", lastweek);
            _prm.Add("@lastThirdMonth", lastThirdMonth);
            
       return db.FetchMultiResult<List<WeatherPredictions>>(EnumSP.SalesReport.USPGetPastWeatherInfo.ToString(), _prm);
            
            
            
            //_prm.Add("@LocationId", locationId);
            //_prm.Add("@date", oneweekBefore);
            //var data2 = db.FetchMultiResult<WeatherPrediction>(EnumSP.SalesReport.USPGetPastWeatherInfo.ToString(), _prm);


           
            //_prm.Add("@LocationId", locationId);
            //_prm.Add("@date", threeMonthBefore);
            //var data3 = db.FetchMultiResult<WeatherPrediction>(EnumSP.SalesReport.USPGetPastWeatherInfo.ToString(), _prm);

          

            //weatherPrediction.WeatherPredictionToday = _db.GetAll<WeatherPrediction>().Where(s => s.LocationId == locationId && s.CreatedDate == dateTime)
            //                     .OrderByDescending(s => s.WeatherId).FirstOrDefault();
            //weatherPrediction.WeatherPredictionOneMonth = data1;
            ////.Where(s => s.LocationId == locationId && s.CreatedDate == oneMonthBefore)
            //                     //.OrderByDescending(s => s.WeatherId).FirstOrDefault();

            //weatherPrediction.WeatherPredictionOneWeek = data2;
            ////.Where(s => s.LocationId == locationId && s.CreatedDate == oneweekBefore)
            ////                 .OrderByDescending(s => s.WeatherId).FirstOrDefault();

            //weatherPrediction.WeatherPredictionOneWeek = data3;
            //    //.Where(s => s.LocationId == locationId && s.CreatedDate == threeMonthBefore)
            //    //                 .OrderByDescending(s => s.WeatherId).FirstOrDefault();


            //return weatherPrediction;
                //.Where(s => s.LocationId == locationId && s.CreatedDate.Date == dateTime.Date)
                //                 .OrderByDescending(s => s.WeatherId).FirstOrDefault();
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
