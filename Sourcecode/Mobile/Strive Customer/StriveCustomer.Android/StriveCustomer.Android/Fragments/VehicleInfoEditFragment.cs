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
using AlertDialog = Android.App.AlertDialog;

namespace StriveCustomer.Android.Fragments
{
    [MvxUnconventionalAttribute]
    public class VehicleInfoEditFragment : MvxFragment<VehicleInfoEditViewModel>
    {
        private Spinner makeSpinner;
        private Spinner modelSpinner;
        private Spinner colorSpinner;
        private TextView membershipInfo;
        private Button backButton;
        private Button saveButton;
        private Dictionary<int, string> makeOptions, colorOptions, modelOptions;
        private ArrayAdapter<string> makeAdapter, colorAdapter, modelAdapter;
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
            this.ViewModel = new VehicleInfoEditViewModel();
            membershipFragment = new VehicleMembershipFragment();
            makeList = new List<string>();
            colorList = new List<string>();
            modelList = new List<string>();
            myProfile = new MyProfileInfoFragment();
            backButton = rootview.FindViewById<Button>(Resource.Id.vehicleBack);
            saveButton = rootview.FindViewById<Button>(Resource.Id.saveVehicle);
            makeSpinner = rootview.FindViewById<Spinner>(Resource.Id.makeOptions);
            modelSpinner = rootview.FindViewById<Spinner>(Resource.Id.modelOptions);
            colorSpinner = rootview.FindViewById<Spinner>(Resource.Id.colorOptions);
            membershipInfo = rootview.FindViewById<TextView>(Resource.Id.membershipId);
            makeSpinner.ItemSelected += MakeSpinner_ItemSelected;
            modelSpinner.ItemSelected += ModelSpinner_ItemSelected;
            colorSpinner.ItemSelected += ColorSpinner_ItemSelected;
            membershipInfo.Click += MembershipInfo_Click;
            backButton.Click += BackButton_Click;
            saveButton.Click += SaveButton_Click;
            LoadSpinner();
            return rootview;
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            await this.ViewModel.SaveVehicle();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            MembershipDetails.clearMembershipData();
            MyProfileInfoNeeds.selectedTab = 1;
            AppCompatActivity activity = (AppCompatActivity)Context;
            activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, myProfile).Commit();
        }

        private void MembershipInfo_Click(object sender, EventArgs e)
        {
            if (MembershipDetails.clientVehicleID != 0)
            {
                AppCompatActivity activity = (AppCompatActivity)Context;
                activity.SupportFragmentManager.BeginTransaction().Replace(Resource.Id.content_frame, membershipFragment).Commit();
            }
            else
            {
                ViewModel.ShowAlert();
            }
        }

        private void ColorSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            MembershipDetails.selectedColor = e.Position;
            var selected = this.ViewModel.colorName.ElementAt(e.Position);
            MembershipDetails.colorNumber = selected.Key;
            MembershipDetails.colorName = selected.Value;
        }

        private void ModelSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            MembershipDetails.selectedModel = e.Position;
            var selected = this.ViewModel.modelList.Model[e.Position];//.ElementAt(e.Position);
            MembershipDetails.modelNumber = selected.ModelId;
            MembershipDetails.modelName = selected.ModelValue;
        }

        private void MakeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            
            MembershipDetails.selectedMake = e.Position;
            var selected = this.ViewModel.makeList.Make.ElementAt(e.Position);
            MembershipDetails.vehicleMakeNumber = selected.MakeId;
            MembershipDetails.vehicleMakeName = selected.MakeValue;
            GetModelList();
        }

        private async void LoadSpinner()
        {
            await ViewModel.getVehicleDetails();
            await ViewModel.GetMakeList();
           // makeOptions = ViewModel.manufacturerName;
           // colorOptions = ViewModel.colorName;
            // modelOptions = ViewModel.modelName;
            var preselectedManufacturer = 0;
            foreach (var makeName in ViewModel.makeList.Make)
            {
                makeList.Add(makeName.MakeValue);
                if (MembershipDetails.vehicleMakeNumber == makeName.MakeId)
                {
                    MembershipDetails.selectedMake = preselectedManufacturer;

                }
                preselectedManufacturer++;
            }
            var preselectedColor = 0;
            foreach (var colorName in ViewModel.colorName)
            {
                colorList.Add(colorName.Value);
                if (MembershipDetails.colorNumber == colorName.Key)
                {
                    MembershipDetails.selectedColor = preselectedColor;

                }
                preselectedColor++;
            }

           

           // makeList.Insert(0, "Select Manufacturer");
           // makeList.RemoveAt(1);
           // colorList.Insert(0, "Select Color");
           // colorList.RemoveAt(1);
            //modelList.Insert(0, "Select Model");
           // modelList.RemoveAt(1);

            makeAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, makeList);
            makeAdapter.SetDropDownViewResource(Android.Resource.Layout.support_simple_spinner_dropdown_item);
            colorAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, colorList);
            colorAdapter.SetDropDownViewResource(Android.Resource.Layout.support_simple_spinner_dropdown_item);
            //modelAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, modelList);
           // modelAdapter.SetDropDownViewResource(Android.Resource.Layout.support_simple_spinner_dropdown_item);
            makeSpinner.Adapter = makeAdapter;
            colorSpinner.Adapter = colorAdapter;
           // modelSpinner.Adapter = modelAdapter;

            makeSpinner.SetSelection(MembershipDetails.selectedMake);
            //modelSpinner.SetSelection(MembershipDetails.selectedModel);
            colorSpinner.SetSelection(MembershipDetails.selectedColor);
        }
        private async void GetModelList() 
        {
            
            await ViewModel.GetModelList(MembershipDetails.vehicleMakeName);
            if (ViewModel.modelList != null)
            {
                modelList = new List<string>();
                var preselectedModel = 0;
                foreach (var modelName in ViewModel.modelList.Model)
                {
                    modelList.Add(modelName.ModelValue);
                    if (MembershipDetails.modelNumber == modelName.ModelId)
                    {
                        MembershipDetails.selectedModel = preselectedModel;

                    }
                    preselectedModel++;

                }
                //modelList.Insert(0, "Select Model");
                //modelList.RemoveAt(1);
            }
           
            modelAdapter = new ArrayAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, modelList);
            modelAdapter.SetDropDownViewResource(Android.Resource.Layout.support_simple_spinner_dropdown_item);
            modelSpinner.Adapter = modelAdapter;
            modelSpinner.SetSelection(MembershipDetails.selectedModel);
        }
    }
}