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

namespace Strive.ResourceAccess
{
    public class EmployeeRal : RalBase
    {
        public EmployeeRal(ITenantHelper tenant) : base(tenant) { }

        public EmployeeViewModel GetEmployeeById(int employeeId)
        {
            _prm.Add("EmployeeId", employeeId);
            var lstResult = db.FetchMultiResult<EmployeeViewModel>(SPEnum.USPGETEMPLOYEEBYID.ToString(), _prm);
            return lstResult;
        }

        public List<EmployeeViewModel> GetEmployeeList()
        {
            return db.Fetch<EmployeeViewModel>(SPEnum.USPGETEMPLOYEELIST.ToString(), _prm);
        }

        public bool AddEmployee(EmployeeModel employee)
        {
            return dbRepo.InsertPc(employee, "EmployeeId");
        }

        public bool UpdateEmployee(EmployeeModel employee)
        {
            return dbRepo.UpdatePc(employee);
        }

        public List<Code> GetAllEmployeeRoles()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Code> lstEmployee = new List<Code>();
            lstEmployee = db.Fetch<Code>(SPEnum.USPGETEMPLOYEEROLES.ToString(), dynParams);
            return lstEmployee;
        }

       

        public EmployeeLoginViewModel GetEmployeeByAuthId(int authId)
        {
            _prm.Add("AuthId", authId);
            var lstResult = db.FetchMultiResult<EmployeeLoginViewModel>(SPEnum.USPGETUSERBYAUTHID.ToString(), _prm);
            return lstResult;

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
