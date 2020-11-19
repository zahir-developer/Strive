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
using Strive.Core.ViewModels.Employee.MyProfile;

namespace StriveEmployee.Android.Fragments.MyProfile
{
    public class EmployeeInfoFragment : MvxFragment<EmployeeInfoViewModel>
    {
        private ImageButton personalDetailEdit_ImageButton;
        private ImageButton personalDetailDelete_ImageButton;
        private ImageButton employeeDetailEdit_ImageButton;
        private ImageButton employeeDetailDelete_ImageButton;
        private TextView FirstName_TextView;
        private TextView LastName_TextView;
        private TextView Gender_TextView;
        private TextView ContactNo_TextView;
        private TextView SSN_TextView;
        private TextView Immigration_TextView;
        private TextView Address_TextView;
        private TextView LoginID_TextView;
        private TextView Password_TextView;
        private TextView DOH_TextView;
        private TextView Status_TextView;
        private TextView Exemptions_TextVIew;
        private EditPersonalDetailsFragment personalDetails_Fragment;
        private EditEmployeeDetailFragment employeeDetails_Fragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.MyProfileEmployeeInfo_Fragment, null);
            this.ViewModel = new EmployeeInfoViewModel();
            GetEmployeeDetails();

            personalDetailEdit_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.PersonalEdit_ImageButton);
            personalDetailDelete_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.personalDelete_ImageButton);
            employeeDetailEdit_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.employeeEdit_ImageButton);
            employeeDetailDelete_ImageButton = rootView.FindViewById<ImageButton>(Resource.Id.employeeDelete_ImageButton);

            FirstName_TextView = rootView.FindViewById<TextView>(Resource.Id.employeeFirstName_TextView);
            LastName_TextView = rootView.FindViewById<TextView>(Resource.Id.employeeLastName_TextView);
            Gender_TextView = rootView.FindViewById<TextView>(Resource.Id.gender_TextView);
            ContactNo_TextView = rootView.FindViewById<TextView>(Resource.Id.contactNo_TextView);
            SSN_TextView = rootView.FindViewById<TextView>(Resource.Id.SSN_TextView);
            Immigration_TextView = rootView.FindViewById<TextView>(Resource.Id.immigration_TextView);
            Address_TextView = rootView.FindViewById<TextView>(Resource.Id.address_TextView);
            LoginID_TextView = rootView.FindViewById<TextView>(Resource.Id.loginID_TextView);
            Password_TextView = rootView.FindViewById<TextView>(Resource.Id.password_TextView);
            DOH_TextView = rootView.FindViewById<TextView>(Resource.Id.DOH_TextView);
            Status_TextView = rootView.FindViewById<TextView>(Resource.Id.status_TextView);
            Exemptions_TextVIew = rootView.FindViewById<TextView>(Resource.Id.exemptions_TextVIew);

            personalDetailEdit_ImageButton.Click += PersonalDetailEdit_ImageButton_Click; ;
            personalDetailDelete_ImageButton.Click += PersonalDetailDelete_ImageButton_Click; ;
            employeeDetailEdit_ImageButton.Click += EmployeeDetailEdit_ImageButton_Click; ;
            employeeDetailDelete_ImageButton.Click += EmployeeDetailDelete_ImageButton_Click; ;

           
           
            return rootView;
        }

        private void EmployeeDetailDelete_ImageButton_Click(object sender, EventArgs e)
        {
            
        }

        private void EmployeeDetailEdit_ImageButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            employeeDetails_Fragment = new EditEmployeeDetailFragment();
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, employeeDetails_Fragment).Commit();
        }

        private void PersonalDetailDelete_ImageButton_Click(object sender, EventArgs e)
        {
           
        }

        private void PersonalDetailEdit_ImageButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)this.Context;
            personalDetails_Fragment = new EditPersonalDetailsFragment();
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, personalDetails_Fragment).Commit();
        }
        private async void GetEmployeeDetails()
        {
            await this.ViewModel.GetPersonalEmployeeInfo();
            if(this.ViewModel.PersonalDetails.Employee != null)
            {
                FirstName_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Firstname;
                LastName_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.LastName;
                Gender_TextView.Text = "";
                ContactNo_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.PhoneNumber;
                SSN_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.SSNo;
                Immigration_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.ImmigrationStatus.ToString();
                Address_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Address1;
                LoginID_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Email;

                var date = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.HiredDate.Split("T");
                DOH_TextView.Text = date[0];
                if (this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Status)
                {
                    Status_TextView.Text = "Active";
                }
                else
                {
                    Status_TextView.Text = "InActive";
                }
                Exemptions_TextVIew.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Exemptions.ToString();
            }
        }

    }
}