// This file has been autogenerated from a class added in the UI designer.

using System;
using Foundation;
using Greeter.Common;
using Greeter.Extensions;
using UIKit;

namespace Greeter
{
    public partial class LocationViewController : BaseViewController, IUIPickerViewDataSource, IUIPickerViewDelegate
    {
        // Data
        public string[] locs = new string[] {
            "Main Street 1",
            "Main Street 2",
            "Main Street 3"
        };

        // Views
        UIPickerView pvLoc = new();

        public LocationViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Initial UI Settings
            tfLocation.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfLocation.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);

            //pvLoc.Model = new LocationModel();

            pvLoc.Delegate = this;

            tfLocation.InputView = pvLoc;

            btnNext.TouchUpInside += delegate
            {
                NavigateToTabsScreen();
            };
        }

        UIStoryboard GetStoryboard(string name)
        {
            return UIStoryboard.FromName(name, null);
        }

        UIStoryboard GetHomeStorybpard()
        {
            return GetStoryboard(StoryBoardNames.HOME);
        }

        UIViewController GetViewController(UIStoryboard sb, Type t)
        {
            //string dsa = nameof(t);

            return sb.InstantiateViewController(nameof(TabViewController));
        }

        void NavigateToTabsScreen()
        {
            UIViewController vcTabs = GetViewController(GetHomeStorybpard(), typeof(TabViewController));

            NavigateToWithAnim(vcTabs);
        }

        public nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return locs.Length;
        }

        [Export("pickerView:didSelectRow:inComponent:")]
        public void Selected(UIPickerView pickerView, nint row, nint component)
        {
            tfLocation.Text = locs[row];
        }

        [Export("pickerView:titleForRow:forComponent:")]
        public string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return locs[row];
        }
    }

    //public class LocationModel : UIPickerViewModel
    //{
    //    public string[] locs = new string[] {
    //        "Main Street 1",
    //        "Main Street 2",
    //        "Main Street 3"
    //    };

    //    //private UILabel personLabel;

    //    //public LocationModel(UILabel personLabel)
    //    //{
    //    //this.personLabel = personLabel;
    //    //}

    //    public LocationModel()
    //    {

    //    }

    //    public override nint GetComponentCount(UIPickerView pickerView)
    //    {
    //        return 1;
    //    }

    //    public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
    //    {
    //        return locs.Length;
    //    }

    //    public override string GetTitle(UIPickerView pickerView, nint row, nint component)
    //    {
    //        return locs[row];
    //    }

    //    public override void Selected(UIPickerView pickerView, nint row, nint component)
    //    {
    //        //personLabel.Text = $"This person is: {names[pickerView.SelectedRowInComponent(0)]},\n they are number {pickerView.SelectedRowInComponent(1)}";
    //    }
    //}
}
