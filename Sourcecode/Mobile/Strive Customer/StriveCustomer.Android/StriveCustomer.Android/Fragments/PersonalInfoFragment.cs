using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
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
    public class PersonalInfoFragment : MvxFragment<MyProfileInfoViewModel>
    {
        TextView fullNameView;
        TextView contactNumberView;
        TextView addressView;
        TextView zipCodeView;
        TextView secPhoneView;
        TextView emailView;
        MyProfileInfoViewModel myProfile = new MyProfileInfoViewModel();
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
            ViewModel = new MyProfileInfoViewModel();
            fullNameView = rootview.FindViewById<TextView>(Resource.Id.fullName);
            contactNumberView = rootview.FindViewById<TextView>(Resource.Id.contactNumber);
            addressView = rootview.FindViewById<TextView>(Resource.Id.address);
            zipCodeView = rootview.FindViewById<TextView>(Resource.Id.zipCode);
            secPhoneView = rootview.FindViewById<TextView>(Resource.Id.secPhoneNumber);
            emailView = rootview.FindViewById<TextView>(Resource.Id.email);

            fullNameView.Text = customerPersonalInfo.Status[0].FirstName;
            contactNumberView.Text = customerPersonalInfo.Status[0].PhoneNumber;
            addressView.Text = customerPersonalInfo.Status[0].Address1;
            zipCodeView.Text = customerPersonalInfo.Status[0].Zip;
            secPhoneView.Text = customerPersonalInfo.Status[0].PhoneNumber2;
            emailView.Text = customerPersonalInfo.Status[0].Email;

            return rootview;
        }
    }
}