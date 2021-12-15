using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class PersonalInfoFragment : MvxFragment<PersonalInfoViewModel>
    {
        TextView fullNameView;
        TextView contactNumberView;
        TextView addressView;
        TextView zipCodeView;
        TextView secPhoneView;
        TextView emailView;
        ImageView personalEditInfo;
        PersonalInfoEditFragment personalEdit;
        CustomerPersonalInfo customerPersonalInfo { get; set; }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.PersonalInfoFragment, null);
            this.ViewModel = new PersonalInfoViewModel();
            personalEdit = new PersonalInfoEditFragment();
            personalEditInfo = rootview.FindViewById<ImageView>(Resource.Id.personalInfoEdit);
            fullNameView = rootview.FindViewById<TextView>(Resource.Id.fullName);
            contactNumberView = rootview.FindViewById<TextView>(Resource.Id.contactNumber);
            addressView = rootview.FindViewById<TextView>(Resource.Id.address);
            zipCodeView = rootview.FindViewById<TextView>(Resource.Id.zipCode);
            secPhoneView = rootview.FindViewById<TextView>(Resource.Id.secPhoneNumber);
            emailView = rootview.FindViewById<TextView>(Resource.Id.email);

            personalEditInfo.Click += PersonalEditInfo_Click;
            GetClientInfo();
            return rootview;
        }

        private void PersonalEditInfo_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, personalEdit).Commit();
        }

        public async void GetClientInfo()
        {
            if (this.ViewModel == null)
            {
                ViewModel = new PersonalInfoViewModel();
            }
            await this.ViewModel.GetClientById();

            if(ViewModel.customerInfo.Status.Count > 0)
            {
                fullNameView.Text = ViewModel.customerInfo.Status.LastOrDefault().FirstName + " " + ViewModel.customerInfo.Status.LastOrDefault().MiddleName + " " + ViewModel.customerInfo.Status.LastOrDefault().LastName;
                contactNumberView.Text = ViewModel.customerInfo.Status.LastOrDefault().PhoneNumber;
                addressView.Text = ViewModel.customerInfo.Status.LastOrDefault().Address1;
                zipCodeView.Text = ViewModel.customerInfo.Status.LastOrDefault().Zip;
                secPhoneView.Text = ViewModel.customerInfo.Status.LastOrDefault().PhoneNumber2;
                emailView.Text = ViewModel.customerInfo.Status.LastOrDefault().Email;

                MyProfileCustomerInfo.FullName = ViewModel.customerInfo.Status.LastOrDefault().FirstName + " " + ViewModel.customerInfo.Status.LastOrDefault().MiddleName + " " + ViewModel.customerInfo.Status.LastOrDefault().LastName; 
                MyProfileCustomerInfo.ContactNumber = ViewModel.customerInfo.Status.LastOrDefault().PhoneNumber;
                MyProfileCustomerInfo.Email = ViewModel.customerInfo.Status.LastOrDefault().Email;
                MyProfileCustomerInfo.SecondaryContactNumber = ViewModel.customerInfo.Status.LastOrDefault().PhoneNumber2;
                MyProfileCustomerInfo.Address = ViewModel.customerInfo.Status.LastOrDefault().Address1;
                MyProfileCustomerInfo.ZipCode = ViewModel.customerInfo.Status.LastOrDefault().Zip;

            }
            
        }
    }
}