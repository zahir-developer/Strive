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
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V4;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Binding.BindingContext;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class PersonalInfoEditFragment : MvxFragment<PersonalInfoEditViewModel>
    {
        private EditText fullNameEditText;
        private EditText contactEditText;
        private EditText addressEditText;
        private EditText zipCodeEditText;
        private EditText secondaryContactEditText;
        private EditText emailEditText;
        private Button saveButton;
        private Button backButton;
        private MyProfileInfoFragment myProfileInfoFragment;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.PersonalInfoEditFragment, null);
            this.ViewModel = new PersonalInfoEditViewModel();
            myProfileInfoFragment = new MyProfileInfoFragment();
            backButton = rootview.FindViewById<Button>(Resource.Id.backPersonal);
            fullNameEditText = rootview.FindViewById<EditText>(Resource.Id.fullName);
            contactEditText = rootview.FindViewById<EditText>(Resource.Id.contactNumber);
            addressEditText = rootview.FindViewById<EditText>(Resource.Id.Address);
            zipCodeEditText = rootview.FindViewById<EditText>(Resource.Id.zipCodes);
            secondaryContactEditText = rootview.FindViewById<EditText>(Resource.Id.secondaryPhone);
            emailEditText = rootview.FindViewById<EditText>(Resource.Id.emails);
            saveButton = rootview.FindViewById<Button>(Resource.Id.personalInfoSave);

            saveButton.Click += SaveButton_Click;
            backButton.Click += BackButton_Click;

            return rootview;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            MyProfileInfoNeeds.selectedTab = 0;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, myProfileInfoFragment).Commit();
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            ViewModel.FullName = fullNameEditText.Text;
            ViewModel.ContactNumber = contactEditText.Text;
            ViewModel.Address = addressEditText.Text;
            ViewModel.ZipCode = zipCodeEditText.Text;
            ViewModel.SecondaryContactNumber = secondaryContactEditText.Text;
            ViewModel.Email = emailEditText.Text;
            var result = await ViewModel.saveClientInfoCommand();
            if(result)
            {
                AppCompatActivity activity = (AppCompatActivity)Context;
                MyProfileInfoNeeds.selectedTab = 0;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, myProfileInfoFragment).Commit();
            }
           
        }
    }
}