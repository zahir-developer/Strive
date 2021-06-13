using System;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using MvvmCross.Plugin.Messenger;
using Strive.Core.Utils;
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
            locationTextField.InputView = pickerView;

            var set = this.CreateBindingSet<LocationView, LocationSelectViewModel>();
            set.Bind(locationTextField).To(vm => vm.ItemLocation);            
            set.Apply();

            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        partial void locProceedTouch(UIButton sender)
        {
            ViewModel.NextScreen();
        }
    }
}

