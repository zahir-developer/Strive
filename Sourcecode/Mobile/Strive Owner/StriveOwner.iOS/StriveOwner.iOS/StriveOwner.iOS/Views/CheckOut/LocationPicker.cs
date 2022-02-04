using System;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views.CheckOut
{
    public class LocationPicker : MvxPickerViewModel
    {
        CheckOutViewModel ViewModel;
        public LocationPicker(CheckOutViewModel viewModel, UIPickerView pickerView) : base(pickerView)
        {
            ViewModel = viewModel;
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            ViewModel.locationID = ViewModel.EmployeeLocations[(int)row].LocationId;
            ViewModel.ItemLocation = ViewModel.EmployeeLocations[(int)row].LocationName;
        }

        public override nint GetRowsInComponent(UIPickerView picker, nint component)
        {
            return ViewModel.EmployeeLocations.Count;
        }

        public override string GetTitle(UIPickerView picker, nint row, nint component)
        {
            return ViewModel.EmployeeLocations[(int)row].LocationName;
        }

    }
}
