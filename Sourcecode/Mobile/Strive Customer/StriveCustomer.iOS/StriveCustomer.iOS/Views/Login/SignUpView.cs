using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CoreGraphics;
using Foundation;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.Models.Customer;
using Strive.Core.ViewModels.Customer;
using UIKit;

namespace StriveCustomer.iOS.Views.Login
{
    public partial class SignUpView : MvxViewController<SignUpViewModel>
    {
        private Dictionary<int, string> makeOptions, colorOptions, modelOptions;
        private List<string> makeList, colorList, modelList;
        int PhoneNumberCount;
        public SignUpView() : base("SignUpView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            //Credentials_Container.ContentSize = new CGSize(400, 2300);
            makeList = new List<string>();
            colorList = new List<string>();
            modelList = new List<string>();
            
            PhoneNumberFld.AddTarget(ValueChanged, UIControlEvent.EditingChanged);
            getPickerData();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
        private void ValueChanged(object sender, EventArgs e)
        {
            if (PhoneNumberFld.Text.Length > PhoneNumberCount)
            {
                string value = PhoneNumberFld.Text;
                if (PhoneNumberFld.Text.Length == 1)
                {
                    value = "(" + value;
                    PhoneNumberFld.Text = value;
                }
                if (PhoneNumberFld.Text.Length == 4)
                {
                    value = value + ")";
                    PhoneNumberFld.Text = value;
                }
                if (PhoneNumberFld.Text.Length == 8)
                {
                    value = value + "-";
                    PhoneNumberFld.Text = value;
                }
                if (PhoneNumberFld.Text.Length > 13)
                {
                    value = value.Remove(value.Length - 1);
                    PhoneNumberFld.Text = value;
                }
            }
           
            PhoneNumberCount = PhoneNumberFld.Text.Length;

        }

        partial void VehicleMake_SpinnerTouchEnd(UITextField sender)
        {
            if (VehicleMakeFld.Text != null)
            {
                getModelList();
            }
        }

        partial void BackButton(UIButton sender)
        {
            ViewModel.NavigateToLogin();
        }
        private void MakePicker(List<string> source, UITextField textField)
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
            //await ViewModel.getVehicleDetails();
            await ViewModel.GetMakeList();
            makeOptions = ViewModel.manufacturerName;
            colorOptions = ViewModel.ColorList;
            //modelOptions = ViewModel.modelName;

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
            foreach (var colorName in ViewModel.ColorList)
            {
                colorList.Add(colorName.Value);
                if (MembershipDetails.colorNumber == colorName.Key)
                {
                    MembershipDetails.selectedColor = preselectedColor;
                }
                preselectedColor++;
            }

            //makeList.Insert(0, "Select Manufacturer");
            //makeList.RemoveAt(1);
            //colorList.Insert(0, "Select Color");
            //colorList.RemoveAt(1);

            MakePicker(makeList, VehicleMakeFld);
            MakePicker(colorList, VehicleColorFld);
            MakePicker(modelList, VehicleModelFld);
        }
        private async void getModelList()
        {
            VehicleModelFld.Text = string.Empty;
            modelList.Clear();
            await ViewModel.GetModelList(VehicleMakeFld.Text);

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
                //modelList.Insert(0, "Select Model");
                //modelList.RemoveAt(1);
                MakePicker(modelList, VehicleModelFld);
            }
        }
        partial void CreateBtn_Touch(UIButton sender)
        {
            ViewModel.FirstName = FirstNameFld.Text;
            ViewModel.LastName = LastNameFld.Text;
            ViewModel.EmailAddress = EmailIdFld.Text;
            ViewModel.PhoneNumber = PhoneNumberFld.Text;
            ViewModel.Password = PasswordFld.Text;
            ViewModel.ConfirmPassword = ConfirmPasswordFld.Text;
            ViewModel.VehicleMake = VehicleMakeFld.Text;
            ViewModel.VehicleColor = VehicleColorFld.Text;
            ViewModel.VehicleModel = VehicleModelFld.Text;

            var makeIndex = 0;
            foreach (var item in makeList)
            {
                if (VehicleMakeFld.Text == item)
                {
                    MembershipDetails.selectedMake = makeIndex;
                    var selected = this.ViewModel.makeList.Make.ElementAt(makeIndex);
                    MembershipDetails.vehicleMakeNumber = selected.MakeId;
                    MembershipDetails.vehicleMakeName = selected.MakeValue;
                }
                makeIndex++;
            }

            var modelIndex = 0;
            foreach (var item in modelList)
            {
                if (VehicleModelFld.Text == item)
                {
                    MembershipDetails.selectedModel = modelIndex;
                    var selected = this.ViewModel.modelList.Model.ElementAt(modelIndex);
                    MembershipDetails.modelNumber = selected.ModelId;
                    MembershipDetails.modelName = selected.ModelValue;
                }
                modelIndex++;
            }

            var colorIndex = 0;
            foreach (var item in colorList)
            {
                if (VehicleColorFld.Text == item)
                {
                    MembershipDetails.selectedColor = colorIndex;
                    var selected = this.ViewModel.ColorList.ElementAt(colorIndex);
                    MembershipDetails.colorNumber = selected.Key;
                    MembershipDetails.colorName = selected.Value;
                }
                colorIndex++;
            }

            ViewModel.SignUpCommand();
        }

    }
}

