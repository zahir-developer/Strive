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
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveEmployee.Android.Fragments.MyProfile
{
    [MvxUnconventionalAttribute]
    public class EmployeeInfoFragment : MvxFragment<EmployeeInfoViewModel>
    {
        private ImageButton personalDetailEdit_ImageButton;

        private ImageButton employeeDetailEdit_ImageButton;

        private TextView FirstName_TextView;
        private TextView LastName_TextView;
        private TextView Gender_TextView;
        private TextView ContactNo_TextView;
        private TextView SSN_TextView;
        private TextView Immigration_TextView;
        private TextView Address_TextView;
        private TextView LoginID_TextView;
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

            FirstName_TextView = rootView.FindViewById<TextView>(Resource.Id.employeeFirstName_TextView);
            LastName_TextView = rootView.FindViewById<TextView>(Resource.Id.employeeLastName_TextView);
            Gender_TextView = rootView.FindViewById<TextView>(Resource.Id.gender_TextView);
            ContactNo_TextView = rootView.FindViewById<TextView>(Resource.Id.contactNo_TextView);
            SSN_TextView = rootView.FindViewById<TextView>(Resource.Id.SSN_TextView);
            Immigration_TextView = rootView.FindViewById<TextView>(Resource.Id.immigration_TextView);
            Address_TextView = rootView.FindViewById<TextView>(Resource.Id.address_TextView);
            LoginID_TextView = rootView.FindViewById<TextView>(Resource.Id.loginID_TextView);
            DOH_TextView = rootView.FindViewById<TextView>(Resource.Id.DOH_TextView);
            Status_TextView = rootView.FindViewById<TextView>(Resource.Id.status_TextView);
            Exemptions_TextVIew = rootView.FindViewById<TextView>(Resource.Id.exemptions_TextVIew);

            return rootView;
        }

        private void FillEmployeeDetails()
        {
            EmployeePersonalDetails.FirstName = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Firstname;
            EmployeePersonalDetails.LastName = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.LastName;
            EmployeePersonalDetails.GenderCodeID = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Gender;
            EmployeePersonalDetails.ContactNumber = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.PhoneNumber;
            EmployeePersonalDetails.SSN = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.SSNo;
            EmployeePersonalDetails.ImmigrationCodeID = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.ImmigrationStatus;
            EmployeePersonalDetails.Address = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Address1;
            EmployeeLoginDetails.LoginID = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Email;
            EmployeeLoginDetails.DateofHire = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.HiredDate;
            EmployeePersonalDetails.AddressID = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.EmployeeAddressId;
            EmployeeLoginDetails.DetailID = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.EmployeeDetailId;
            EmployeeLoginDetails.WashRate = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.WashRate;
            EmployeeLoginDetails.AuthID = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.AuthId;
            EmployeeLoginDetails.Exemptions = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Exemptions.ToString();
        }
        public async void GetEmployeeDetails()
        {
            if (this.ViewModel == null) 
            {
                ViewModel = new EmployeeInfoViewModel();
            }
            try
            {
                await ViewModel.GetGender();
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
            try
            {
                await ViewModel.GetImmigrationStatus();
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
            try
            {
                await ViewModel.GetPersonalEmployeeInfo();

            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
            if ((this.ViewModel?.PersonalDetails!=null && this.ViewModel.PersonalDetails.Employee != null && this.ViewModel.PersonalDetails.Employee.EmployeeInfo != null) || this.ViewModel.PersonalDetails.Employee.EmployeeCollision != null
                || this.ViewModel.PersonalDetails.Employee.EmployeeDocument != null || this.ViewModel.PersonalDetails.Employee.EmployeeLocations != null || this.ViewModel.PersonalDetails.Employee.EmployeeRoles != null)
            {
                FillEmployeeDetails();
                FirstName_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Firstname;
                LastName_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.LastName;
                if(this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Gender != null)
                {
                  
                    if(this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Gender != 0)
                    {
                        var genders = this.ViewModel.gender.Codes.Find(x => x.CodeId == this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Gender).CodeValue;
                        Gender_TextView.Text = this.ViewModel.gender.Codes.Find(x => x.CodeId == this.ViewModel.PersonalDetails.Employee.EmployeeInfo.Gender).CodeValue;
                    }
                }
                
                ContactNo_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.PhoneNumber;
                SSN_TextView.Text = this.ViewModel.PersonalDetails.Employee.EmployeeInfo.SSNo;

                foreach(var data in this.ViewModel.ImmigrationStatus.Codes)
                {
                    if(data.CodeId == this.ViewModel.PersonalDetails.Employee.EmployeeInfo.ImmigrationStatus)
                    {
                        Immigration_TextView.Text = data.CodeValue;
                    }
                }
                
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