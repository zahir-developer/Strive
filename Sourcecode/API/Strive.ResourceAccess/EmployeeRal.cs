using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Strive.BusinessEntities.Employee;
using Strive.BusinessEntities.Code;
using System;
using Strive.BusinessEntities.DTO.Employee;
using System.Reflection;
//using FastDeepCloner;
using Newtonsoft.Json;
using System.Collections;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel.Employee;
using Strive.BusinessEntities.ViewModel;

namespace Strive.ResourceAccess
{
    public class EmployeeRal : RalBase
    {
        public EmployeeRal(ITenantHelper tenant) : base(tenant) { }

        public EmployeeDetailViewModel GetEmployeeById(int employeeId)
        {
            _prm.Add("EmployeeId", employeeId);
            var lstResult = db.FetchMultiResult<EmployeeDetailViewModel>(SPEnum.USPGETEMPLOYEEBYID.ToString(), _prm);
            return lstResult;
        }

        public EmployeeListViewModel GetEmployeeList()
        {
            return db.FetchMultiResult<EmployeeListViewModel>(SPEnum.USPGETEMPLOYEELIST.ToString(), _prm);
        }

        public List<EmployeeViewModel> GetAllEmployeeDetail(string employeeName)
        {
            _prm.Add("@EmployeeName", employeeName);
            return db.Fetch<EmployeeViewModel>(EnumSP.Employee.USPGETALLEMPLOYEEDETAIL.ToString(), _prm);
        }

        public bool AddEmployee(EmployeeModel employee)
        {
            return dbRepo.InsertPc(employee, "EmployeeId");
        }

        public bool UpdateEmployee(EmployeeModel employee)
        {
            return dbRepo.UpdatePc(employee);
        }

        public List<RoleViewModel> GetAllEmployeeRoles()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<RoleViewModel> lstEmployee = new List<RoleViewModel>();
            lstEmployee = db.Fetch<RoleViewModel>(SPEnum.USPGETEMPLOYEEROLES.ToString(), dynParams);
            return lstEmployee;
        }

        public EmployeeLoginViewModel GetEmployeeByAuthId(int authId)
        {
            _prm.Add("AuthId", authId);
            var lstResult = db.FetchMultiResult<EmployeeLoginViewModel>(SPEnum.USPGETUSERBYAUTHID.ToString(), _prm);
            return lstResult;
        }

        public bool DeleteEmployeeDetails(int employeeId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@EmployeeId", employeeId);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETEEMPLOYEE.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public EmployeeRoleViewModel GetEmployeeRoleById(int employeeId)
        {
            _prm.Add("@EmployeeId", employeeId);
            var lstResult = db.FetchMultiResult<EmployeeRoleViewModel>(SPEnum.USPGETEMPLOYEEROLEBYID.ToString(), _prm);
            return lstResult;
        }


    }
}

