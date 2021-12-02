using System;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.Employee;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.Employee;
using UIKit;

namespace StriveEmployee.iOS.Views.PayRoll
{
    public class LocationPicker : MvxPickerViewModel
    {
        PayRollViewModel ViewModel;
        public LocationPicker(PayRollViewModel viewModel, UIPickerView pickerView) : base(pickerView)
        {
            ViewModel = viewModel;
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            ViewModel.locationCommand(ViewModel.EmployeeLocations[(int)row]);
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