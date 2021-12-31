using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using DeviceCheck;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
using StriveEmployee.iOS.Views.Profile;
using UIKit;

namespace StriveEmployee.iOS.Views
{
    public class DocumentDataSource : UITableViewSource
    {
        List<EmployeeDocument> employeeDocuments;
        DocumentsViewModel view;
        MvxViewController webview;
        public DocumentDataSource(List<EmployeeDocument> documentsList, DocumentsViewModel ViewModel, MvxViewController pdfView)
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
            cell.SetData(indexPath, employeeDocuments, webview, view);
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
        }

        public async void downloadDoc(NSIndexPath indexPath)
        {
            var fileBase64 = await view.DownloadDocument(employeeDocuments[indexPath.Row].EmployeeDocumentId, "string");
            MyProfileTempData.DocumentString = fileBase64.Document.Base64Url.ToString();
            navigate();                       
        }

        void navigate()
        {
            var pastTabView = new DocumentView();
            webview.NavigationController.PushViewController(pastTabView, true);
        }
    }

}
