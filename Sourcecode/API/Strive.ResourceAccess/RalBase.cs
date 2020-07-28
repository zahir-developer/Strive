using Cocoon.ORM;
using Dapper;
using Strive.Common;
using Strive.Repository;
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

        public RalBase(ITenantHelper tenant)
        {
            _tenant = tenant;
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
            _prm = new DynamicParameters();
            cs = _dbconnection.ConnectionString;
        }
        public RalBase(ITenantHelper tenant, bool isAuth)
        {
            _tenant = tenant;
            if (isAuth)
                _dbconnection = tenant.dbAuth();
            else
                _dbconnection = tenant.db();

            db = new Db(_dbconnection);
        }

        protected void AddAudit<T>(T tdata)
        {
            string action = "ADD";
            Type type = typeof(T);
            foreach (PropertyInfo prp in type.GetProperties())
            {
                var model = prp.GetValue(tdata, null);


                var propertiesWithMyAttribute = type.GetProperties().Where(x => x.GetCustomAttributes(typeof(IgnoreOnInsert), true).Any()).FirstOrDefault();
                if(propertiesWithMyAttribute.GetValue(tdata,null).toInt() > 0 )
                {
                    action = "UPD";
                }

                Type subModelType = model.GetType();
                if (action == "ADD")
                {
                    subModelType.GetProperty("CreatedBy").SetValue(model, _tenant.EmployeeId);
                    subModelType.GetProperty("CreatedDate").SetValue(model, DateTime.Now.ToString());
                    subModelType.GetProperty("UpdatedBy").SetValue(model, _tenant.EmployeeId);
                    subModelType.GetProperty("UpdatedDate").SetValue(model, DateTime.Now.ToString());
                }
                if (action == "UPD")
                {
                    subModelType.GetProperty("UpdatedBy").SetValue(model, _tenant.EmployeeId);
                    subModelType.GetProperty("UpdatedDate").SetValue(model, DateTime.Now.ToString());
                }
            }
        }
    }

}
