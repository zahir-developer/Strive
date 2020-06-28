using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

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
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = db.FetchRelation3<Employee, EmployeeAddress, EmployeeDetail,EmployeeRole>(SPEnum.USPGETEMPLOYEE.ToString(),dynParams);
            return lstEmployee;
        }

        public bool SaveEmployeeDetails(List<Employee> lstEmployee)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpEmployee", lstEmployee.ToDataTable().AsTableValuedParameter("tvpEmployee"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVEEMPLOYEE.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

        public Employee GetEmployeeByAuthId(int authId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("AuthId", authId);
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = db.FetchRelation1<Employee, EmployeeRole>(SPEnum.USPGETUSERBYAUTHID.ToString(), dynParams);
            return lstEmployee.FirstOrDefault();
        }
    }
}
