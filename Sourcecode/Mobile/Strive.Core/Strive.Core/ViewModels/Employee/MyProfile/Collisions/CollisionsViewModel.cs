using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Resources;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee.MyProfile.Collisions
{
    public class CollisionsViewModel : BaseViewModel
    {
        #region Properties

        public PersonalDetails CollisionDetails { get; set; }
        public bool confirm { get; set; }

        #endregion Properties

        public bool isAndroid = false;
        #region Commands

        public async Task GetCollisionInfo()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetPersonalDetails(EmployeeTempData.EmployeeID);
            if (result == null || result.Employee.EmployeeCollision == null)
            {
                CollisionDetails = null;
                if (!isAndroid) 
                {
                    _userDialog.Toast("No relatable data");
                }
               
            }
            else
            {
                StoreTempData();
                CollisionDetails = new PersonalDetails();
                CollisionDetails.Employee = new Models.Employee.PersonalDetails.Employee();
                CollisionDetails.Employee.EmployeeCollision = new List<EmployeeCollision>();
                CollisionDetails.Employee.EmployeeDocument = new List<EmployeeDocument>();
                CollisionDetails.Employee.EmployeeInfo = new EmployeeInfo();
                CollisionDetails.Employee.EmployeeLocations = new List<EmployeeLocations>();
                CollisionDetails.Employee.EmployeeRoles = new List<EmployeeRoles>();
                CollisionDetails = result;
                EmployeeTempData.EmployeePersonalDetails = result;
            }

            _userDialog.HideLoading();
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

        public async Task DeleteCollisions(int liabilityID)
        {
            confirm = await _userDialog.ConfirmAsync("Are you sure you want to delete the collision document?");
            
            if (confirm)
            {
                await AdminService.DeleteCollisions(liabilityID);
            }
        }

        #endregion Commands

    }
}
