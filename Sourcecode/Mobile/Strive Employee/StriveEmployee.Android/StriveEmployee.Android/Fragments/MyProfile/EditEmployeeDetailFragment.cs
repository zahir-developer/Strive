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
using Strive.Core.Utils;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile;

namespace StriveEmployee.Android.Fragments.MyProfile
{
    public class EditEmployeeDetailFragment : MvxFragment<EditEmployeeDetailsViewModel>
    {
        private Button back_Button;
        private Button save_Button;
        private EditText LoginID;
        private EditText DateOfHire;
        private Spinner status_Spinner;
        private MyProfileFragment profile_Fragment;
        private ArrayAdapter statusCodesAdapter;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.EditEmployeeDetails_Fragment, null);
            profile_Fragment = new MyProfileFragment();
            this.ViewModel = new EditEmployeeDetailsViewModel();

            status_Spinner = rootView.FindViewById<Spinner>(Resource.Id.status_Spinner);
            back_Button = rootView.FindViewById<Button>(Resource.Id.employmentDetails_BackButton);
            save_Button = rootView.FindViewById<Button>(Resource.Id.employmentDetails_SaveButton);
            LoginID = rootView.FindViewById<EditText>(Resource.Id.loginID_EditText);
            DateOfHire = rootView.FindViewById<EditText>(Resource.Id.DateoFHire_EditText);
            var dates = EmployeeLoginDetails.DateofHire.Split("T");
            var formattedDate = dates[0].Split("-");
            DateOfHire.Text = formattedDate[2] + "-" + formattedDate[1] + "-" + formattedDate[0];
            DateOfHire.Click += DateOfHire_Click;
            back_Button.Click += Back_Button_Click;
            save_Button.Click += Save_Button_Click;
            status_Spinner.ItemSelected += Status_Spinner_ItemSelected;
            GetStatus();

            LoginID.Text = EmployeeLoginDetails.LoginID;

            return rootView;
        }

        private void Status_Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
           if(e.Id == 0)
            {
                EmployeeLoginDetails.IsActive = true;
            }
            else
            {
                EmployeeLoginDetails.IsActive = false;
            }
        }

        private void DateOfHire_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DatePickerDialog dialog = new DatePickerDialog(Context, OnDateSet, today.Year, today.Month - 1, today.Day);
            dialog.DatePicker.MinDate = today.Millisecond;
            dialog.Show();
        }
        private void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            DateOfHire.Text = e.Month + 1 + "-" + e.DayOfMonth + "-" + e.Year;
            var setDate = DateUtils.ConvertDateTimeWithZ().Split("T"); 
            this.ViewModel.DateOfHire = e.Year + "-" + e.Month + 1  + "-" + e.DayOfMonth;
        }

        private async void Save_Button_Click(object sender, EventArgs e)
        {
            var dates = DateOfHire.Text.Split("-");
            this.ViewModel.DateOfHire = dates[2] +"-"+dates[1] +"-"+dates[0];
            EmployeeLoginDetails.LoginID = LoginID.Text;
            var result =  await this.ViewModel.SavePersonalInfo();
            if(result)
            {
                EmployeeLoginDetails.clearData();
                EmployeePersonalDetails.clearData();
                MyProfileInfoNeeds.selectedTab = 0;
                FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, profile_Fragment).Commit();
            }
        }
        private async void GetStatus()
        {
            var activeCodes = new List<string>() { "Active", "Inactive"};
                
                statusCodesAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, activeCodes);
                status_Spinner.Adapter = statusCodesAdapter;
                if (EmployeePersonalDetails.GenderSpinnerPosition != -1)
                {
                status_Spinner.SetSelection(EmployeePersonalDetails.GenderSpinnerPosition);
                }

        }

        private void Back_Button_Click(object sender, EventArgs e)
        {   
            MyProfileInfoNeeds.selectedTab = 0;
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, profile_Fragment).Commit();
        }
    }
}