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

        public bool SaveEmployeeDetails(List<Employee> lstEmployee)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpEmployee", lstEmployee.ToDataTable().AsTableValuedParameter("tvpEmployee"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVEEMPLOYEE.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}
