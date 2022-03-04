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
using Java.Util;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.ViewModels.Customer.Schedule;
using StriveCustomer.Android.Adapter;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveCustomer.Android.Fragments
{
    public class ScheduleAppointmentFragment : MvxFragment<ScheduleAppointmentDateViewModel>
    {
        private GridView TimeSlot_GridView;
        private TextView SlotTxtView;
        private CalendarView schedule_CalendarView;
        private Button Cancel_Button;
        private Button Next_Button;
        private Button Back_Button;
        private ScheduleFragment scheduleFragment;
        private ScheduleSelectServiceFragment selectServiceFragment;
        private SchedulePreviewFragment previewFragment;
        private int day, year, month;
        private Calendar calendar;
        private DateTime dt;
        public AvailableScheduleSlots updatedScheduleSlotInfo { get; set; }

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
            SlotTxtView = rootView.FindViewById<TextView>(Resource.Id.slotTextView);
            Back_Button.Click += Back_Button_Click;
            schedule_CalendarView.DateChange += Schedule_CalendarView_DateChange1;
            Cancel_Button.Click += Cancel_Button_Click;
            calendar = Calendar.GetInstance(Java.Util.TimeZone.Default);
            day = calendar.Get(CalendarField.DayOfMonth);
            year = calendar.Get(CalendarField.Year);
            month = calendar.Get(CalendarField.Month);
            schedule_CalendarView.MinDate = calendar.TimeInMillis;
            CurrentDateSlots();
            TimeSlot_GridView.ScrollStateChanged += OnGridViewScrollStateChanged;

            return rootView;
        }

        private void OnGridViewScrollStateChanged(object sender, AbsListView.ScrollStateChangedEventArgs e)
        {
            var adapter = (ScheduleTimeSlots)TimeSlot_GridView.Adapter;
            if (e.ScrollState != ScrollState.Idle)
            {
                adapter.IsScrolling = true;
            }
            else
            {
                adapter.IsScrolling = false;
                adapter.NotifyDataSetChanged();
            }
        }

        private void Back_Button_Click(object sender, EventArgs e)
        {
            selectServiceFragment = new ScheduleSelectServiceFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, selectServiceFragment).Commit();
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

        private void CurrentDateSlots()
        {
            dt = DateTime.Now;
            GetAvailableSlot(dt.Month, dt.Year, dt.Day);
        }

        private void Schedule_CalendarView_DateChange1(object sender, CalendarView.DateChangeEventArgs e)
        {
            var str = e.Month + 1 + "-" + e.DayOfMonth + "-" + e.Year + " " + DateTime.Now.ToString("HH:mm:ss");
            try
            {

                //dt = DateTime.ParseExact(str, "MM/dd/yyyy", System.Globalization.CultureInfo.CurrentUICulture);//Convert.ToDateTime(str);//DateTime.Parse(str, System.Globalization.CultureInfo.CurrentCulture);
                 dt = Convert.ToDateTime(str);
                //dt = DateTime.ParseExact(str, "M-dd-yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentUICulture);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                dt = DateTime.ParseExact(str, "M-dd-yyyy HH:mm:ss", System.Globalization.CultureInfo.CurrentUICulture);
            }
            GetAvailableSlot(e.Month + 1, e.Year, e.DayOfMonth);
        }

        private async void GetAvailableSlot(int month, int year, int day)
        {
            updatedScheduleSlotInfo = new AvailableScheduleSlots();
            updatedScheduleSlotInfo.GetTimeInDetails = new List<GetTimeInDetails>();
            string date = "";
            switch (month)
            {
                case 01:
                    date = year + "-" + "01" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "January";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 02:
                    date = year + "-" + "02" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "February";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 03:
                    date = year + "-" + "03" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "March";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 04:
                    date = year + "-" + "04" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "April";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 05:
                    date = year + "-" + "05" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "May";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 06:
                    date = year + "-" + "06" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "June";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 07:
                    date = year + "-" + "07" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "July";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 08:
                    date = year + "-" + "08" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "August";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 09:
                    date = year + "-" + "09" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "September";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 10:
                    date = year + "-" + "10" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "October";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 11:
                    date = year + "-" + "11" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "November";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
                case 12:
                    date = year + "-" + "12" + "-" + day;
                    CustomerScheduleInformation.ScheduleMonth = "December";
                    CustomerScheduleInformation.ScheduleDate = day.ToString();
                    CustomerScheduleInformation.ScheduleYear = year.ToString();
                    break;
            }
            //date = date + "T00:00:00.000Z";
            CustomerScheduleInformation.ScheduleFullDate = (dt.ToString("yyyy-MM-dd HH:mm:ss")).Substring(0, 10);
            //CustomerScheduleInformation.ScheduleFullDate = date.Year + "-" + date.Month + "-" + date.Day;
            //DateTime local = date1.d;
            var dateToServer = dt.ToString("yyy/MM/dd HH:mm:ss");
            this.ViewModel.checkDate = CustomerScheduleInformation.ScheduleDate + "/" + CustomerScheduleInformation.ScheduleMonth + "/" + CustomerScheduleInformation.ScheduleYear;
            try
            {
                await ViewModel.GetSlotAvailability(CustomerScheduleInformation.ScheduleLocationCode, dateToServer);
                //await this.ViewModel.GetSlotAvailability(8, date);
                var datenow = DateTime.Now.TimeOfDay;
                if (this.ViewModel.ScheduleSlotInfo != null && this.ViewModel.ScheduleSlotInfo.GetTimeInDetails.Count > 0)
                {
                    foreach (var item in this.ViewModel.ScheduleSlotInfo.GetTimeInDetails)
                    {
                        DateTime availabletime = DateTime.Parse(item.TimeIn, System.Globalization.CultureInfo.CurrentCulture);
                        if (dt.Date == DateTime.Now.Date)
                        {
                            if (availabletime.TimeOfDay > datenow)
                            {
                                updatedScheduleSlotInfo.GetTimeInDetails.Add(item);
                            }
                        }
                        else
                        {
                            updatedScheduleSlotInfo.GetTimeInDetails.Add(item);
                        }
                    }
                    if (updatedScheduleSlotInfo.GetTimeInDetails.Count > 0)
                    {
                        SlotTxtView.Text = "Available Times Slots";
                    }
                    else
                    {
                        SlotTxtView.Text = "No Available Time Slots";
                    }
                    TimeSlot_GridView.Adapter = new ScheduleTimeSlots(Context, updatedScheduleSlotInfo);
                }

                if (this.ViewModel.ScheduleSlotInfo != null && this.ViewModel.ScheduleSlotInfo.GetTimeInDetails.Count > 0)
                {
                    TimeSlot_GridView.Adapter = new ScheduleTimeSlots(Context, updatedScheduleSlotInfo);

                }
                else
                {

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