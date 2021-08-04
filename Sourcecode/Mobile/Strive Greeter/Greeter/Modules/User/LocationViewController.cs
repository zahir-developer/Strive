// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.Extensions;
using Greeter.Services.Network;
using UIKit;

namespace Greeter
{
    public partial class LocationViewController : BaseViewController, IUIPickerViewDelegate, IUIPickerViewDataSource
    {
        // Data
        const string SCREEN_TITLE = "Location";
        const string PICKER_TOOLBAR_TITLE = SCREEN_TITLE;

        public string[] locs = new string[] {
            "Main Street 1",
            "Main Street 2",
            "Main Street 3"
        };

        // Views
        UIPickerView pvLoc = new UIPickerView();

        public LocationViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Initial UI
            Initialise();

            GetData();

            //Clicks
            tfLocation.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            //btnDropdown.TouchUpInside += delegate
            //{
            //    tfLocation.BecomeFirstResponder();
            //};

            btnNext.TouchUpInside += delegate
            {
                NavigateToTabsScreen();
            };
        }

        async Task GetData()
        {
            var response1 = await new ApiService(new NetworkService()).GetLocations();
            var response2 = await new ApiService(new NetworkService()).GetCheckoutList();
        }

        void Initialise()
        {
            NavigationItem.HidesBackButton = true;

            Title = SCREEN_TITLE;

            tfLocation.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfLocation.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            AddPickerToolbar(tfLocation, PICKER_TOOLBAR_TITLE, PickerDone);

            tfLocation.InputView = pvLoc;
            pvLoc.DataSource = this;
            pvLoc.Delegate = this;
        }

        void PickerDone()
        {
            int pos = (int)pvLoc.SelectedRowInComponent(0);
            tfLocation.Text = locs[pos];
        }

        void NavigateToTabsScreen()
        {
            UIViewController vcTabs = GetViewController(GetHomeStorybpard(), nameof(TabViewController));

            NavigationController.ViewControllers = new UIViewController[] { vcTabs };

            //NavigateToWithAnim(vcTabs);
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