using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Strive.BusinessEntities.Employee;

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
        public List<Employees> GetEmployeeDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Employees> lstEmployee = new List<Employees>();
            lstEmployee = db.FetchRelation2<Employees, EmployeeDetail, EmployeeAddress>(SPEnum.USPGETEMPLOYEE.ToString(),dynParams);
            return lstEmployee;
        }
        
        public List<EmployeeInfo> GetEmployeeByIdDetails(long id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@EmployeeId", id);
            List<EmployeeInfo> lstEmployeeInfo = new List<EmployeeInfo>();
            lstEmployeeInfo = db.FetchRelation2<EmployeeInfo, EmployeeDetail, EmployeeAddress>(SPEnum.USPGETEMPLOYEEBYEMPID.ToString(), dynParams);
            return lstEmployeeInfo;
        }

        public bool SaveEmployeeDetails(List<Employees> lstEmployee)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpEmployee", lstEmployee.FirstOrDefault().Employee.ToDataTable().AsTableValuedParameter("tvpEmployee"));
            dynParams.Add("@tvpEmployeeDetail", lstEmployee.FirstOrDefault().EmployeeDetail.ToDataTable().AsTableValuedParameter("tvpEmployeeDetail"));
            dynParams.Add("@tvpEmployeeAddress", lstEmployee.FirstOrDefault().EmployeeAddress.ToDataTable().AsTableValuedParameter("tvpEmployeeAddress"));
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

        public bool DeleteEmployeeDetails(long empId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblEmployeeId", empId);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETEEMPLOYEE.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}
