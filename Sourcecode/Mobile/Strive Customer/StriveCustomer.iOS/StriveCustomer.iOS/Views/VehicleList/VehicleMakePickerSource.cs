using System;
using System.Collections.Generic;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public class VehicleMakePickerSource : UIPickerViewModel
    {
        List<string> makeList;
        UITextField textValue;
        public string SelectedValue;
        public EventHandler ValueChanged;
        public VehicleMakePickerSource(List<string> sourceList, UITextField textField)
        {
            this.makeList = sourceList;
            this.textValue = textField;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return makeList.Count;
        }
        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }
        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return makeList[(int)row];
        }
        public override void Selected(UIPickerView pickerView, nint row, nint component)
        {
            if (makeList.Count!=0)
            {
                var selectedItem = makeList[(int)row];
                SelectedValue = selectedItem;
                ValueChanged?.Invoke(null, null);
                textValue.Text = SelectedValue;
            }
            
        }        
    }
}
