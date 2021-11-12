using System;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Messenger;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public partial class LocationView : MvxViewController<LocationSelectViewModel>
    {
        public LocationView() : base("LocationView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;
            locProceedBtn.Layer.CornerRadius = 3;
            
            var pickerView = new UIPickerView();
            var PickerViewModel = new LocationPicker(ViewModel, pickerView);
            pickerView.Model = PickerViewModel;
            pickerView.ShowSelectionIndicator = true;
            AddPickerToolbar(locationTextField, "Location", PickerDone);
            locationTextField.InputView = pickerView;

            var set = this.CreateBindingSet<LocationView, LocationSelectViewModel>();
            set.Bind(locationTextField).To(vm => vm.ItemLocation);            
            set.Apply();

            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
        }

        void PickerDone()
        {            
            if (locationTextField.Text == "")
            {
                locationTextField.Text = EmployeeData.EmployeeDetails.EmployeeLocations[0].LocationName;
                EmployeeData.selectedLocationId = EmployeeData.EmployeeDetails.EmployeeLocations[0].LocationId;
            }
            View.EndEditing(true);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void locProceedTouch(UIButton sender)
        {
            

            if (locationTextField.Text == "")
            {
                EmployeeData.selectedLocationId = 0;
                ViewModel.NextScreen();
            }
            else
            {
                IUserDialogs dialogs = Mvx.IoCProvider.Resolve<IUserDialogs>();
                dialogs.ShowLoading("Loading", MaskType.Gradient);
                ViewModel.NextScreen();
            }
        }

        public void AddPickerToolbar(UITextField textField, string title, Action action)
        {
            const string CANCEL_BUTTON_TXT = "Cancel";
            const string DONE_BUTTON_TXT = "Done";

            var toolbarDone = new UIToolbar();
            toolbarDone.SizeToFit();

            var barBtnCancel = new UIBarButtonItem(CANCEL_BUTTON_TXT, UIBarButtonItemStyle.Plain, (sender, s) =>
            {
                textField.EndEditing(false);
            });

            var barBtnDone = new UIBarButtonItem(DONE_BUTTON_TXT, UIBarButtonItemStyle.Done, (sender, s) =>
            {
                textField.EndEditing(false);
                action.Invoke();
            });

            var barBtnSpace = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);

            var lbl = new UILabel();
            lbl.Text = title;
            lbl.TextAlignment = UITextAlignment.Center;
            lbl.Font = UIFont.BoldSystemFontOfSize(size: 16.0f);
            var lblBtn = new UIBarButtonItem(lbl);

            toolbarDone.Items = new UIBarButtonItem[] { barBtnCancel, barBtnSpace, lblBtn, barBtnSpace, barBtnDone };
            textField.InputAccessoryView = toolbarDone;
        }
    }
}

