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
    public class EditEmployeeDetailFragment : MvxFragment<EditEmployeeDetailsViewModel>
    {
        private Button back_Button;
        private Button save_Button;
        private EditText LoginID;
        private EditText DateOfHire;
        private Spinner Status;
        private MyProfileFragment profile_Fragment;
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

            back_Button = rootView.FindViewById<Button>(Resource.Id.employmentDetails_BackButton);
            save_Button = rootView.FindViewById<Button>(Resource.Id.employmentDetails_SaveButton);
            LoginID = rootView.FindViewById<EditText>(Resource.Id.loginID_EditText);
            back_Button.Click += Back_Button_Click;
            save_Button.Click += Save_Button_Click;

            return rootView;
        }

        private void Save_Button_Click(object sender, EventArgs e)
        {
           
        }

        private void Back_Button_Click(object sender, EventArgs e)
        {
            FragmentManager.BeginTransaction().Replace(Resource.Id.content_Frame, profile_Fragment).Commit();
        }
    }
}