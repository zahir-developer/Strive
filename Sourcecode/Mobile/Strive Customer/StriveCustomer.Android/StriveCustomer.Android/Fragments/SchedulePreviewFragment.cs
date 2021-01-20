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
        private TextView appointmentPreviewDate_TextView;
        private TextView rescheduleButton;
        private TextView vehicleName_TextView;
        private ScheduleFragment scheduleFragment;
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

            appointmentPreviewDate_TextView = rootView.FindViewById<TextView>(Resource.Id.appointmentPreviewDate_TextView);
            vehicleName_TextView = rootView.FindViewById<TextView>(Resource.Id.vehicleName_TextView);
            rescheduleButton = rootView.FindViewById<TextView>(Resource.Id.rescheduleButton);
            rescheduleButton.PaintFlags = PaintFlags.UnderlineText;
            rescheduleButton.Click += RescheduleButton_Click;
            vehicleName_TextView.Text = CustomerScheduleInformation.ScheduledVehicleName;
            appointmentPreviewDate_TextView.Text = CustomerScheduleInformation.ScheduleDate + " " + CustomerScheduleInformation.ScheduleMonth + " " + CustomerScheduleInformation.ScheduleYear + " | ";
            return rootView;
        }

        private void RescheduleButton_Click(object sender, EventArgs e)
        {
            scheduleFragment = new ScheduleFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, scheduleFragment).Commit();
        }
    }
}