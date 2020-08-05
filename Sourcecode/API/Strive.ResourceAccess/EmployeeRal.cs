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
using FastDeepCloner;
using Newtonsoft.Json;
using System.Collections;
using Strive.BusinessEntities.ViewModel.Employee;
using Strive.BusinessEntities.Model;

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

        public List<Code> GetAllEmployeeRoles()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Code> lstEmployee = new List<Code>();
            lstEmployee = db.Fetch<Code>(SPEnum.USPGETEMPLOYEEROLES.ToString(), dynParams);
            return lstEmployee;
        }

        public bool SaveEmployeeDetails(EmployeeModel employee)
        {
            return dbRepo.SavePc(employee, "EmployeeId");
        }

        public EmployeeLoginViewModel GetEmployeeByAuthId(int authId)
        {
            _prm.Add("AuthId", authId);
            var lstResult = db.FetchMultiResult<EmployeeLoginViewModel>(SPEnum.USPGETUSERBYAUTHID.ToString(), _prm);
            return lstResult;


            //DynamicParameters dynParams = new DynamicParameters();
            //dynParams.Add("AuthId", authId);
            //lstEmployee = db.FetchRelation1<EmployeeView, EmployeeRole>(SPEnum.USPGETUSERBYAUTHID.ToString(), dynParams);
            //return lstEmployee.FirstOrDefault();
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
