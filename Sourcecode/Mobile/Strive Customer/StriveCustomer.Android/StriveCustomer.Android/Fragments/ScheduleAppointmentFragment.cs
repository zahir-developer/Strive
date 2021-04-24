﻿using System;
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
        private Button Next_Button;
        private Button Back_Button;
        private ScheduleFragment scheduleFragment;
        private ScheduleLocationsFragment locationFragment;
        private SchedulePreviewFragment previewFragment;
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
            Next_Button = rootView.FindViewById<Button>(Resource.Id.scheduleAppointment_NextButton);
            Next_Button.Click += Next_Button_Click;
            Back_Button = rootView.FindViewById<Button>(Resource.Id.scheduleAppoitment_BackButton); 
            Back_Button.Click += Back_Button_Click;
            schedule_CalendarView.DateChange += Schedule_CalendarView_DateChange1;
            Cancel_Button.Click += Cancel_Button_Click;

            CurrentDateSlots();

            return rootView;
        }

        private void Back_Button_Click(object sender, EventArgs e)
        {
            locationFragment = new ScheduleLocationsFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, locationFragment).Commit();
        }

        private void Next_Button_Click(object sender, EventArgs e)
        {
            if (this.ViewModel.checkSelectedTime() && this.ViewModel.checkSelectedDate()) 
            {
                previewFragment = new SchedulePreviewFragment();
                AppCompatActivity activity = (AppCompatActivity)this.Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, previewFragment).Commit();

            }            
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            scheduleFragment = new ScheduleFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, scheduleFragment).Commit();
        }

        private async void CurrentDateSlots()
        {
            var dt = DateTime.Now;
            GetAvailableSlot(dt.Month, dt.Year, dt.Day);
        }

        private async void Schedule_CalendarView_DateChange1(object sender, CalendarView.DateChangeEventArgs e)
        {
            GetAvailableSlot(e.Month, e.Year, e.DayOfMonth);
        }

       private async void GetAvailableSlot(int month, int year, int day)
        {
            string date = "";
           
            switch (month)
            {
                case 0:
                    date = year + "-" + "01" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "January";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 1:
                    date = year + "-" + "02" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "February";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 2:
                    date = year + "-" + "03" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "March";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 3:
                    date = year + "-" + "04" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "April";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 4:
                    date = year + "-" + "05" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "May";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 5:
                    date = year + "-" + "06" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "June";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 6:
                    date = year + "-" + "07" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "July";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 7:
                    date = year + "-" + "08" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "August";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 8:
                    date = year + "-" + "09" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "September";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 9:
                    date = year + "-" + "10" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "October";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 10:
                    date = year + "-" + "11" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "November";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 11:
                    date = year + "-" + "12" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "December";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
            }
            date = date + "T00:00:00.000Z";
            this.ViewModel.checkDate = CustomerScheduleInformation.ScheduleDate + "/" + CustomerScheduleInformation.ScheduleMonth + "/" + CustomerScheduleInformation.ScheduleYear;
            //await this.ViewModel.GetSlotAvailability(CustomerScheduleInformation.ScheduleLocationCode, date);
            await this.ViewModel.GetSlotAvailability(8, date);
            if (this.ViewModel.ScheduleSlotInfo != null && this.ViewModel.ScheduleSlotInfo.GetTimeInDetails.Count > 0)
            {
                TimeSlot_GridView.Adapter = new ScheduleTimeSlots(Context, this.ViewModel.ScheduleSlotInfo);
            }
        }
    }
}