using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Util;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Owner;
using Strive.Core.ViewModels.Employee.Schedule;
using StriveEmployee.Android.Adapter.Schedule;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveEmployee.Android.Fragments.Schedule
{
    public class ScheduleFragment : MvxFragment<ScheduleViewModel>
    {
        private RecyclerView scheduleInfo;
        private ScheduleAdapter scheduleAdapter;
        // private Button backButton;
        private Calendar calendar;
        private int day, year, month;
        private CalendarView schedule_CalendarView;
        List<ScheduleDetailViewModel> schedules = new List<ScheduleDetailViewModel>();
        
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.Schedule_Fragment, null);
            scheduleInfo = rootView.FindViewById<RecyclerView>(Resource.Id.scheduleInfo);
            //backButton = rootView.FindViewById<Button>(Resource.Id.schedule_BackButton);
            schedule_CalendarView = rootView.FindViewById<CalendarView>(Resource.Id.schedule_Calendar);

            this.ViewModel = new ScheduleViewModel();            
            SetupCalender();
            // backButton.Click += BackButton_Click;

            schedule_CalendarView.DateChange += Schedule_CalendarView_DateChange;
            GetScheduleList();
            return rootView;
        }

        public void SetupCalender()
        {
            calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            day = calendar.Get(CalendarField.DayOfMonth);
            year = calendar.Get(CalendarField.Year);
            month = calendar.Get(CalendarField.Month);
            schedule_CalendarView.MinDate = calendar.TimeInMillis;            
        }

        private void Schedule_CalendarView_DateChange(object sender, CalendarView.DateChangeEventArgs e)
        {
            var date = e.Year + "-" + (e.Month + 1) + "-" + e.DayOfMonth + " "+ DateTime.Now.ToString("HH:mm:ss");             
            ScheduleViewModel.StartDate = date;
            ScheduleViewModel.isNoData = false;
            GetScheduleList();
        }

        //private void BackButton_Click(object sender, EventArgs e)
        //{
        //    MessengerFragment messengerFragment = new MessengerFragment();
        //    FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerFragment).Commit();
        //}

        public async Task GetScheduleList()
        {
            if (this.ViewModel == null)
            {
                ViewModel = new ScheduleViewModel();
            }
            try
            {
                await ViewModel.GetScheduleList();
                if (ViewModel.scheduleList != null && ViewModel.scheduleList.ScheduleDetailViewModel != null)
                {
                    setData(DateTime.Now.ToString("yyyy-MM-dd"));
                }
                else
                {
                    scheduleInfo.SetAdapter(null);
                    scheduleInfo.SetLayoutManager(null);
                }
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }

            //try
            //{                
            //    await this.ViewModel.GetScheduleList();
            //    if (this.ViewModel.scheduleList != null && this.ViewModel.scheduleList.ScheduleDetailViewModel != null)
            //    {
            //        scheduleAdapter = new ScheduleAdapter(Context, this.ViewModel.scheduleList);
            //        var layoutManager = new LinearLayoutManager(Context);
            //        scheduleInfo.SetLayoutManager(layoutManager);
            //        scheduleInfo.SetAdapter(scheduleAdapter);
            //    }
            //    else
            //    {
            //        scheduleInfo.SetAdapter(null);
            //        scheduleInfo.SetLayoutManager(null);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    if (ex is OperationCanceledException)
            //    {
            //        return;
            //    }
            //}
        }

        private void setData(string date)
        {
            schedules.Clear();
            foreach (var item in ViewModel.scheduleList.ScheduleDetailViewModel)
            {
                var newDate = date.Replace("/", "-");
                if (item.ScheduledDate.Substring(0, 10) == newDate.Substring(0, 10))
                {
                    schedules.Add(item);
                }
            }
            scheduleAdapter = new ScheduleAdapter(Context, schedules);
            var layoutManager = new LinearLayoutManager(Context);
            scheduleInfo.SetLayoutManager(layoutManager);
            scheduleInfo.SetAdapter(scheduleAdapter);

        }
    }
}