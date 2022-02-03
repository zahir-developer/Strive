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
using Strive.Core.Models.Employee.Common;
using Strive.Core.Utils.Employee;
using Strive.Core.ViewModels.Employee.MyProfile;
using OperationCanceledException = System.OperationCanceledException;

namespace StriveEmployee.Android.Fragments.MyProfile
{
    public class EditPersonalDetailsFragment : MvxFragment<EditPersonalDetailsViewModel>
    {
        private Button back_Button;
        private Button save_Button;
        private EditText FirstName;
        private EditText LastName;
        private EditText ContactNumber;
        private EditText Address;
        private EditText SSN;
        private Spinner ImmigrationStatus;
        private Spinner gender_Spinner;
        private ArrayAdapter<string> codesAdapter, immigrationCodesAdapter;
        private List<string> codes, immigrationCodes;
        private MyProfileFragment profile_Fragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootView = this.BindingInflate(Resource.Layout.EditPersonalDetails_Fragment, null);
            profile_Fragment = new MyProfileFragment();
            this.ViewModel = new EditPersonalDetailsViewModel();

            back_Button = rootView.FindViewById<Button>(Resource.Id.personalDetails_BackButton);
            save_Button = rootView.FindViewById<Button>(Resource.Id.personalDetails_SaveButton);
            gender_Spinner = rootView.FindViewById<Spinner>(Resource.Id.genderOptions_Spinner);

            FirstName = rootView.FindViewById<EditText>(Resource.Id.firstName_EditText);
            LastName = rootView.FindViewById<EditText>(Resource.Id.lastName_EditText);
            ContactNumber = rootView.FindViewById<EditText>(Resource.Id.contactNumber_EditText);
            Address = rootView.FindViewById<EditText>(Resource.Id.address_EditText);
            SSN = rootView.FindViewById<EditText>(Resource.Id.ssn_EditText);
            ImmigrationStatus = rootView.FindViewById<Spinner>(Resource.Id.immigrationOptions_Spinner);
           
            FirstName.Text = EmployeePersonalDetails.FirstName;
            LastName.Text = EmployeePersonalDetails.LastName;
            ContactNumber.Text = EmployeePersonalDetails.ContactNumber;
            Address.Text = EmployeePersonalDetails.Address;
            SSN.Text = EmployeePersonalDetails.SSN;


            back_Button.Click += Back_Button_Click;
            save_Button.Click += Save_Button_Click;
            gender_Spinner.ItemSelected += Gender_Spinner_ItemSelected;
            ImmigrationStatus.ItemSelected += ImmigrationStatus_ItemSelected;


            GetGender();
            GetImmigrationStatus();



            return rootView;
        }

        private void ImmigrationStatus_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var data = this.ViewModel.ImmigrationStatus.Codes.ElementAt(e.Position);
            EmployeePersonalDetails.ImmigrationCodeID = data.CodeId;
            EmployeePersonalDetails.ImmigrationSpinnerPosition = e.Position;
        }

        private void Gender_Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var data = this.ViewModel.gender.Codes.ElementAt(e.Position);
            EmployeePersonalDetails.GenderCodeID = data.CodeId;
            EmployeePersonalDetails.GenderSpinnerPosition = e.Position;            
        }

        private async void GetGender()
        {
            try
            {
                await this.ViewModel.GetGender();
                if (this.ViewModel.gender != null)
                {
                    codes = new List<string>();
                    foreach (var data in this.ViewModel.gender.Codes)
                    {
                        codes.Add(data.CodeValue);
                    }
                    codesAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, codes);
                    gender_Spinner.Adapter = codesAdapter;
                    if (EmployeePersonalDetails.GenderSpinnerPosition != -1)
                    {
                        gender_Spinner.SetSelection(EmployeePersonalDetails.GenderSpinnerPosition);
                    }
                    if (EmployeePersonalDetails.GenderCodeID != -1)
                    {
                        for (int i = 0; i < this.ViewModel.gender.Codes.Count; i++)
                        {
                            if (this.ViewModel.gender.Codes[i].CodeId == EmployeePersonalDetails.GenderCodeID)
                            {
                                gender_Spinner.SetSelection(i);
                            }
                        }
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

        private async void GetImmigrationStatus()
        {
            try
            {
                await this.ViewModel.GetImmigrationStatus();
                if (this.ViewModel.ImmigrationStatus != null)
                {
                    immigrationCodes = new List<string>();
                    foreach (var data in this.ViewModel.ImmigrationStatus.Codes)
                    {
                        immigrationCodes.Add(data.CodeValue);
                    }
                    immigrationCodesAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, immigrationCodes);
                    ImmigrationStatus.Adapter = immigrationCodesAdapter;
                    if (EmployeePersonalDetails.ImmigrationSpinnerPosition != -1)
                    {
                        ImmigrationStatus.SetSelection(EmployeePersonalDetails.ImmigrationSpinnerPosition);
                    }
                    if (EmployeePersonalDetails.ImmigrationCodeID != -1)
                    {
                        for (int i = 0; i < this.ViewModel.ImmigrationStatus.Codes.Count; i++)
                        {
                            if (this.ViewModel.ImmigrationStatus.Codes[i].CodeId == EmployeePersonalDetails.ImmigrationCodeID)
                            {
                                ImmigrationStatus.SetSelection(i);
                            }
                        }
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

        private async void Save_Button_Click(object sender, EventArgs e)
        {
            EmployeePersonalDetails.FirstName = FirstName.Text;
            EmployeePersonalDetails.LastName = LastName.Text ;
            EmployeePersonalDetails.ContactNumber = ContactNumber.Text;
            EmployeePersonalDetails.Address = Address.Text;
            EmployeePersonalDetails.SSN = SSN.Text ;
            try
            {
                var result = await this.ViewModel.SavePersonalInfo();
                if (result)
                {
                    EmployeeLoginDetails.clearData();
                    EmployeePersonalDetails.clearData();
                    MyProfileInfoNeeds.selectedTab = 0;
                    FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, profile_Fragment).Commit();
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

        private void Back_Button_Click(object sender, EventArgs e)
        {
            MyProfileInfoNeeds.selectedTab = 0;
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, profile_Fragment).Commit();
        }
    }
}