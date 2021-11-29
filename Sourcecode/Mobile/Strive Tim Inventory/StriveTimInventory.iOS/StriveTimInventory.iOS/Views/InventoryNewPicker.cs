using System;
using System.Collections.ObjectModel;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public class InventoryNewPicker : MvxPickerViewModel
    {
        public ObservableCollection<LocationDetail> LocationNames;
        public ObservableCollection<Code> TypeList;
        InventoryEditViewModel ViewModel;
        bool isLocation;

        public InventoryNewPicker(UIPickerView pickerView, InventoryEditViewModel viewModel, bool isLocationPicker) : base(pickerView)
        {
            ViewModel = viewModel;
            LocationNames = ViewModel.LocationList;
            TypeList = viewModel.ProductTypeList;
            isLocation = isLocationPicker;
        }

        public override void Selected(UIPickerView picker, nint row, nint component)
        {
            if (isLocation)
            {
                ViewModel.setLocationCommand(LocationNames[(int)row]);
            }
            else
            {
                ViewModel.setProdTypeCommand(TypeList[(int)row]);
            }
        }

        public override nint GetRowsInComponent(UIPickerView picker, nint component)
        {
            if (isLocation)
            {
                return LocationNames.Count;
            }
            else
            {
                return TypeList.Count;
            }            
        }

        public override string GetTitle(UIPickerView picker, nint row, nint component)
        {
            if (isLocation)
            {
                return LocationNames[(int)row].LocationName;
            }
            else
            {
                return TypeList[(int)row].CodeValue;
            }          
        }
    }
}
