using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
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
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Customer;
using StriveCustomer.Android.Adapter;
using OperationCanceledException = System.OperationCanceledException;

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
        //private ArrayAdapter<string> colorAdapter;
        private VehicleAdapter<string> makeAdapter, modelAdapter, colorAdapter;
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
            membershipFragment = new VehicleMembershipFragment(this.Activity);
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
            try
            {
                await this.ViewModel.SaveVehicle();
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }

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
            if (MembershipDetails.clientVehicleID != 0 && ViewModel.VehicleDetailsCheck())
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
            if (e.Position > 0)
            {
                MembershipDetails.selectedColor = e.Position;
                var selected = this.ViewModel.colorName.ElementAt(e.Position);
                MembershipDetails.colorNumber = selected.Key;
                MembershipDetails.colorName = selected.Value;
            }
            else 
            {
                MembershipDetails.selectedColor = e.Position;
                if (e.Position == 0 && colorSpinner.SelectedItem.ToString() == "Vehicle Color")
                {
                    ((TextView)colorSpinner.SelectedView).SetTextColor(Color.ParseColor("#bbbcbc"));
                }
                MembershipDetails.selectedColor = e.Position;
                MembershipDetails.colorName = null;
            }
            
        }

        private void ModelSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position > 0)
            {
                MembershipDetails.selectedModel = e.Position - 1;
                var selected = this.ViewModel.modelList.Model[e.Position - 1];//.ElementAt(e.Position);
                MembershipDetails.modelNumber = selected.ModelId;
                MembershipDetails.modelName = selected.ModelValue;
            }
            else
            {
                if (e.Position == 0 && modelSpinner.SelectedItem.ToString() == "Vehicle Model")
                {
                    ((TextView)modelSpinner.SelectedView).SetTextColor(Color.ParseColor("#bbbcbc"));
                }
                MembershipDetails.selectedModel = e.Position;
                MembershipDetails.modelName = null;

            }

        }

        private void MakeSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            if (e.Position > 0)
            {
                MembershipDetails.selectedMake = e.Position - 1;
                var selected = this.ViewModel.makeList.Make.ElementAt(e.Position - 1);
                MembershipDetails.vehicleMakeNumber = selected.MakeId;
                MembershipDetails.vehicleMfr = selected.MakeId;
                MembershipDetails.vehicleMakeName = selected.MakeValue;
                MembershipDetails.clientVehicleID = 0;
                MembershipDetails.selectedModel = 0;


            }
            else
            {
                if (e.Position == 0 && makeSpinner.SelectedItem.ToString() == "Vehicle Manufacturer")
                {
                    ((TextView)makeSpinner.SelectedView).SetTextColor(Color.ParseColor("#bbbcbc"));
                }
                MembershipDetails.selectedMake = e.Position;
                //var selected = this.ViewModel.makeList.Make.ElementAt(e.Position);
                MembershipDetails.vehicleMakeNumber = 0;
                MembershipDetails.vehicleMakeName = null;

            }
            GetModelList();

        }

        private async void LoadSpinner()
        {
            try
            {
                await ViewModel.getVehicleDetails();
                await ViewModel.GetMakeList();
                var preselectedManufacturer = 0;
                makeList.Insert(0, "Vehicle Manufacturer");
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
                    if (colorName.Value.Contains("Unknown"))
                    {
                        colorList.Add("Vehicle Color");
                    }
                    else
                    {
                        colorList.Add(colorName.Value);
                    }
                    if (MembershipDetails.colorNumber == colorName.Key)
                    {
                        MembershipDetails.selectedColor = preselectedColor;

                    }
                    preselectedColor++;
                }

                //modelList.Insert(0, "Model");

                makeAdapter = new VehicleAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, makeList);
                makeAdapter.SetDropDownViewResource(Android.Resource.Layout.support_simple_spinner_dropdown_item);
                colorAdapter = new VehicleAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, colorList);
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
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }
            // makeOptions = ViewModel.manufacturerName;
            // colorOptions = ViewModel.colorName;
            // modelOptions = ViewModel.modelName;
           
        }
        private async void GetModelList()
        {
            try
            {                
                await ViewModel.GetModelList(MembershipDetails.vehicleMakeName ?? "");
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

                }
                modelList.Insert(0, "Vehicle Model");
                modelAdapter = new VehicleAdapter<string>(Context, Resource.Layout.support_simple_spinner_dropdown_item, modelList);
                modelAdapter.SetDropDownViewResource(Android.Resource.Layout.support_simple_spinner_dropdown_item);
                modelSpinner.Adapter = modelAdapter;
                modelSpinner.SetSelection(MembershipDetails.selectedModel);
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    return;
                }
            }

            
        }
    }
}