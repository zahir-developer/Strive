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
using Strive.Core.ViewModels.Employee.MyProfile;

namespace StriveEmployee.Android.Fragments.MyProfile
{
    public class EditPersonalDetailsFragment : MvxFragment<EditPersonalDetailsViewModel>
    {
        private Button back_Button;
        private Button save_Button;
        private Spinner gender_Spinner;
        private ArrayAdapter<string> codesAdapter;
        private List<string> codes;
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
           
            back_Button.Click += Back_Button_Click;
            save_Button.Click += Save_Button_Click;
            gender_Spinner.ItemSelected += Gender_Spinner_ItemSelected; ;
            GetGender();
            
            
            return rootView;
        }

        private void Gender_Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {

        }

        private async void GetGender()
        {
            await this.ViewModel.GetGender();
            if(this.ViewModel.gender != null)
            {
                codes = new List<string>();
                foreach (var data in this.ViewModel.gender.Codes)
                {
                    codes.Add(data.CodeValue);
                }
                codesAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, codes);
                gender_Spinner.Adapter = codesAdapter;
            }
           
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