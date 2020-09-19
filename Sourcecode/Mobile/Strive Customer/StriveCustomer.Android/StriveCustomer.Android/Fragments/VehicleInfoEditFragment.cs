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
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class VehicleInfoEditFragment : MvxFragment<MyProfileInfoViewModel>
    {
        private Spinner makeSpinner;
        private Spinner modelSpinner;
        private Spinner colorSpinner;
        private TextView membershipInfo;
        private Button backButton;
        private MyProfileInfoViewModel mpvm;
        private Dictionary<int, string> makeOptions,colorOptions,modelOptions;
        private ArrayAdapter<string> makeAdapter,colorAdapter,modelAdapter;
        private List<string> makeList, colorList, modelList;
        private VehicleMembershipFragment membershipFragment;
        private MyProfileInfoFragment myProfile;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);
            var rootview = this.BindingInflate(Resource.Layout.VehicleInfoEditFragment, null);
            mpvm = new MyProfileInfoViewModel();
            membershipFragment = new VehicleMembershipFragment();
            makeList = new List<string>();
            colorList = new List<string>();
            modelList = new List<string>();
            myProfile = new MyProfileInfoFragment();
            backButton = rootview.FindViewById<Button>(Resource.Id.vehicleBack);
            makeSpinner = rootview.FindViewById<Spinner>(Resource.Id.makeOptions);
            modelSpinner = rootview.FindViewById<Spinner>(Resource.Id.modelOptions);
            colorSpinner = rootview.FindViewById<Spinner>(Resource.Id.colorOptions);
            membershipInfo = rootview.FindViewById<TextView>(Resource.Id.membershipId);
            makeSpinner.ItemSelected += MakeSpinner_ItemSelected;
            modelSpinner.ItemSelected += ModelSpinner_ItemSelected;
            colorSpinner.ItemSelected += ColorSpinner_ItemSelected;
            membershipInfo.Click += MembershipInfo_Click;
            backButton.Click += BackButton_Click;
            LoadSpinner();
            return rootview;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, myProfile).Commit();
        }

        private void MembershipInfo_Click(object sender, EventArgs e)
        {
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, membershipFragment).Commit();
        }

        private void ColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var selected = mpvm.colorName.ElementAt(e.Position);
            CustomerInfo.colorNumber = selected.Key;
            CustomerInfo.colorName = selected.Value;
        }

        private void ModelSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var selected = mpvm.modelName.ElementAt(e.Position);
            CustomerInfo.modelNumber = selected.Key;
            CustomerInfo.modelName = selected.Value;
        }

        private void MakeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
           var selected = mpvm.manufacturerName.ElementAt(e.Position);
            CustomerInfo.vehicleMakeNumber = selected.Key;
            CustomerInfo.vehicleMakeName = selected.Value;
        }

        private async void LoadSpinner()
        {
            var data = await mpvm.getVehicleDetails();
            if(data != null)
            {
                makeOptions = mpvm.manufacturerName;
                colorOptions = mpvm.colorName;
                modelOptions = mpvm.modelName;
                foreach (var makeName in mpvm.manufacturerName.Values)
                {
                    makeList.Add(makeName);
                }
                foreach (var colorName in mpvm.colorName.Values)
                {
                    colorList.Add(colorName);
                }
                foreach (var modelName in mpvm.modelName.Values)
                {
                    modelList.Add(modelName);
                }
                makeAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, makeList);
                makeAdapter.SetDropDownViewResource(Android.Resource.Layout.support_simple_spinner_dropdown_item);
                colorAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, colorList);
                colorAdapter.SetDropDownViewResource(Android.Resource.Layout.support_simple_spinner_dropdown_item);
                modelAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, modelList);
                modelAdapter.SetDropDownViewResource(Android.Resource.Layout.support_simple_spinner_dropdown_item);
                
                makeSpinner.Adapter = makeAdapter;
                colorSpinner.Adapter = colorAdapter;
                modelSpinner.Adapter = modelAdapter;
            }
           
        } 
    }
}