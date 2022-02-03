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
using OperationCanceledException = System.OperationCanceledException;

namespace StriveCustomer.Android.Fragments
{
    public class ScheduleLocationsFragment : MvxFragment<ScheduleLocationsViewModel>
    {
        private ScheduleFragment scheduleFragment;
        private Button ScheduleCancel_Button;
        private Button scheduleLocations_BackButton;
        private Button scheduleLocations_NextButton;
        private ScheduleSelectServiceFragment selectServiceFragment;
        private ScheduleFragment scheduleragment;
        LinearLayout.LayoutParams layoutParams;
        private RadioGroup scheduleLocationsGroup;
        int someId = 2114119900;
        int count = 0;
        int firstTime = 0;
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
                selectServiceFragment = new ScheduleSelectServiceFragment();
                AppCompatActivity activity = (AppCompatActivity)this.Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, selectServiceFragment).Commit();
            }         
        }

        private void ScheduleLocations_BackButton_Click(object sender, EventArgs e)
        {
            scheduleragment = new ScheduleFragment();
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, scheduleragment).Commit();
        }

        private async void GetAllLocations()
        {
            try
            {
                await this.ViewModel.GetAllLocationsCommand();
                if (this.ViewModel.Locations != null && this.ViewModel.Locations.Location.Count > 0)
                {
                    for (int locations = 0; locations < this.ViewModel.Locations.Location.Count; locations++)
                    {
                        RadioButton radioButton = new RadioButton(Context);
                        layoutParams = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                        layoutParams.Gravity = GravityFlags.Left | GravityFlags.Center;
                        layoutParams.SetMargins(0, 25, 0, 25);
                        radioButton.LayoutParameters = layoutParams;
                        radioButton.Text = this.ViewModel.Locations.Location[locations].Address1;
                        radioButton.SetButtonDrawable(Resource.Drawable.radioButton);
                        radioButton.Id = someId;
                        radioButton.SetTextSize(ComplexUnitType.Sp, (float)16.5);
                        radioButton.SetTypeface(null, TypefaceStyle.Bold);
                        radioButton.TextAlignment = TextAlignment.ViewStart;
                        radioButton.CheckedChange += RadioButton_CheckedChange;
                        radioButton.SetPadding(15, 0, 15, 0);
                        someId++;
                        if (CustomerScheduleInformation.ScheduleServiceLocationNumber == locations)
                        {
                            radioButton.Checked = true;
                        }
                        scheduleLocationsGroup.AddView(radioButton);
                    }
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

        private void RadioButton_CheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            var radiobtn = sender as RadioButton;
            var text = radiobtn.Text;
            for(int data = 0;  data < this.ViewModel.Locations.Location.Count; data ++)
            {
                if(string.Equals(this.ViewModel.Locations.Location[data].Address1, text))
                {
                    CustomerScheduleInformation.ScheduleLocationCode = this.ViewModel.Locations.Location[data].LocationId;
                    CustomerScheduleInformation.ScheduleLocationAddress = this.ViewModel.Locations.Location[data].Address1;
                    if(firstTime == 0)
                    {
                        CustomerScheduleInformation.ScheduleServiceLocationNumber = data;
                        firstTime++;
                    }
                    else if(count == 0)
                    {
                        CustomerScheduleInformation.ScheduleServiceLocationNumber = data;
                        count++;
                    }
                    else if (count == 1)
                    {
                        count = 0;
                    }
                }              
            }           
        }
    }
}