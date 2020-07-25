using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Net;

namespace Strive.BusinessLogic
{
    public class Strivebase
    {
        public readonly IDistributedCache _cache;
        public readonly ITenantHelper _tenant;
        public readonly IConfiguration _config;
        public JObject _resultContent;
        public Result _result;
        public Strivebase(ITenantHelper tenantHelper, IDistributedCache cache=null, IConfiguration config=null)
        {
            _cache = cache;
            _tenant = tenantHelper;
            _config = config;
            _resultContent = new JObject();
        }

        public void SetTenantSchematoCache(TenantSchema tenantSchema)
        {
            string strTenantSchema = _cache.GetString(tenantSchema.UserGuid);
            if (string.IsNullOrEmpty(strTenantSchema))
            {
                //Strivecache.SetString(tenantSchema.UserGuid, JsonConvert.SerializeObject(tenantSchema));
                _cache.SetString(tenantSchema.Schemaname, JsonConvert.SerializeObject(tenantSchema));
            }
        }

        protected string GetRefreshToken(string userGuid)
        {
            string refreshToken = _cache.GetString(userGuid);
            return refreshToken;
        }

        protected void DeleteRefreshToken(string userGuid, string refreshToken)
        {
            _cache.Remove(userGuid);
        }

        protected void SaveRefreshToken(string userGuid, string refreshToken)
        {
            _cache.SetString(userGuid, refreshToken);
        }

        protected string GetTenantConnectionString(TenantSchema tenantSchema, string conString)
        {
            conString = conString.Replace("[UserName]", tenantSchema.Username);
            conString = conString.Replace("[Password]", tenantSchema.Password);
            
            return conString;
        }

        protected Result ResultWrap<T>(Func<List<T>> getEmployeeList, string ResultName)
        {
            try
            {
                var res = getEmployeeList.Invoke();
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(Func<int, List<T>> getEmployeeList, int id, string ResultName)
        {
            try
            {
                var res = getEmployeeList.Invoke(id);
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(Func<int, T> getEmployeeList, int id, string ResultName)
        {
            try
            {
                var res = getEmployeeList.Invoke(id);
                _resultContent.Add(res.WithName(ResultName));
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