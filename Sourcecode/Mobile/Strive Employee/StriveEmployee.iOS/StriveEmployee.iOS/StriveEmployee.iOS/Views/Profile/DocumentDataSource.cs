using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DeviceCheck;
using Foundation;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
using UIKit;

namespace StriveEmployee.iOS.Views
{
    public class DocumentDataSource : UITableViewSource
    {
        List<EmployeeDocument> employeeDocuments;
        DocumentsViewModel view;
        ProfileView webview;
        public DocumentDataSource(List<EmployeeDocument> documentsList, DocumentsViewModel ViewModel, ProfileView pdfView)
        {
            employeeDocuments = documentsList;
            view = ViewModel;
            webview = pdfView;
        }

        public override nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 120;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("DocumentsCell", indexPath) as DocumentsCell;
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            cell.SetData(indexPath, employeeDocuments);
            return cell;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return employeeDocuments.Count;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            MyProfileTempData.EmployeeDocumentID = employeeDocuments[indexPath.Row].EmployeeDocumentId;
            MyProfileTempData.DocumentPassword = "string";
            downloadDoc(indexPath);

            var filename = employeeDocuments[indexPath.Row].FileName;

            var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filePath = Path.Combine(documents, filename);                                                                
                                 
        }

        public async void downloadDoc(NSIndexPath indexPath)
        {
            var fileBase64 = await view.DownloadDocument(employeeDocuments[indexPath.Row].EmployeeDocumentId, "string");
            var backingFile = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "codex.txt");

            File.WriteAllBytes(backingFile, Convert.FromBase64String(fileBase64.Document.Base64Url.ToString()));

            //var viewer = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(backingFile));
            //viewer.PresentPreview(true);

            //var url = new NSUrl($"com.adobe.adobe-reader:{backingFile}");
            //UIApplication.SharedApplication.OpenUrl(url);

            //webview.loadPdf(backingFile);
        }       
    }
}
