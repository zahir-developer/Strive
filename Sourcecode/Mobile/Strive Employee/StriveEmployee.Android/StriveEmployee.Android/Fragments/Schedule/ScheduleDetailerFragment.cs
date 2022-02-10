using System;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Java.Util;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Employee.Schedule;
using StriveEmployee.Android.Adapter.Schedule;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveEmployee.Android.Fragments.Schedule
{
    public class ScheduleDetailerFragment : MvxFragment
    {
        private RecyclerView scheduleDetailInfo;
        private ScheduleAdapter scheduleAdapter;
        //private Button backButton;
        private Calendar calendar;
        private int day, year, month;
        private CalendarView scheduleDetail_CalendarView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.ScheduleDetailer_Fragment, null);
            scheduleDetailInfo = rootView.FindViewById<RecyclerView>(Resource.Id.scheduleInfo);
            //backButton = rootView.FindViewById<Button>(Resource.Id.schedule_BackButton);
            scheduleDetail_CalendarView = rootView.FindViewById<CalendarView>(Resource.Id.scheduleDetail_Calendar);

            //this.ViewModel = new ScheduleViewModel();

            //backButton.Click += BackButton_Click;
            calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            day = calendar.Get(CalendarField.DayOfMonth);
            year = calendar.Get(CalendarField.Year);
            month = calendar.Get(CalendarField.Month);
            scheduleDetail_CalendarView.MinDate = calendar.TimeInMillis;
           // GetScheduleDetailList();
            return rootView;
        }

        //private void BackButton_Click(object sender, EventArgs e)
        //{
        //    MessengerFragment messengerFragment = new MessengerFragment();
        //    FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, messengerFragment).Commit();
        //}

        public async void GetScheduleDetailList()
        {
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
    }
}