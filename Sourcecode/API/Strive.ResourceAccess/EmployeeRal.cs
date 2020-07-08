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
        public List<Employee> GetEmployeeDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Employee> lstEmployee = new List<Employee>();
            lstEmployee = db.FetchRelation3<Employee, EmployeeAddress, EmployeeDetail,EmployeeRole>(SPEnum.USPGETEMPLOYEE.ToString(),dynParams);
            return lstEmployee;
        }

        public bool SaveEmployeeDetails(List<EmployeeTable> lstEmployee)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<EmployeeInformation> lstEmp = new List<EmployeeInformation>();
            var empReg = lstEmployee.FirstOrDefault();
            lstEmp.Add(new EmployeeInformation
            {
                EmployeeId = empReg.EmployeeId,
                FirstName = empReg.FirstName,
                MiddleName = empReg.MiddleName,
                LastName = empReg.LastName,
                Gender = empReg.Gender,
                MaritalStatus = empReg.MaritalStatus,
                IsCitizen = empReg.IsCitizen,
                AlienNo = empReg.AlienNo,
                BirthDate = empReg.BirthDate,
                ImmigrationStatus = empReg.ImmigrationStatus,

            });
            dynParams.Add("@tvpEmployee", lstEmp.ToDataTable().AsTableValuedParameter("tvpEmployee"));
            dynParams.Add("@tvpEmployeeAddress", empReg.EmployeeAddress.ToDataTable().AsTableValuedParameter("tvpEmployeeAddress"));
            dynParams.Add("@tvpEmployeeDetail", empReg.EmployeeDetail.ToDataTable().AsTableValuedParameter("tvpEmployeeDetail"));
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
