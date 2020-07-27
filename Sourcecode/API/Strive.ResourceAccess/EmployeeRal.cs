using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Strive.BusinessEntities.Employee;
using Strive.BusinessEntities.Code;

namespace Strive.ResourceAccess
{
    public class EmployeeRal : RalBase
    {
        public EmployeeRal(ITenantHelper tenant) : base(tenant) { }

        public List<EmployeeView> GetEmployeeDetails()
        {
            ///... To get Employee Details by EmployeeId field
            var empDetails = db.GetListByFkId<EmployeeDetail>(1, "EmployeeId");

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
    }
}
