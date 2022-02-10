using System;
using System.Collections.Generic;
using System.Globalization;
using CoreGraphics;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Owner;
using Strive.Core.Utils.Employee;
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

            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Logout", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.LogoutCommand();
            };

            ParentView.Layer.CornerRadius = 5;

            ScheduleDateView.Layer.CornerRadius = 5;
            ScheduleDateView.MinimumDate = (Foundation.NSDate)System.DateTime.Today;           
            empSchedule_TableView.RegisterNibForCellReuse(empSchedule_Cell.Nib, empSchedule_Cell.Key);
            empSchedule_TableView.BackgroundColor = UIColor.Clear;
            empSchedule_TableView.ReloadData();

            getSheduleDetails();
        }
        
        partial void Schedule_Segment_Touch(UISegmentedControl sender)
        {
            var segment = Scheduledetailer_Seg_Ctrl.SelectedSegment;
            if (segment == 0)
            {
                ScheduleParentView.Hidden = false;
                DetailerView.Hidden = true;
                

                
            }
            else if (segment == 1)
            {
                ScheduleParentView.Hidden = true;
                DetailerView.Hidden = false;
                getdetailer(EmployeeTempData.EmployeeID, DateTime.Now.ToString("yyyy-MM-dd"));
                
                
                detailer_TableView.RegisterNibForCellReuse(DetailerTableCell.Nib, DetailerTableCell.Key);
                detailer_TableView.BackgroundColor = UIColor.Clear;
                detailer_TableView.ReloadData();


            }
        }

        private async void getdetailer(int empid, string jobdate)
        {
            await ViewModel.GetDetailer(empid,jobdate);
            if(ViewModel.DetailerList != null)
            {
                var empScheduleSource = new Detailer_DataSource(ViewModel.DetailerList);
                detailer_TableView.Source = empScheduleSource;
                detailer_TableView.TableFooterView = new UIView(CGRect.Empty);
                detailer_TableView.DelaysContentTouches = false;
            }
            else
            {
                detailer_TableView.Hidden = true;
            }
        }
        private async void getSheduleDetails()
        {
            try
            {
                await ViewModel.GetScheduleList();
                if (ViewModel.scheduleList != null && ViewModel.scheduleList.ScheduleDetailViewModel != null)
                {
                    setData(DateTime.Now.ToString("yyyy-MM-dd"));
                }
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }           
          
        }

        private void setData(string date)
        {
            schedules.Clear();
            if (ViewModel.scheduleList != null && ViewModel.scheduleList.ScheduleDetailViewModel != null)
            { 
                foreach (var item in ViewModel.scheduleList.ScheduleDetailViewModel)
                {
                    var newDate = date.Replace("/", "-");
                    if (item.ScheduledDate.Substring(0, 10) == newDate.Substring(0, 10))
                    {
                        schedules.Add(item);
                    }
                }
            }
            var empScheduleSource = new Emp_Schedule_DataSource(schedules);
            empSchedule_TableView.Source = empScheduleSource;
            empSchedule_TableView.TableFooterView = new UIView(CGRect.Empty);
            empSchedule_TableView.DelaysContentTouches = false;
            
            empSchedule_TableView.ReloadData();
        }

        async partial void DetailDate_Touch(UIDatePicker sender)
        {
            DateTime jobDate = (DateTime)DetailDateView.Date;
            getdetailer(EmployeeTempData.EmployeeID,jobDate.ToString("yyyy-MM-dd"));
            detailer_TableView.ReloadData();

        }

        async partial void scheduleDate_Touch(UIDatePicker sender)
        {
            var  selectedDate = ScheduleDateView.Date;
            ScheduleViewModel.StartDate = selectedDate.ToString();
            try
            {
                await ViewModel.GetScheduleList();
                setData(selectedDate.ToString());
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
            
        }
    }
}

