using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
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

namespace StriveCustomer.Android.Fragments
{
    public class SchedulePreviewFragment : MvxFragment<SchedulePreviewDetailsViewModel>
    {
        private TextView appointmentPreviewTime_TextView;
        private TextView appointmentPreviewDate_TextView;
        private TextView rescheduleButton;
        private TextView vehicleName_TextView;
        private TextView serviceName_TextView;
        private TextView serviceLocation_TextView;
        private Button Cancel_Button;
        private Button BookNow_Button;
        private Button schedulePreview_BackButton;
        private ScheduleAppointmentFragment appointmentFragment;
        private ScheduleFragment scheduleFragment;
        private ScheduleConfirmationFragment confirmationFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.SchedulePreviewFragment, null);
            this.ViewModel = new SchedulePreviewDetailsViewModel();

            appointmentPreviewTime_TextView = rootView.FindViewById<TextView>(Resource.Id.appointmentPreviewTime_TextView);
            appointmentPreviewDate_TextView = rootView.FindViewById<TextView>(Resource.Id.appointmentPreviewDate_TextView);
            vehicleName_TextView = rootView.FindViewById<TextView>(Resource.Id.vehicleName_TextView);
            rescheduleButton = rootView.FindViewById<TextView>(Resource.Id.rescheduleButton);
            schedulePreview_BackButton = rootView.FindViewById<Button>(Resource.Id.schedulePreview_BackButton);
            serviceLocation_TextView = rootView.FindViewById<TextView>(Resource.Id.serviceLocation_TextView);
            Cancel_Button = rootView.FindViewById<Button>(Resource.Id.CancelButton);
            Cancel_Button.Click += Cancel_Button_Click;
            BookNow_Button = rootView.FindViewById<Button>(Resource.Id.BookNowButton);
            BookNow_Button.Click += BookNow_Button_Click;
            serviceLocation_TextView.Text = CustomerScheduleInformation.ScheduleLocationAddress;
            rescheduleButton.PaintFlags = PaintFlags.UnderlineText;
            rescheduleButton.Click += RescheduleButton_Click;
            schedulePreview_BackButton.Click += SchedulePreview_BackButton_Click;
            serviceName_TextView = rootView.FindViewById<TextView>(Resource.Id.serviceName_TextView);
            serviceName_TextView.Text = CustomerScheduleInformation.ScheduleServiceName;
            vehicleName_TextView.Text = CustomerScheduleInformation.ScheduledVehicleName;
            appointmentPreviewTime_TextView.Text = CustomerScheduleInformation.ScheduleServiceTime;
            appointmentPreviewDate_TextView.Text = CustomerScheduleInformation.ScheduleDate + " " + CustomerScheduleInformation.ScheduleMonth + " " + CustomerScheduleInformation.ScheduleYear + " | ";
            return rootView;
        }

        private void SchedulePreview_BackButton_Click(object sender, EventArgs e)
        {
            CustomerScheduleInformation.ScheduleServiceTime = null;
            appointmentFragment = new ScheduleAppointmentFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, appointmentFragment).Commit();
        }

        private async void BookNow_Button_Click(object sender, EventArgs e)
        {
            await this.ViewModel.BookNow();
            confirmationFragment = new ScheduleConfirmationFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, confirmationFragment).Commit();
        }

        private void Cancel_Button_Click(object sender, EventArgs e)
        {
            CustomerScheduleInformation.ScheduleServiceTime = null;
            appointmentFragment = new ScheduleAppointmentFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, appointmentFragment).Commit();
        }

        private void RescheduleButton_Click(object sender, EventArgs e)
        {
            scheduleFragment = new ScheduleFragment(this.Context);
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, scheduleFragment).Commit();
        }
    }
}