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
    public class ScheduleLocationsFragment : MvxFragment<ScheduleLocationsViewModel>
    {
        private ScheduleFragment scheduleFragment;
        private Button ScheduleCancel_Button;
        private Button scheduleLocations_BackButton;
        private Button scheduleLocations_NextButton;
        private ScheduleSelectServiceFragment selectServiceFragment;
        private ScheduleAppointmentFragment appointmentFragment;
        LinearLayout.LayoutParams layoutParams;
        private RadioGroup scheduleLocationsGroup;
        int someId = 2114119900;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.ScheduleLocationsFragment, null);
            this.ViewModel = new ScheduleLocationsViewModel();
            ScheduleCancel_Button = rootView.FindViewById<Button>(Resource.Id.ScheduleLocationCancel_Button);
            ScheduleCancel_Button.Click += ScheduleCancel_Button_Click;
            scheduleLocations_BackButton = rootView.FindViewById<Button>(Resource.Id.scheduleLocations_BackButton);
            scheduleLocations_BackButton.Click += ScheduleLocations_BackButton_Click;
            scheduleLocations_NextButton = rootView.FindViewById<Button>(Resource.Id.scheduleLocations_NextButton);
            scheduleLocations_NextButton.Click += ScheduleLocations_NextButton_Click;
            scheduleLocationsGroup = rootView.FindViewById<RadioGroup>(Resource.Id.scheduleLocations_RadioGroup);
            GetAllLocations();
            return rootView;
        }

        private void ScheduleCancel_Button_Click(object sender, EventArgs e)
        {
            scheduleFragment = new ScheduleFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, scheduleFragment).Commit();
        }

        private void ScheduleLocations_NextButton_Click(object sender, EventArgs e)
        {
            if(this.ViewModel.checkSelectedLocation())
            {
                appointmentFragment = new ScheduleAppointmentFragment();
                AppCompatActivity activity = (AppCompatActivity)this.Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, appointmentFragment).Commit();
            }         
        }

        private void ScheduleLocations_BackButton_Click(object sender, EventArgs e)
        {
            selectServiceFragment = new ScheduleSelectServiceFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, selectServiceFragment).Commit();
        }

        private async void GetAllLocations()
        {
            await this.ViewModel.GetAllLocationsCommand();
            if(this.ViewModel.Locations != null && this.ViewModel.Locations.Location.Count > 0)
            {
                foreach(var location in this.ViewModel.Locations.Location)
                {
                    RadioButton radioButton = new RadioButton(Context);
                    layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                    layoutParams.Gravity = GravityFlags.Left | GravityFlags.Center;
                    layoutParams.SetMargins(0, 25, 0, 25);
                    radioButton.LayoutParameters = layoutParams;
                    radioButton.Text = location.Address1;
                    radioButton.SetButtonDrawable(Resource.Drawable.radioButton);
                    radioButton.Id = someId;
                    radioButton.SetTextSize(ComplexUnitType.Sp, (float)16.5);
                    radioButton.SetTypeface(null, TypefaceStyle.Bold);
                    radioButton.TextAlignment = TextAlignment.ViewStart;
                    radioButton.CheckedChange += RadioButton_CheckedChange;
                    radioButton.SetPadding(15,0,15,0);
                    someId++;
                    scheduleLocationsGroup.AddView(radioButton);
                }

            }
        }

        private void RadioButton_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var radiobtn = sender as RadioButton;
            var text = radiobtn.Text;
            foreach(var data in this.ViewModel.Locations.Location)
            {
                if(string.Equals(data.Address1, text))
                {
                    CustomerScheduleInformation.ScheduleLocationCode = data.LocationId;
                    CustomerScheduleInformation.ScheduleLocationAddress = data.Address1;
                }              
            }
        }
    }
}