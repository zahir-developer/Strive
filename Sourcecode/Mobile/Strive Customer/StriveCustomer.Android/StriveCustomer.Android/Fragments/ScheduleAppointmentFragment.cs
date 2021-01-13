using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    public class ScheduleAppointmentFragment : MvxFragment<ScheduleAppointmentDateViewModel>
    {
        private GridView TimeSlot_GridView;
        private CalendarView schedule_CalendarView;
        private Button Cancel_Button;
        private ScheduleFragment scheduleFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.ScheduleAppointmentDateFragment, null);
            this.ViewModel = new ScheduleAppointmentDateViewModel();
            schedule_CalendarView = rootView.FindViewById<CalendarView>(Resource.Id.scheduleAppointment_Calendar);
            TimeSlot_GridView = rootView.FindViewById<GridView>(Resource.Id.slotAvailable_gridview);
            Cancel_Button = rootView.FindViewById<Button>(Resource.Id.cancelAppointment_Button);
            schedule_CalendarView.DateChange += Schedule_CalendarView_DateChange1; ;

            Cancel_Button.Click += Cancel_Button_Click;
            return rootView;
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            scheduleFragment = new ScheduleFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, scheduleFragment).Commit();
        }

        private async void Schedule_CalendarView_DateChange1(object sender, CalendarView.DateChangeEventArgs e)
        {
            string date = "";
            switch (e.Month)
            {
                case 0:
                    date = e.Year + "-" + "01" + "-" + e.DayOfMonth; 
                    break;
                case 1:
                    date = e.Year + "-" + "02" + "-" + e.DayOfMonth; 
                    break; 
                case 2:
                    date = e.Year + "-" + "03" + "-" + e.DayOfMonth; 
                    break; 
                case 3:
                    date = e.Year + "-" + "04" + "-" + e.DayOfMonth; 
                    break; 
                case 4:
                    date = e.Year + "-" + "05" + "-" + e.DayOfMonth; 
                    break; 
                case 5:
                    date = e.Year + "-" + "06" + "-" + e.DayOfMonth; 
                    break;
                case 6:
                    date = e.Year + "-" + "07" + "-" + e.DayOfMonth; 
                    break; 
                case 7:
                    date = e.Year + "-" + "08" + "-" + e.DayOfMonth; 
                    break; 
                case 8:
                    date = e.Year + "-" + "09" + "-" + e.DayOfMonth; 
                    break;  
                case 9:
                    date = e.Year + "-" + "10" + "-" + e.DayOfMonth; 
                    break;
                case 10:
                    date = e.Year + "-" + "11" + "-"+ e.DayOfMonth; 
                    break;
                case 11:
                    date = e.Year + "-" + "12" + "-" + e.DayOfMonth; 
                    break;
            }
            date = date + "T00:00:00.000Z";
            await this.ViewModel.GetSlotAvailability(CustomerScheduleInformation.ScheduleLocationCode, date);

            if(this.ViewModel.ScheduleSlotInfo != null && this.ViewModel.ScheduleSlotInfo.GetTimeInDetails.Count > 0)
            {
                TimeSlot_GridView.Adapter = new ScheduleTimeSlots(Context, this.ViewModel.ScheduleSlotInfo);
            }
        }
    }
}