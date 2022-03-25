using System;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile.Documents;
using StriveEmployee.iOS.Views.Profile;
using UIKit;

namespace StriveEmployee.iOS.Views
{
    public partial class DocumentsCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DocumentsCell");
        public static readonly UINib Nib;
        List<EmployeeDocument> docList;
        NSIndexPath selectedIndex;
        MvxViewController pdfView;
        DocumentsViewModel viewModel;
        static DocumentsCell()
        {
            Nib = UINib.FromName("DocumentsCell", NSBundle.MainBundle);
        }

        protected DocumentsCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(NSIndexPath indexPath, List<EmployeeDocument> list, MvxViewController view, DocumentsViewModel ViewModel)
        {
            docList = list;
            selectedIndex = indexPath;
            pdfView = view;
            viewModel = ViewModel;
            DocumentName.Text = list[indexPath.Row].FileName;
            if (!String.IsNullOrEmpty(list[indexPath.Row].CreatedDate))
            {
                var date = list[indexPath.Row].CreatedDate.Split("T");
                DocumentDate.Text = date[0];
            }
        }

        partial void DocView_BtnTouch(UIButton sender)
        {
            MyProfileTempData.EmployeeDocumentID = docList[selectedIndex.Row].EmployeeDocumentId;
            MyProfileTempData.DocumentPassword = "string";
            downloadDoc(selectedIndex.Row);
        }

        public async void downloadDoc(int Id)
        {
            var fileBase64 = await viewModel.DownloadDocument(docList[Id].EmployeeDocumentId, "string");
            MyProfileTempData.DocumentString = fileBase64.Document.Base64Url.ToString();
            navigate();
        }

        void navigate()
        {
            var pastTabView = new DocumentView();
            pdfView.NavigationController.PushViewController(pastTabView, true);
        }
    }
}
