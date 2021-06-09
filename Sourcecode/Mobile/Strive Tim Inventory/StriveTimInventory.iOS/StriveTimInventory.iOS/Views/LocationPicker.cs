using System;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public class LocationPicker : MvxPickerViewModel
    {
        LocationSelectViewModel ViewModel;
        public LocationPicker(LocationSelectViewModel viewModel, UIPickerView pickerView) : base(pickerView)
        {
            ViewModel = viewModel;
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            ViewModel.locationCommand(EmployeeData.EmployeeDetails.EmployeeLocations[(int)row]);
        }

        public override nint GetRowsInComponent(UIPickerView picker, nint component)
        {
            return EmployeeData.EmployeeDetails.EmployeeLocations.Count;
        }

        public override string GetTitle(UIPickerView picker, nint row, nint component)
        {            
            return EmployeeData.EmployeeDetails.EmployeeLocations[(int)row].LocationName;
        }
    }
}
