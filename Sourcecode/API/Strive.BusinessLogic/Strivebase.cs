using Cocoon.ORM;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities;
using Strive.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;

namespace Strive.BusinessLogic
{
    public class Strivebase
    {
        public readonly IDistributedCache _cache;
        public readonly ITenantHelper _tenant;
        public readonly IConfiguration _config;
        public JObject _resultContent;
        public Result _result;

        public Strivebase()
        {

        }

        public Strivebase(ITenantHelper tenantHelper, IDistributedCache cache = null, IConfiguration config = null)
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
                _tenant.SchemaName = tenantSchema.Schemaname;
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


        protected Result ResultWrap<T>(Func<List<T>> RALMethod, string ResultName)
        {
            try
            {
                var res = RALMethod.Invoke();
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(Func<T> RALMethod, string ResultName)
        {
            try
            {
                var res = RALMethod.Invoke();
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(Func<DateTime?, DateTime?, T> RALMethod, DateTime? startDate, DateTime? endDate, string ResultName)
        {
            try
            {
                var res = RALMethod.Invoke(startDate, endDate);
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }


        protected Result ResultWrap<T>(T result, string ResultName)
        {
            try
            {
                _resultContent.Add(result.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(Func<string, int, DateTime, T> RALMethod, string cashRegType, int locationId, DateTime cashRegDate, string ResultName)
        {
            try
            {
                var res = RALMethod.Invoke(cashRegType, locationId, cashRegDate);
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        protected Result ResultWrap<T>(string ResultName, BusinessEntities.Model.Document document)
        {
            try
            {
                // var res = RALMethod.Invoke();
                _resultContent.Add(ResultName);
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(Func<int, List<T>> RALMethod, int id, int employeeId, int roleId, DateTime date, string ResultName)
        {
            try
            {
                var res = RALMethod.Invoke(id);
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(Func<int, T> RALMethod, int id, string ResultName)
        {
            try
            {
                var res = RALMethod.Invoke(id);
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(Func<string, T> RALMethod, string emailId, string ResultName)
        {
            try
            {
                var res = RALMethod.Invoke(emailId);
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(Func<JObject, T> RALMethod, JObject obj, string ResultName)
        {
            try
            {
                var res = RALMethod.Invoke(obj);
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        //protected Result ResultWrap<T>(Func<int, T> RALMethod, int id)
        //{
        //    try
        //    {
        //        var res = RALMethod.Invoke(id);
        //        _result = Helper.BindSuccessResult(_resultContent);
        //    }
        //    catch (Exception ex)
        //    {
        //        _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
        //    }
        //    return _result;
        //}


        protected Result ResultWrap<T, T1>(Func<T1, T> ralmethod, T1 model, string ResultName)
        {
            try
            {
                AddAudit(model);
                var res = ralmethod.Invoke(model);
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(Func<int, string, T> RALMethod, int id, string str, string ResultName)
        {
            try
            {
                var res = RALMethod.Invoke(id, str);
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected Result ResultWrap<T>(bool res, string ResultName, string message)
        {
            try
            {
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        protected Result ResultWrap(bool res, string ResultName, string message)
        {
            try
            {
                _resultContent.Add(res.WithName(ResultName));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        protected void AddAudit<T>(T tdata)
        {
            string action = "ADD";
            Type type = typeof(T);
            var clsProperty = type.GetProperties().Any(x => !x.PropertyType.FullName.StartsWith("System."));// type.GetProperties().Any(x => x.PropertyType.IsClass == true);
            //var prpTypes = type.GetProperties().Select(x => x.PropertyType);
            if (clsProperty & tdata != null)
            {
                 
                foreach (PropertyInfo prp in type.GetProperties())
                {
                    var model = prp.GetValue(tdata, null);

                    if (model is null) continue;

                    Type subModelType = model.GetType();

                    if (typeof(int) == subModelType || typeof(string) == subModelType || typeof(decimal) == subModelType) continue;

                    PropertyInfo prInfo = null;
                    if (subModelType.IsGenericType)
                    {
                        //SetAuditDetailsForList(action, ref model, type);
                        model = SetAuditDetailsForList(action, ref model, type);
                        prp.SetValue(tdata, model);
                    }
                    else if (subModelType.IsClass)
                    {
                        prInfo = subModelType.GetProperties().Where(x => x.GetCustomAttributes(typeof(IgnoreOnInsert), true).Any()).FirstOrDefault();
                        if (prInfo != null)
                        {
                            action = (prInfo.GetValue(model, null).toInt() > 0) ? "UPD" : action;
                            SetAuditDetails(action, ref model, subModelType);
                        }
                    }
                    else
                    {
                        prInfo = type.GetProperties().Where(x => x.GetCustomAttributes(typeof(IgnoreOnInsert), true).Any()).FirstOrDefault();
                        if (prInfo != null)
                        {
                            action = (prInfo.GetValue(tdata, null).toInt() > 0) ? "UPD" : action;
                            SetAuditDetails(action, ref model, type);
                        }
                    }
                }
            }
            else
            {
                var prInfo = type.GetProperties().Where(x => x.GetCustomAttributes(typeof(IgnoreOnInsert), true).Any()).FirstOrDefault();
                if (prInfo != null)
                {
                    action = (prInfo.GetValue(tdata, null).toInt() > 0) ? "UPD" : action;
                    var obj = (object)tdata;

                    SetAuditDetails(action, ref obj, type);
                }
            }
        }

        private void SetAuditDetails(string action, ref object model, Type subModelType)
        {
            if (subModelType.HasProperty("CreatedBy"))
            {
                if (action == "ADD")
                {
                    subModelType.GetProperty("CreatedBy")?.SetValue(model, _tenant.EmployeeId.toInt());
                    subModelType.GetProperty("CreatedDate")?.SetValue(model, DateTimeOffset.UtcNow);
                    subModelType.GetProperty("UpdatedBy")?.SetValue(model, _tenant.EmployeeId.toInt());
                    subModelType.GetProperty("UpdatedDate")?.SetValue(model, DateTimeOffset.UtcNow);
                    subModelType.GetProperty("IsActive")?.SetValue(model, true);
                    subModelType.GetProperty("IsDeleted")?.SetValue(model, false);
                }
                if (action == "UPD")
                {
                    subModelType.GetProperty("UpdatedBy")?.SetValue(model, _tenant.EmployeeId.toInt());
                    subModelType.GetProperty("UpdatedDate")?.SetValue(model, DateTimeOffset.UtcNow);
                }
            }
        }

        private object SetAuditDetailsForList(string action, ref object model, Type subModelType)
        {
            var tp = model.GetType().GenericTypeArguments.First();
            var jString1 = JsonConvert.SerializeObject(model);
            var components = (IList)JsonConvert.DeserializeObject(jString1, typeof(List<>).MakeGenericType(new[] { tp }));

            foreach (var m in components)
            {
                var smt = m.GetType();
                if (smt.HasProperty("CreatedBy"))
                {
                    if (action == "ADD")
                    {
                        smt.GetProperty("CreatedBy")?.SetValue(m, _tenant.EmployeeId.toInt());
                        smt.GetProperty("CreatedDate")?.SetValue(m, DateTimeOffset.UtcNow);
                        smt.GetProperty("UpdatedBy")?.SetValue(m, _tenant.EmployeeId.toInt());
                        smt.GetProperty("UpdatedDate")?.SetValue(m, DateTimeOffset.UtcNow);
                        smt.GetProperty("IsActive")?.SetValue(m, true);
                        smt.GetProperty("IsDeleted")?.SetValue(m, false);
                    }
                    if (action == "UPD")
                    {
                        smt.GetProperty("UpdatedBy")?.SetValue(m, _tenant.EmployeeId.toInt());
                        smt.GetProperty("UpdatedDate")?.SetValue(m, DateTimeOffset.UtcNow);
                    }
                }
            }
            return components;
        }

    }
}