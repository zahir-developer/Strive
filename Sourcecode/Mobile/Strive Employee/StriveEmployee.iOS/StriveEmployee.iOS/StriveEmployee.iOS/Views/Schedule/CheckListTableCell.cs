using System;

using Foundation;
using Strive.Core.Models.Employee.CheckList;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.Schedule;
using UIKit;


namespace StriveEmployee.iOS.Views.Schedule
{
    public partial class CheckListTableCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("CheckListTableCell");
        public static readonly UINib Nib;
        public bool ischecked = false;
        static CheckListTableCell()
        {
            Nib = UINib.FromName("CheckListTableCell", NSBundle.MainBundle);
        }

        protected CheckListTableCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        public void updateData(Checklist item, NSIndexPath index)
        {
            TaskContainer.Layer.CornerRadius = 5;
            Task.Text = item.ChecklistNotification[index.Row].Name;
            Time.Text = "Time: " + item.ChecklistNotification[index.Row].NotificationTime;
        }
        public void updateServices(Checklist item,NSIndexPath indexPath)
        {
            
            if (TaskCheck.CurrentImage == UIImage.FromBundle("icon-checked-round"))
            {
                TaskCheck.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
            }
            else
            {
                TaskCheck.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                var checklist = new checklistupdate();
                checklist.CheckListEmployeeId = item.ChecklistNotification[indexPath.Row].CheckListEmployeeId;
                //checklist.CheckListId = item.ChecklistNotification[indexPath.Row].CheckListId;
                checklist.IsCompleted = true;
                checklist.NotificationDate = "2022-02-16T08:02:01.028Z";
                checklist.UserId= EmployeeTempData.EmployeeID;
                ScheduleViewModel.SelectedChecklist.Add(checklist);

            }
        }
    }
}
