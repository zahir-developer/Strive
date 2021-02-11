using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Resources;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee.MyProfile.Documents
{
    public class DocumentsViewModel : BaseViewModel
    {
        #region Properties

        public PersonalDetails DocumentDetails { get; set; }

        #endregion Properties

        #region Commands

        public async Task GetDocumentInfo()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetPersonalDetails(EmployeeTempData.EmployeeID);
            if (result == null)
            {
                DocumentDetails = null;
                _userDialog.Toast("No relatable data");
            }
            else
            {
                StoreTempData();
                DocumentDetails = new PersonalDetails();
                DocumentDetails.Employee = new Models.Employee.PersonalDetails.Employee();
                DocumentDetails.Employee.EmployeeCollision = new List<EmployeeCollision>();
                DocumentDetails.Employee.EmployeeDocument = new List<EmployeeDocument>();
                DocumentDetails.Employee.EmployeeInfo = new EmployeeInfo();
                DocumentDetails.Employee.EmployeeLocations = new List<EmployeeLocations>();
                DocumentDetails.Employee.EmployeeRoles = new List<EmployeeRoles>();
                DocumentDetails = result;
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

        public async Task DownloadDocument()
        {
            var result = await AdminService.DownloadDocuments(229,"string");
            if(result != null)
            {

            }
        }

            #endregion Commands
        }
}
