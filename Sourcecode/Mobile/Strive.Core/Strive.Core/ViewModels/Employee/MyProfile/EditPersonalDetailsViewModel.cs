using Strive.Core.Models.Employee.Common;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee.MyProfile
{
    public class EditPersonalDetailsViewModel : BaseViewModel
    {

        #region Properties

        public CommonCodes gender { get; set; }
        public CommonCodes ImmigrationStatus { get; set; }
       // public EmployeeUpdate employeeUpdate { get; set; }

        #endregion Properties



        #region Commands

        public async Task GetGender()
        {
            var result = await AdminService.GetCommonCodes("GENDER");

            if(result == null)
            {
                gender = null;
            }
            else
            {
                gender = new CommonCodes();
                gender.Codes = new List<Codes>();
                gender = result;
            }
        }

        public async Task GetImmigrationStatus()
        {
            var result = await AdminService.GetCommonCodes("IMMIGRATIONSTATUS");

            if (result == null)
            {
                ImmigrationStatus = null;
            }
            else
            {
                ImmigrationStatus = new CommonCodes();
                ImmigrationStatus.Codes = new List<Codes>();
                ImmigrationStatus = result;
            }
        }

        public async Task<bool> SavePersonalInfo()
        {
            UpdatePersonalDetails updatePersonalDetails = new UpdatePersonalDetails();
            updatePersonalDetails.employee = new employee();
            updatePersonalDetails.employee.employeeId = EmployeeTempData.EmployeeID;
            updatePersonalDetails.employee.firstName = EmployeePersonalDetails.FirstName;
            updatePersonalDetails.employee.lastName = EmployeePersonalDetails.LastName;
            updatePersonalDetails.employee.gender = EmployeePersonalDetails.GenderCodeID;
            updatePersonalDetails.employee.ssNo = EmployeePersonalDetails.SSN;
            updatePersonalDetails.employee.maritalStatus = 0;
            updatePersonalDetails.employee.isCitizen = true;
            updatePersonalDetails.employee.alienNo = "string";
            updatePersonalDetails.employee.birthDate = DateUtils.ConvertDateTimeWithZ();
            updatePersonalDetails.employee.workPermit = DateUtils.ConvertDateTimeWithZ();
            updatePersonalDetails.employee.immigrationStatus = EmployeePersonalDetails.ImmigrationCodeID;
            updatePersonalDetails.employee.isActive = true;
            updatePersonalDetails.employee.isDeleted = false;
            updatePersonalDetails.employee.createdBy = 0;
            updatePersonalDetails.employee.createdDate = DateUtils.ConvertDateTimeWithZ(); 
            updatePersonalDetails.employee.updatedBy = 0;
            updatePersonalDetails.employee.updatedDate = DateUtils.ConvertDateTimeWithZ();


            updatePersonalDetails.employeeAddress = new employeeAddress();
            updatePersonalDetails.employeeAddress.employeeAddressId = EmployeePersonalDetails.AddressID;
            updatePersonalDetails.employeeAddress.employeeId = EmployeeTempData.EmployeeID;
            updatePersonalDetails.employeeAddress.address1 = EmployeePersonalDetails.Address;
            updatePersonalDetails.employeeAddress.address2 = EmployeePersonalDetails.Address;
            updatePersonalDetails.employeeAddress.phoneNumber = EmployeePersonalDetails.ContactNumber;
            updatePersonalDetails.employeeAddress.phoneNumber2 = EmployeePersonalDetails.ContactNumber;
            updatePersonalDetails.employeeAddress.email = EmployeeLoginDetails.LoginID;
            updatePersonalDetails.employeeAddress.city = 1;
            updatePersonalDetails.employeeAddress.state = 1;
            updatePersonalDetails.employeeAddress.zip = null;
            updatePersonalDetails.employeeAddress.country = 1;
            updatePersonalDetails.employeeAddress.isActive = EmployeeLoginDetails.IsActive;
            updatePersonalDetails.employeeAddress.isDeleted = false;
            updatePersonalDetails.employeeAddress.createdBy = 0;
            updatePersonalDetails.employeeAddress.createdDate = DateUtils.ConvertDateTimeWithZ();
            updatePersonalDetails.employeeAddress.updatedBy = 0;
            updatePersonalDetails.employeeAddress.updatedDate = DateUtils.ConvertDateTimeWithZ();


            var result = await AdminService.UpdateEmployeePersonalDetails(updatePersonalDetails);
            if(result != null)
            {
                _userDialog.Toast("Personal info save successful");
            }
            return result.Status;
        }


        #endregion Commands

    }
}
