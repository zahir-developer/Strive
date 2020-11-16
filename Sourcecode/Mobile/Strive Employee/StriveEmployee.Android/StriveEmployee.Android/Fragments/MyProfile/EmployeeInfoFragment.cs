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
using Strive.Core.ViewModels.Employee.MyProfile;

namespace StriveEmployee.Android.Fragments.MyProfile
{
    public class EmployeeInfoFragment : MvxFragment<EmployeeInfoViewModel>
    {
        public TextView FirstName_TextView;
        public TextView LastName_TextView;
        public TextView Gender_TextView;
        public TextView ContactNo_TextView;
        public TextView SSN_TextView;
        public TextView Immigration_TextView;
        public TextView Address_TextView;
        public TextView LoginID_TextView;
        public TextView Password_TextView;
        public TextView DOH_TextView;
        public TextView Status_TextView;
        public TextView Exemptions_TextVIew;
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

            return rootView;
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