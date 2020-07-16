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
        public List<Employee> GetEmployeeDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = db.FetchRelation3<Employee, EmployeeDetail, EmployeeAddress,EmployeeRoles>(SPEnum.USPGETEMPLOYEE.ToString(),dynParams);
            return lstEmployee;
        }
        
        public List<Code> GetAllEmployeeRoles()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Code> lstEmployee = new List<Code>();
            lstEmployee = db.Fetch<Code>(SPEnum.USPGETEMPLOYEEROLES.ToString(), dynParams);
            return lstEmployee;
        }
        public List<Employee> GetEmployeeByIdDetails(long id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@EmployeeId", id);
            List<Employee> lstEmployeeInfo = new List<Employee>();
            lstEmployeeInfo = db.FetchRelation3<Employee, EmployeeDetail, EmployeeAddress,EmployeeRoles>(SPEnum.USPGETEMPLOYEEBYEMPID.ToString(), dynParams);
            return lstEmployeeInfo;
        }

        public bool SaveEmployeeDetails(List<Employee> lstEmployee)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<EmployeeInformation> lstEmpInfo = new List<EmployeeInformation>();
            var empInf = lstEmployee.FirstOrDefault();
            lstEmpInfo.Add(new EmployeeInformation
            {
                EmployeeId = empInf.EmployeeId,
                FirstName = empInf.FirstName,
                MiddleName = empInf.MiddleName,
                LastName = empInf.LastName,
                Gender = empInf.Gender,
                SSNo = empInf.SSNo,
                MaritalStatus = empInf.MaritalStatus,
                IsCitizen = empInf.IsCitizen,
                AlienNo = empInf.AlienNo,
                BirthDate = empInf.BirthDate,
                ImmigrationStatus = empInf.ImmigrationStatus,
                CreatedDate = empInf.CreatedDate,
                IsActive = empInf.IsActive
            });
            dynParams.Add("@tvpEmployee", lstEmpInfo.ToDataTable().AsTableValuedParameter("tvpEmployee"));
            dynParams.Add("@tvpEmployeeDetail", empInf.EmployeeDetail.ToDataTable().AsTableValuedParameter("tvpEmployeeDetail"));
            dynParams.Add("@tvpEmployeeAddress", empInf.EmployeeAddress.ToDataTable().AsTableValuedParameter("tvpEmployeeAddress"));
            dynParams.Add("@tvpEmployeeRoles", empInf.EmployeeRoles.ToDataTable().AsTableValuedParameter("tvpEmployeeRoles"));
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

        public void SaveEmployeeLogin(List<EmployeeLogin> lstEmployeeLogin)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@Logintbl", lstEmployeeLogin.ToDataTable().AsTableValuedParameter("tvpAuthMaster"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVELOGIN.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
        }

    }
}
