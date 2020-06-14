using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Strive.ResourceAccess
{
    public class EmployeeRal
    {
        IDbConnection _dbconnection;
        public Db db;
        public EmployeeRal(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        public EmployeeRal(ITenantHelper tenant)
        {
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }
        public List<Employee> GetEmployeeDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Employee> lstResource = new List<Employee>();
            var res = db.Fetch<Employee>(SPEnum.USPGETEMPLOYEE.ToString(), dynParams);
            return res;
        }
    }
}
