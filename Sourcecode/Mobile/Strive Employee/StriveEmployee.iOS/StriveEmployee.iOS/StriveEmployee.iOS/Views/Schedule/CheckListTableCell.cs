using System;
using System.Linq;
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
        public Checklist Checklist = new Checklist();
        public NSIndexPath index;
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
            if (ScheduleViewModel.SelectedChecklist.Count != 0)
            {
                var test = ScheduleViewModel.SelectedChecklist.Find(x => x.CheckListEmployeeId == item.ChecklistNotification[index.Row].CheckListEmployeeId);
                if (test !=null)
                {
                    TaskCheck.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                }
                else
                {
                    TaskCheck.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                }
            }
        }
        public void updateServices(Checklist item,NSIndexPath indexPath)
        {
            Checklist = item;
            index = indexPath;
            if (ScheduleViewModel.SelectedChecklist.Any(x => x.CheckListEmployeeId == item.ChecklistNotification[indexPath.Row].CheckListEmployeeId))
            {
                TaskCheck.SetImage(UIImage.FromBundle("icon-unchecked-round"), UIControlState.Normal);
                var element = ScheduleViewModel.SelectedChecklist.Find(x => x.CheckListEmployeeId == item.ChecklistNotification[indexPath.Row].CheckListEmployeeId);
                ScheduleViewModel.SelectedChecklist.Remove(element);
          
            }
            else
            {
                TaskCheck.SetImage(UIImage.FromBundle("icon-checked-round"), UIControlState.Normal);
                var checklist = new checklistupdate();
                checklist.CheckListEmployeeId = item.ChecklistNotification[indexPath.Row].CheckListEmployeeId;
                //checklist.CheckListId = item.ChecklistNotification[indexPath.Row].CheckListId;
                checklist.IsCompleted = true;
                checklist.NotificationDate = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.fff") + "Z";
                checklist.UserId= EmployeeTempData.EmployeeID;
                //checklist.CheckListNotificationId = item.ChecklistNotification[indexPath.Row].ChecklistNotificationId;
                ScheduleViewModel.SelectedChecklist.Add(checklist);

            }
        }
       
    }
}
