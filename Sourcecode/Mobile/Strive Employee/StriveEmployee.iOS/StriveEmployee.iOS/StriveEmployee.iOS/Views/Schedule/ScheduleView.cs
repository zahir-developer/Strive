using System;
using System.Collections.Generic;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Owner;
using Strive.Core.ViewModels.Employee.Schedule;
using StriveEmployee.iOS.UIUtils;   
using UIKit;

namespace StriveEmployee.iOS.Views.Schedule
{
    public partial class ScheduleView : MvxViewController<ScheduleViewModel>
    {
        List<ScheduleDetailViewModel> schedules = new List<ScheduleDetailViewModel>();
        public ScheduleView() : base("ScheduleView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            InitialSetup();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        private void InitialSetup()
        {
            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Schedule";

            ScheduleParentView.Layer.CornerRadius = 5;
            ScheduleDateView.Layer.CornerRadius = 5;                       

            empSchedule_TableView.RegisterNibForCellReuse(empSchedule_Cell.Nib, empSchedule_Cell.Key);
            empSchedule_TableView.BackgroundColor = UIColor.Clear;
            empSchedule_TableView.ReloadData();

            getSheduleDetails();
        }

        private async void getSheduleDetails()
        {
            await ViewModel.GetScheduleList();

            if(ViewModel.scheduleList != null)
            {
                setData(DateTime.Now.ToString("yyyy-MM-dd"));
            }
        }

        private void setData(string date)
        {
            schedules.Clear();
            foreach(var item in ViewModel.scheduleList.ScheduleDetailViewModel)
            {
                var newDate = date.Replace("/", "-");                
                if(item.ScheduledDate.Substring(0,10) == newDate.Substring(0,10))
                {
                    schedules.Add(item);
                }
            }

            var empScheduleSource = new Emp_Schedule_DataSource(schedules);
            empSchedule_TableView.Source = empScheduleSource;
            empSchedule_TableView.TableFooterView = new UIView(CGRect.Empty);
            empSchedule_TableView.DelaysContentTouches = false;
            empSchedule_TableView.ReloadData();
        }

        partial void scheduleDate_Touch(UIDatePicker sender)
        {
            var selectedDate = ScheduleDateView.Date;

            setData(selectedDate.ToString());
        }
    }
}

