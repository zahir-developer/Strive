using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Employee.CheckList;
using Strive.Core.Models.Employee.Detailer;
using Strive.Core.Models.Owner;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;

namespace Strive.Core.ViewModels.Employee.Schedule
{
    public class ScheduleCheckListViewModel : BaseViewModel
    {
       
        public static bool isNoData = false;
        public Checklist checklist { get; set; }
        public int Roleid { get; set; }
        public string RoleName { get; set; }
        public static List<checklistupdate> SelectedChecklist = new List<checklistupdate>();
        #region Properties

        public ScheduleDetail scheduleList { get; set; }
        //(System.DateTime.Now).ToString("yyy-MM-dd")
        #endregion Properties
        public ChecklistUpdateRequest checklistUpdateRequest;
        #region Commands 
       
        public async Task FinishTask()
        {
            //_userDialog.Alert("Do you want to complete the Tasks");
            checklistUpdateRequest = new ChecklistUpdateRequest();
            if (SelectedChecklist.Count == 0)
            {
                _userDialog.Alert("No Tasks Selected");
            }
            else
            {
                checklistUpdateRequest.CheckListNotification = SelectedChecklist;
                var result = await AdminService.FinishCheckList(checklistUpdateRequest);
                if (result.ChecklistNotification == true)
                {
                    SelectedChecklist.Clear();
                    _userDialog.Alert("Successfully completed the tasks");
                }
            }

            //Console.WriteLine("Task Has been completed successfully");
        }
        public async Task GetTaskList()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            ChecklistRequest checklistRequest = new ChecklistRequest();
            checklistRequest.notificationDate = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.fff") + "Z";
            checklistRequest.role = Roleid;
            checklistRequest.EmployeeId = EmployeeTempData.EmployeeID;
            var result = await AdminService.GetCheckList(checklistRequest);
            if (result != null)
            {
                checklist = result;
            }
            else
            {
                _userDialog.Alert("Unable to fetch the data");
            }

            _userDialog.HideLoading();
        }
        public async Task LogoutCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }

        #endregion Commands
    }
}
