using Strive.Core.Models.Employee.Documents;
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
        public bool isAndroidFlag;
        #region Commands

        public async Task GetDocumentInfo()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetPersonalDetails(EmployeeTempData.EmployeeID);
            if (result == null || result.Employee.EmployeeDocument == null)
            {
                DocumentDetails = null;
                if (!isAndroidFlag) 
                { 
                    _userDialog.Toast("No relatable data"); 
                }
                
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

        public async Task<DownloadDocuments> DownloadDocument(int documentID, string password)
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.DownloadDocuments(documentID, password);
            DownloadDocuments docs = new DownloadDocuments();
            if (result != null)
            {
               
                docs = result;
            }
            return docs;
            _userDialog.HideLoading();
        }
        public async Task<bool> DeleteDocument(int DocumentID)
        {

            var alert = await _userDialog.ConfirmAsync("Do you want to delete this document ?");

            if(alert)
            {
                var result = await AdminService.DeleteDocuments(DocumentID);
                if (result.Result)
                {
                    _userDialog.Toast("Documents has been deleted successfully");
                }

            }
            return alert; 
        }

        #endregion Commands
    }
}
