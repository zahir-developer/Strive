using Strive.Core.Models.Employee.Common;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee.MyProfile
{
    public class EmployeeInfoViewModel : BaseViewModel
    {
        #region Properties
        
        public PersonalDetails PersonalDetails { get; set; }
        public CommonCodes gender { get; set; }
        public CommonCodes ImmigrationStatus { get; set; }

        #endregion Properties

        #region Commands

        public async Task GetPersonalEmployeeInfo()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetPersonalDetails(EmployeeTempData.EmployeeID);
            if(result == null)
            {
                _userDialog.Toast("No relatable data");
            }
            else
            {
                StoreTempData();
                PersonalDetails = new PersonalDetails();
                PersonalDetails.Employee = new Models.Employee.PersonalDetails.Employee();
                PersonalDetails.Employee.EmployeeCollision = new List<EmployeeCollision>();
                PersonalDetails.Employee.EmployeeDocument = new List<EmployeeDocument>();
                PersonalDetails.Employee.EmployeeInfo = new EmployeeInfo();
                PersonalDetails.Employee.EmployeeLocations = new List<EmployeeLocations>();
                PersonalDetails.Employee.EmployeeRoles = new List<EmployeeRoles>();
                PersonalDetails = result;
                EmployeeTempData.EmployeePersonalDetails = result;
            }

            _userDialog.HideLoading();
        }
        public async Task GetGender()
        {
            var result = await AdminService.GetCommonCodes("GENDER");

            if (result == null)
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

        private void StoreTempData()
        {
            EmployeeTempData.EmployeePersonalDetails = new PersonalDetails();
            EmployeeTempData.EmployeePersonalDetails.Employee = new Models.Employee.PersonalDetails.Employee();
            EmployeeTempData.EmployeePersonalDetails.Employee.EmployeeCollision = new List<EmployeeCollision>();
            EmployeeTempData.EmployeePersonalDetails.Employee.EmployeeDocument = new List<EmployeeDocument>();
            EmployeeTempData.EmployeePersonalDetails.Employee.EmployeeInfo = new EmployeeInfo();
            EmployeeTempData.EmployeePersonalDetails.Employee.EmployeeLocations = new List<EmployeeLocations>();
            EmployeeTempData.EmployeePersonalDetails.Employee.EmployeeRoles = new List<EmployeeRoles>();
        }

        public async Task LogoutCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }

        #endregion Commands
    }
}
