using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.Android.Adapter;

namespace StriveCustomer.Android.Fragments
{
    public class ScheduleAppointmentFragment : MvxFragment<ScheduleAppointmentDateViewModel>
    {
        private GridView TimeSlot_GridView;
        private CalendarView schedule_CalendarView;
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
            TimeSlot_GridView.Adapter = new ScheduleTimeSlots(Context, 20);
            schedule_CalendarView.DateChange += Schedule_CalendarView_DateChange1; ;


            return rootView;
        }

        private void Schedule_CalendarView_DateChange1(object sender, CalendarView.DateChangeEventArgs e)
        {
            string date = "";
            switch (e.Month)
            {
                case 0:
                    date = e.Year + "-" + "01" + e.DayOfMonth; 
                    break;
                case 1:
                    date = e.Year + "-" + "02" + e.DayOfMonth; 
                    break; 
                case 2:
                    date = e.Year + "-" + "03" + e.DayOfMonth; 
                    break; 
                case 3:
                    date = e.Year + "-" + "04" + e.DayOfMonth; 
                    break; 
                case 4:
                    date = e.Year + "-" + "05" + e.DayOfMonth; 
                    break; 
                case 5:
                    date = e.Year + "-" + "06" + e.DayOfMonth; 
                    break;
                case 6:
                    date = e.Year + "-" + "07" + e.DayOfMonth; 
                    break; 
                case 7:
                    date = e.Year + "-" + "08" + e.DayOfMonth; 
                    break; 
                case 8:
                    date = e.Year + "-" + "09" + e.DayOfMonth; 
                    break;  
                case 9:
                    date = e.Year + "-" + "10" + e.DayOfMonth; 
                    break;
                case 10:
                    date = e.Year + "-" + "11" + e.DayOfMonth; 
                    break;
                case 11:
                    date = e.Year + "-" + "12" + e.DayOfMonth; 
                    break;
            }

        }
    }
}