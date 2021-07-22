using Strive.Core.Models.Employee.Documents;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Employee.MyProfile.Documents
{
    public class AddDocumentsViewModel : BaseViewModel
    {
        #region Properties

        public string filedata { get; set; }
        public string filepath { get; set; }
        public string filename { get; set; }
        public string filetype { get; set; }


        #endregion Properties


        #region Commands

        public async Task SaveDocuments()
        {
            var fileInfo = new AddDocuments();
            fileInfo.employeeDocument = new List<employeeDocument>();

            var employeeDocuments = new employeeDocument();

            employeeDocuments.employeeId = EmployeeTempData.EmployeeID;
            employeeDocuments.fileName = this.filename;
            employeeDocuments.filePath = this.filepath;
            employeeDocuments.base64 = this.filedata;
            employeeDocuments.fileType = this.filetype;
            employeeDocuments.isPasswordProtected = false;
            employeeDocuments.password = "";
            employeeDocuments.comments = "";
            employeeDocuments.isActive = true;
            employeeDocuments.isDeleted = false;
            employeeDocuments.createdDate = DateUtils.ConvertDateTimeWithZ();
            employeeDocuments.updatedDate = DateUtils.ConvertDateTimeWithZ();

            fileInfo.employeeDocument.Add(employeeDocuments);


           var result = await AdminService.SaveDocuments(fileInfo);
            if(result.Status)
            {
                _userDialog.Toast("Document uploaded successfully");
            }

        }

        #endregion Commands


    }
}
