using System;
using System.Collections.ObjectModel;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views.Inventory
{
    public class InventoryPicker : MvxPickerViewModel
    {
        public ObservableCollection<VendorDetail> VendorList;
        InventoryEditViewModel ViewModel;
        public InventoryPicker(UIPickerView pickerView, InventoryEditViewModel viewModel) : base(pickerView)
        {
            ViewModel = viewModel;
            VendorList = ViewModel.VendorList;
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            ViewModel.PickerSelectionCommand(VendorList[(int)row]);
        }

        public override nint GetRowsInComponent(UIPickerView picker, nint component)
        {
            return VendorList.Count;
        }

        public override string GetTitle(UIPickerView picker, nint row, nint component)
        {
            return VendorList[(int)row].VendorName;
        }
    }
}
