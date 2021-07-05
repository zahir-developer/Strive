using System;
using System.Collections.Generic;
using Foundation;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Utils.Employee;
using UIKit;

namespace StriveEmployee.iOS.Views
{
    public partial class DocumentsCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("DocumentsCell");
        public static readonly UINib Nib;
        List<EmployeeDocument> docList;
        NSIndexPath selectedIndex;

        static DocumentsCell()
        {
            Nib = UINib.FromName("DocumentsCell", NSBundle.MainBundle);
        }

        protected DocumentsCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public void SetData(NSIndexPath indexPath, List<EmployeeDocument> list)
        {
            docList = list;
            selectedIndex = indexPath;
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
        }
    }
}
