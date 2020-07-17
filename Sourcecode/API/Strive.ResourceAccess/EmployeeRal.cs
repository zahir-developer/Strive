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
using Strive.BusinessEntities.Code;

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

        public EmployeeRal(ITenantHelper tenant, bool isAuth)
        {
            if (isAuth)
                _dbconnection = tenant.dbAuth();
        }

        public EmployeeRal(ITenantHelper tenant)
        {
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }
        public List<EmployeeView> GetEmployeeDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            var lstEmployee = db.FetchRelation3<EmployeeView, EmployeeDetail, EmployeeAddress, EmployeeRole>(SPEnum.USPGETEMPLOYEE.ToString(), dynParams);
            return lstEmployee;
        }

        public List<Code> GetAllEmployeeRoles()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Code> lstEmployee = new List<Code>();
            lstEmployee = db.Fetch<Code>(SPEnum.USPGETEMPLOYEEROLES.ToString(), dynParams);
            return lstEmployee;
        }
        public List<EmployeeView> GetEmployeeByIdDetails(long id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@EmployeeId", id);
            var lstEmployeeInfo = db.FetchRelation3<EmployeeView, EmployeeDetail, EmployeeAddress, EmployeeRole>(SPEnum.USPGETEMPLOYEEBYEMPID.ToString(), dynParams);
            return lstEmployeeInfo;
        }

        public bool SaveEmployeeDetails(EmployeeView employee)
        {
            DynamicParameters dynParams = new DynamicParameters();
            var lstEmp = new List<Employee>();
            lstEmp.Add(employee);

            dynParams.Add("@tvpEmployee", lstEmp.ToDataTable().AsTableValuedParameter("tvpEmployee"));
            dynParams.Add("@tvpEmployeeDetail", employee.EmployeeDetail.ToDataTable().AsTableValuedParameter("tvpEmployeeDetail"));
            dynParams.Add("@tvpEmployeeAddress", employee.EmployeeAddress.ToDataTable().AsTableValuedParameter("tvpEmployeeAddress"));
            dynParams.Add("@tvpEmployeeRole", employee.EmployeeRole.ToDataTable().AsTableValuedParameter("tvpEmployeeRole"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVEEMPLOYEE.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

        public EmployeeView GetEmployeeByAuthId(int authId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("AuthId", authId);
            List<EmployeeView> lstEmployee = new List<EmployeeView>();
            lstEmployee = db.FetchRelation1<EmployeeView, EmployeeRole>(SPEnum.USPGETUSERBYAUTHID.ToString(), dynParams);
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

        public void SaveEmployeeLogin(List<EmployeeLogin> lstEmployeeLogin)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@Logintbl", lstEmployeeLogin.ToDataTable().AsTableValuedParameter("tvpAuthMaster"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVELOGIN.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
        }

    }
}
