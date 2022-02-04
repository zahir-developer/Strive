using System;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.Employee;
using Strive.Core.Utils.TimInventory;
using Strive.Core.ViewModels.Employee;
using Strive.Core.ViewModels.Employee.CheckOut;
using UIKit;

namespace StriveEmployee.iOS.Views.CheckOut
{
    public class LocationPicker : MvxPickerViewModel
    {
        CheckOutViewModel ViewModel;
        public LocationPicker(CheckOutViewModel viewModel , UIPickerView pickerView) : base(pickerView)
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