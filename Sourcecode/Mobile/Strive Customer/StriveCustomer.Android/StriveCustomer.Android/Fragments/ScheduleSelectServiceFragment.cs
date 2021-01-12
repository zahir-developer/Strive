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
using Strive.Core.ViewModels.Customer.Schedule;

namespace StriveCustomer.Android.Fragments
{
    public class ScheduleSelectServiceFragment : MvxFragment<ScheduleServicesViewModel>
    {
        private Button ScheduleServices_BackButton;
        private Button ScheduleServices_FrontButton;
        private ScheduleFragment scheduleFragment;
        private ScheduleLocationsFragment locationsFragment;
        private View[] layout;
        private CheckBox[] checkBoxes;
        private LinearLayout ScheduleServices_LinearLayout;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.ScheduleServicesFragment, null);
            this.ViewModel = new ScheduleServicesViewModel();

            ScheduleServices_BackButton = rootView.FindViewById<Button>(Resource.Id.scheduleServices_BackButton);
            ScheduleServices_FrontButton = rootView.FindViewById<Button>(Resource.Id.scheduleServices_NextButton);
            ScheduleServices_LinearLayout = rootView.FindViewById<LinearLayout>(Resource.Id.ScheduleServices_LinearLayout);

            ScheduleServices_BackButton.Click += ScheduleServices_BackButton_Click;
            ScheduleServices_FrontButton.Click += ScheduleServices_NextButton_Click;

            GetServicesDates();

            return rootView;
        }

        private void ScheduleServices_NextButton_Click(object sender, EventArgs e)
        {
            locationsFragment = new ScheduleLocationsFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, locationsFragment).Commit();
        }

        private void ScheduleServices_BackButton_Click(object sender, EventArgs e)
        {
            scheduleFragment = new ScheduleFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, scheduleFragment).Commit();
        }

        private async void GetServicesDates()
        {
            await this.ViewModel.GetScheduledServices();

            if(this.ViewModel.scheduleServices != null && this.ViewModel.scheduleServices.ServicesWithPrice.Count > 0)
            {
                checkBoxes = new CheckBox[this.ViewModel.scheduleServices.ServicesWithPrice.Count];
                layout = new View[this.ViewModel.scheduleServices.ServicesWithPrice.Count];
                for (int service = 0; service < this.ViewModel.scheduleServices.ServicesWithPrice.Count; service++ )
                {
                    layout[service] = LayoutInflater.From(Context).Inflate(Resource.Layout.ScheduleSelectService_ItemView, ScheduleServices_LinearLayout, false);
                    var serviceName = layout[service].FindViewById<TextView>(Resource.Id.scheduleServiceName_TextView);
                    var serviceCost = layout[service].FindViewById<TextView>(Resource.Id.scheduleServiceCost_TextView);
                    var serviceDetailPassage = layout[service].FindViewById<TextView>(Resource.Id.scheduleServiceCost_TextView);
                    serviceDetailPassage.Visibility = ViewStates.Visible;
                    var serviceViewToggle = layout[service].FindViewById<TextView>(Resource.Id.infoShowChange_TextView);

                    serviceName.Text = this.ViewModel.scheduleServices.ServicesWithPrice[service].ServiceName;
                    serviceCost.Text = this.ViewModel.scheduleServices.ServicesWithPrice[service].Price.ToString();
                    ScheduleServices_LinearLayout.AddView(layout[service]);
                }
            }
            
        }
    }
}