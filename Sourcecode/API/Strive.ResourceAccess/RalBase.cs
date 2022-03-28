using Cocoon.ORM;
using Dapper;
using Strive.Common;
using Strive.Repository;
using Strive.RepositoryCqrs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class RalBase
    {
        public IDbConnection _dbconnection;
        public ITenantHelper _tenant;
        public Db db;
        protected DynamicParameters _prm;
        protected string cs;
        public DbRepo dbRepo;
            

        public RalBase(ITenantHelper tenant)
        {
            _tenant = tenant;
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
            _prm = new DynamicParameters();
            cs = _dbconnection.ConnectionString;
            dbRepo = new DbRepo(cs, _tenant.SchemaName);


        }
        public RalBase(ITenantHelper tenant, bool isAuth, bool isTenantAdmin = false)
        {
            string schemaName = tenant.SchemaName;
            _tenant = tenant;
            _prm = new DynamicParameters();

            if (isAuth)
            {
                if (!isTenantAdmin)
                _dbconnection = tenant.dbAuth();
                else
                    _dbconnection = tenant.dbAuthAdmin();

                schemaName = "dbo";
            }
            else
            {
                _dbconnection = tenant.db();
                schemaName = tenant.SchemaName;
            }
            db = new Db(_dbconnection);
            cs = _dbconnection.ConnectionString;
            
            dbRepo = new DbRepo(cs, schemaName);
        }

        protected (T, string, int,string,string) AddAudit<T>(int id,string cs, string sc) where T : class, new()
        {
            var tdata = new T();
            Type type = typeof(T);
            //var model = type.GetValue(tdata, null);

            var prInfo = type.GetProperties().Where(x => x.GetCustomAttributes(typeof(IgnoreOnInsert), true).Any()).FirstOrDefault();

            prInfo.SetValue(tdata, id);

            type.GetProperty("IsActive").SetValue(tdata, false);
            type.GetProperty("IsDeleted").SetValue(tdata, true);
            type.GetProperty("UpdatedBy").SetValue(tdata, _tenant.EmployeeId.toInt());
            type.GetProperty("UpdatedDate").SetValue(tdata, DateTimeOffset.UtcNow);
            return (tdata, prInfo.Name, id,cs,sc);
        }



        //protected T AddAudit<T>() where T : class,new()
        //{
        //    var tdata = new T();
        //    string action = "ADD";
        //    Type type = typeof(T);
        //    foreach (PropertyInfo prp in type.GetProperties())
        //    {
        //        var model = prp.GetValue(tdata, null);


        //        var propertiesWithMyAttribute = type.GetProperties().Where(x => x.GetCustomAttributes(typeof(IgnoreOnInsert), true).Any()).FirstOrDefault();
        //        if(propertiesWithMyAttribute.GetValue(tdata,null).toInt() > 0 )
        //        {
        //            action = "UPD";
        //        }

        //        Type subModelType = model.GetType();
        //        if (action == "ADD")
        //        {
        //            subModelType.GetProperty("CreatedBy").SetValue(model, _tenant.EmployeeId);
        //            subModelType.GetProperty("CreatedDate").SetValue(model, DateTime.Now.ToString());
        //            subModelType.GetProperty("UpdatedBy").SetValue(model, _tenant.EmployeeId);
        //            subModelType.GetProperty("UpdatedDate").SetValue(model, DateTime.Now.ToString());
        //        }
        //        if (action == "UPD")
        //        {
        //            subModelType.GetProperty("UpdatedBy").SetValue(model, _tenant.EmployeeId);
        //            subModelType.GetProperty("UpdatedDate").SetValue(model, DateTime.Now.ToString());
        //        }
        //    }

        //    return tdata;
        //}
    }

}
