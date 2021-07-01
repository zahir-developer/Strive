using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using CoreGraphics;
using MvvmCross.Binding.Extensions;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;
using StriveCustomer.iOS.UIUtils;

namespace StriveCustomer.iOS.Views
{
    public partial class AddVehicleList : MvxViewController<VehicleInfoEditViewModel>
    {
        private Dictionary<int, string> makeOptions, colorOptions, modelOptions;
        private List<string> makeList, colorList, modelList;
        public AddVehicleList() : base("AddVehicleList", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Initial_Setup();            
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void Initial_Setup()
        {
            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);

            AddVehicleView.Layer.CornerRadius = 5;
            SelectMembership_Text.Layer.CornerRadius = 5;
            SaveAddVehicle_Btn.Layer.CornerRadius = 5;

            NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
            {
                Font = DesignUtils.OpenSansBoldFifteen(),
                ForegroundColor = UIColor.Clear.FromHex(0x24489A),
            };
            NavigationItem.Title = "Vehicle";

            var leftBtn = new UIButton(UIButtonType.Custom);
            leftBtn.SetTitle("Account", UIControlState.Normal);
            leftBtn.SetTitleColor(UIColor.FromRGB(0, 110, 202), UIControlState.Normal);

            var leftBarBtn = new UIBarButtonItem(leftBtn);
            NavigationItem.SetLeftBarButtonItems(new UIBarButtonItem[] { leftBarBtn }, false);
            leftBtn.TouchUpInside += (sender, e) =>
            {
                ViewModel.NavigateProfile();
            };

            makeList = new List<string>();
            colorList = new List<string>();
            modelList = new List<string>();                                       
            
            getPickerData();           
        }

        partial void VehicleMake_SpinnerTouchEnd(UITextField sender)
        {
            if(VehicleMake_TextField.Text != null)
            {
                getModelList();               
            }
        }

        partial void VehicleModel_SpinnerTouchBegin(UITextField sender)
        {           
           
        }
        private void MakePicker(List<string> source,UITextField textField)
        {
            var picker = new UIPickerView
            {
                Model = new VehicleMakePickerSource(source, textField),
                ShowSelectionIndicator = true
            };
            var screenWidth = UIScreen.MainScreen.Bounds.Width;
            var pickerToolBar = new UIToolbar(new RectangleF(0, 0, (float)screenWidth, 44)) { BarStyle = UIBarStyle.Default, Translucent = true };
            var flexibleSpaceButton = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
            var doneButton = new UIBarButtonItem(UIBarButtonSystemItem.Done, (sender, e) => textField.ResignFirstResponder());
            pickerToolBar.SetItems(new[] { flexibleSpaceButton, doneButton }, false);

            textField.InputAccessoryView = pickerToolBar;
            textField.InputView = picker;
        }                      
         
        private async void getPickerData()
        {
            await ViewModel.getVehicleDetails();
            await ViewModel.GetMakeList();
            makeOptions = ViewModel.manufacturerName;
            colorOptions = ViewModel.colorName;
            //modelOptions = ViewModel.modelName;

            var preselectedManufacturer = 0;
            foreach (var makeName in ViewModel.manufacturerName)
            {
                makeList.Add(makeName.Value);
                if (MembershipDetails.vehicleMakeNumber == makeName.Key)
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

            makeList.Insert(0, "Select Manufacturer");
            makeList.RemoveAt(1);
            colorList.Insert(0, "Select Color");
            colorList.RemoveAt(1);

            MakePicker(makeList, VehicleMake_TextField);
            MakePicker(colorList, VehicleColor_TextField);
            MakePicker(modelList, VehicleModel_TextField);
        }
        private async void getModelList()
        {
            await ViewModel.GetModelList(VehicleMake_TextField.Text);

            if (ViewModel.modelList != null)
            {
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
                modelList.Insert(0, "Select Model");
                modelList.RemoveAt(1);
                MakePicker(modelList, VehicleModel_TextField);
            }
        }
        partial void SelectMembership_Touch(UIButton sender)
        {
            if (MembershipDetails.clientVehicleID != 0)
            {
                ViewModel.NavToVehicleMembership();
            }
            else
            {
                ViewModel.ShowAlert();
            }

        }

        partial void SaveAddVehicle_BtnTouch(UIButton sender)
        {
            var makeIndex = 0;
            foreach (var item in makeList)
            {
                if(VehicleMake_TextField.Text == item)
                {
                    MembershipDetails.selectedMake = makeIndex;
                    var selected = this.ViewModel.manufacturerName.ElementAt(makeIndex);
                    MembershipDetails.vehicleMakeNumber = selected.Key;
                    MembershipDetails.vehicleMakeName = selected.Value;
                }
                makeIndex++;
            }

            var modelIndex = 0;
            foreach (var item in modelList)
            {
                if (VehicleModel_TextField.Text == item)
                {
                    MembershipDetails.selectedModel = modelIndex;
                    var selected = this.ViewModel.modelName.ElementAt(modelIndex);
                    MembershipDetails.modelNumber = selected.Key;
                    MembershipDetails.modelName = selected.Value;
                }
                modelIndex++;
            }

            var colorIndex = 0;
            foreach (var item in colorList)
            {
                if (VehicleColor_TextField.Text == item)
                {
                    MembershipDetails.selectedColor = colorIndex;
                    var selected = this.ViewModel.colorName.ElementAt(colorIndex);
                    MembershipDetails.colorNumber = selected.Key;
                    MembershipDetails.colorName = selected.Value;
                }
                colorIndex++;
            }

            saveVehicle();
        }

        private async void saveVehicle()
        {
            await ViewModel.SaveVehicle();
        }
    }
}