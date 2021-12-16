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
using Newtonsoft.Json;
using System.Collections;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel.Employee;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.DTO;

namespace Strive.ResourceAccess
{
    public class EmployeeRal : RalBase
    {
        public EmployeeRal(ITenantHelper tenant) : base(tenant) { }

        public EmployeeDetailViewModel GetEmployeeById(int employeeId)
        {
            _prm.Add("EmployeeId", employeeId);
            var lstResult = db.FetchMultiResult<EmployeeDetailViewModel>(EnumSP.Employee.USPGETEMPLOYEEBYID.ToString(), _prm);
            return lstResult;
        }

        public EmployeeListViewModel GetEmployeeList()
        {
            return db.FetchMultiResult<EmployeeListViewModel>(EnumSP.Employee.USPGETEMPLOYEELIST.ToString(), _prm);
        }

        public EmployeeViewModel GetAllEmployeeDetail(SearchDto searchDto)
        {

            _prm.Add("@PageNo", searchDto.PageNo);
            _prm.Add("@PageSize", searchDto.PageSize);
            _prm.Add("@Query", searchDto.Query);
            _prm.Add("@SortOrder", searchDto.SortOrder);
            _prm.Add("@SortBy", searchDto.SortBy);
            _prm.Add("@Status", searchDto.Status);
            return db.FetchMultiResult<EmployeeViewModel>(EnumSP.Employee.USPGETALLEMPLOYEEDETAIL.ToString(), _prm);
        }

        public int AddEmployee(EmployeeModel employee)
        {
            return dbRepo.InsertPK(employee, "EmployeeId");
        }

        public bool UpdateEmployee(EmployeeModel employee)
        {
            return dbRepo.UpdatePc(employee);
        }

        public List<RoleViewModel> GetAllEmployeeRoles()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<RoleViewModel> lstEmployee = new List<RoleViewModel>();
            lstEmployee = db.Fetch<RoleViewModel>(EnumSP.Employee.USPGETEMPLOYEEROLES.ToString(), dynParams);
            return lstEmployee;
        }

        public EmployeeLoginViewModel GetEmployeeByAuthId(int authId)
        {
            _prm.Add("AuthId", authId);
            var lstResult = db.FetchMultiResult<EmployeeLoginViewModel>(EnumSP.Employee.USPGETUSERBYAUTHID.ToString(), _prm);
            return lstResult;
        }

        public bool DeleteEmployeeDetails(int employeeId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@EmployeeId", employeeId);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Employee.USPDELETEEMPLOYEE.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public EmployeeRoleViewModel GetEmployeeRoleById(int employeeId)
        {
            _prm.Add("@EmployeeId", employeeId);
            var lstResult = db.FetchMultiResult<EmployeeRoleViewModel>(EnumSP.Employee.USPGETEMPLOYEEROLEBYID.ToString(), _prm);
            return lstResult;
        }
        public List<EmployeeNameDto> GetAllEmployeeName(int locationId)
        {
            _prm.Add("@LocationId", locationId);
            var lstResult = db.Fetch<EmployeeNameDto>(EnumSP.Employee.USPGETALLEMPLOYEE.ToString(), _prm);
            return lstResult;
        }

        public List<EmployeeHourlyRateDto> GetEmployeeHourlyRateById(int employeeId)
        {
            _prm.Add("EmployeeId", employeeId);
            var lstResult = db.Fetch<EmployeeHourlyRateDto>(EnumSP.Employee.USPGETEMPLOYEEHOURLYRATEBYID.ToString(), _prm);
            return lstResult;
        }
        public EmployeePayCheckViewModel GetEmployeePayCheck(EmployeePayCheckDto searchDto)
        {

            _prm.Add("@EmployeeId", searchDto.EmployeeId);
            _prm.Add("@Month", searchDto.Month);
            _prm.Add("@Year", searchDto.Year);
            return db.FetchMultiResult<EmployeePayCheckViewModel>(EnumSP.Employee.USPGETEMPLOYEEPAYCHECK.ToString(), _prm);
        }

        public bool UpdateEmployeeAddressIsNotified(int employeeAddressId, bool IsNotified)
        {
            _prm.Add("@EmployeeAddressId", employeeAddressId);
            _prm.Add("@IsNotified", IsNotified);
            db.Save(SPEnum.USPUPDATEEMPLOYEEADDRESSISNOTIFIED.ToString(), _prm);
            return true;
        }
    }
}

