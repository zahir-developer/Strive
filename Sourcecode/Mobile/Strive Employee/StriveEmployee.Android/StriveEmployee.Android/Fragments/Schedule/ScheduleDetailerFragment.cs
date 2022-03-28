using System;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Util;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels;
using Strive.Core.ViewModels.Employee.Schedule;
using StriveEmployee.Android.Adapter.Schedule;
using StriveEmployee.Android.Views;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveEmployee.Android.Fragments.Schedule
{
    public class ScheduleDetailerFragment : MvxFragment<ScheduleDetailerViewModel>
    {

        private Calendar calendar;
        private int day, year, month;
        private CalendarView scheduleDetail_CalendarView;
        private LinearLayout detailer_Layout;
        private View layout;
        private View rootView;        
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            rootView = this.BindingInflate(Resource.Layout.ScheduleDetailer_Fragment, null);
            scheduleDetail_CalendarView = rootView.FindViewById<CalendarView>(Resource.Id.scheduleDetail_Calendar);
            detailer_Layout = rootView.FindViewById<LinearLayout>(Resource.Id.scheduleDetailerInfo);
            //this.ViewModel = new ScheduleDetailerViewModel();            
            calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            day = calendar.Get(CalendarField.DayOfMonth);
            year = calendar.Get(CalendarField.Year);
            month = calendar.Get(CalendarField.Month);
            // scheduleDetail_CalendarView.MaxDate = calendar.TimeInMillis;
            scheduleDetail_CalendarView.DateChange += ScheduleDetail_CalendarView_DateChange;
            
            //GetScheduleDetailList(EmployeeTempData.EmployeeID, DateTime.Now.ToString("yyyy-MM-dd"));
            return rootView;
        }       

        private void ScheduleDetail_CalendarView_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            DashboardView.date = e.Year + "-" + (e.Month + 1) + "-" + e.DayOfMonth;
            detailer_Layout.RemoveAllViews();
            GetScheduleDetailList(EmployeeTempData.EmployeeID, DashboardView.date);
        }
        
        public async void GetScheduleDetailList(int empID,string jobDate)
        {
            if (this.ViewModel == null) 
            {
                ViewModel = new ScheduleDetailerViewModel();
            }
            try
            {
                if (detailer_Layout != null) 
                { 
                    detailer_Layout.RemoveAllViews(); 
                }                
                await ViewModel.GetDetailer(empID, jobDate);
                if (ViewModel.DetailerList != null && ViewModel.DetailerList.Status.Count != 0)
                {
                    detailer_Layout.Visibility = ViewStates.Visible;
                    foreach (var data in ViewModel.DetailerList.Status)
                    {
                        if (Context != null)
                        {
                            layout = LayoutInflater.From(Context).Inflate(Resource.Layout.ScheduleDetailer_ItemView, detailer_Layout, false);
                            var TicketNumber = layout.FindViewById<TextView>(Resource.Id.TicketNumber_TextView);
                            var VehicleDetails = layout.FindViewById<TextView>(Resource.Id.VehicleInfo_TextView);
                            var DetailService = layout.FindViewById<TextView>(Resource.Id.DetailService_TextView);
                            var AdditionalService = layout.FindViewById<TextView>(Resource.Id.AdditionalService_TextView);
                            var TimeIn = layout.FindViewById<TextView>(Resource.Id.TimeIn_TextView);
                            var TimeOut = layout.FindViewById<TextView>(Resource.Id.TimeOut_TextView);
                            TicketNumber.Text = data.TicketNumber;
                            VehicleDetails.Text = data.VehicleMake + "/" + data.VehicleModel + "/" + data.VehicleColor;
                            DetailService.Text = data.DetailService;
                            AdditionalService.Text = data.AdditionalService.Replace(" ", "");
                            TimeIn.Text = DateTime.Parse(data.TimeIn).TimeOfDay.ToString();
                            TimeOut.Text = DateTime.Parse(data.EstimatedTimeOut).TimeOfDay.ToString();

                            detailer_Layout.AddView(layout);

                        }
                    }

                }
                else
                {
                    detailer_Layout.Visibility = ViewStates.Gone;
                    var alert=Snackbar.Make(rootView,"No relatable data!",BaseTransientBottomBar.LengthShort);
                    alert.Show();
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
    }
}