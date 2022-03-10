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
    public class ScheduleConfirmationFragment : MvxFragment<ScheduleConfirmationViewModel>
    {
        private TextView confirmationPreviewDate_TextView;
        private TextView confirmationPreviewTime_TextView;
        private TextView confirmationVehicleName_TextView;
        private TextView backtodashboard_Textview;
        private ScheduleFragment scheduleFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.ScheduleConfirmationFragment, null);
            this.ViewModel = new ScheduleConfirmationViewModel();

            confirmationPreviewDate_TextView = rootView.FindViewById<TextView>(Resource.Id.confirmationPreviewDate_TextView);
            confirmationPreviewTime_TextView = rootView.FindViewById<TextView>(Resource.Id.confirmationPreviewTime_TextView);
            confirmationVehicleName_TextView = rootView.FindViewById<TextView>(Resource.Id.confirmationVehicleName_TextView);
            backtodashboard_Textview = rootView.FindViewById<TextView>(Resource.Id.backtodashboard_Textview);

            confirmationPreviewDate_TextView.Text = CustomerScheduleInformation.ScheduleDate + " " + CustomerScheduleInformation.ScheduleMonth + " " + CustomerScheduleInformation.ScheduleYear + " | ";
            confirmationPreviewTime_TextView.Text = CustomerScheduleInformation.ScheduleServiceTime;
            confirmationVehicleName_TextView.Text = CustomerScheduleInformation.ScheduledVehicleName;

            backtodashboard_Textview.PaintFlags = PaintFlags.UnderlineText;
            backtodashboard_Textview.Click += Backtodashboard_Textview_Click;


            return rootView;
        }

        private void Backtodashboard_Textview_Click(object sender, EventArgs e)
        {
            this.ViewModel.ClearScheduleData();
            scheduleFragment = new ScheduleFragment(this.Context);
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, scheduleFragment).Commit();
        }
    }
}