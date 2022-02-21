using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class ScheduleViewModel : BaseViewModel
    {
        private string date = (System.DateTime.Now).ToString("yyy-MM-dd");
        public static string StartDate { get; set; }
        public status DetailerList { get; set; }
        public static bool isNoData = false;
        public Checklist checklist { get; set; }
        public static List<checklistupdate> SelectedChecklist = new List<checklistupdate>();
        public int Roleid { get; set; }
        public string RoleName { get; set; }
        #region Properties

        public ScheduleDetail scheduleList { get; set; }
        //(System.DateTime.Now).ToString("yyy-MM-dd")
        #endregion Properties
        public ChecklistUpdateRequest checklistUpdateRequest;
        #region Commands

        public async Task GetScheduleList()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            if (StartDate!=null)
            {
                date = StartDate;
            }
            
            var result = await AdminService.GetScheduleList(new ScheduleRequest
            {
                startDate = date,
                endDate = getEndDate().ToString("yyy-MM-dd"),
                locationId = 0,
                employeeId = EmployeeTempData.EmployeeID,
            });
            if(result == null)
            {
                if (!isNoData)
                {
                    _userDialog.Toast("No relatable data");
                }
            }
            else
            {
                if(result.ScheduleDetail.ScheduleDetailViewModel == null)
                {
                    if (!isNoData)
                    {
                        _userDialog.Toast("No relatable data");
                    }                    
                }
                scheduleList = new ScheduleDetail();
                scheduleList.ScheduleDetailViewModel = new List<ScheduleDetailViewModel>();
                scheduleList.ScheduleEmployeeViewModel = new ScheduleEmployeeViewModel();
                scheduleList.ScheduleHoursViewModel = new ScheduleHoursViewModel();
                scheduleList = result.ScheduleDetail;
            }
            _userDialog.HideLoading();
        }
        public async Task GetDetailer(int employeeid,string jobdate)
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            DetailerList = await AdminService.GetEmployeeDetailer(employeeid,jobdate);
            if (DetailerList.Status.Count == 0)
            {
                _userDialog.Toast("No relatable data");
            }
            _userDialog.HideLoading();
        }


        public DateTime getEndDate()
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddYears(10).AddDays(-1); //Some future date

            return endDate;
        }

        public async Task LogoutCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }

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
                if (result != null)
                {
                    _userDialog.Alert("Successfully completed the tasks");
                }
            }
            Console.WriteLine("Task Has been completed successfully");
        }
        public async Task GetTaskList()
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            ChecklistRequest checklistRequest = new ChecklistRequest();
            checklistRequest.notificationDate = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.fff")+"Z"; //"2022-02-16T08:02:01.028Z";(Sample date with data)
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

        #endregion Commands
    }
}
