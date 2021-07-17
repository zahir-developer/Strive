// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;
using Greeter.Services.Network;
using UIKit;

namespace Greeter
{
    public partial class LocationViewController : BaseViewController, IUIPickerViewDelegate, IUIPickerViewDataSource
    {
        // Data
        const string SCREEN_TITLE = "Location";
        const string PICKER_TOOLBAR_TITLE = SCREEN_TITLE;

        //public string[] locs = new string[] {
        //    "Main Street 1",
        //    "Main Street 2",
        //    "Main Street 3"
        //};

        string[] locs;
        List<Location> locations;

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

            _ = GetData();

            // For Restricting typing in the location field
            tfLocation.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            //Clicks
            btnDropdown.TouchUpInside += delegate
            {
                tfLocation.BecomeFirstResponder();
            };

            btnNext.TouchUpInside += delegate
            {
                if (!string.IsNullOrEmpty(tfLocation.Text))
                {
                    int pos = (int)pvLoc.SelectedRowInComponent(0);
                    AppSettings.LocationID = locations[pos].ID;
                    AppSettings.WashTime = locations[pos].WashTimeMinutes;
                    NavigateToTabsScreen();
                }
                else ShowAlertMsg(Common.Messages.LOCATION_EMPTY, titleTxt: Common.Messages.LOCATION);
            };
        }

        async Task GetData()
        {
            ShowActivityIndicator();
            var response = await new GeneralApiService().GetLocations();
            HideActivityIndicator();

            if (response.IsNoInternet())
            {
                ShowAlertMsg(response.Message);
                return;
            }

            if (response.IsSuccess())
            {
                locations = response?.Locations;

                locs = locations?.Select(x => x.Name).ToArray();
                pvLoc.ReloadComponent(0);
            }
        }

        void Initialise()
        {
            NavigationController.NavigationBar.Hidden = true;

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
            if (locs?.Length > 0)
            {
                int pos = (int)pvLoc.SelectedRowInComponent(0);
                tfLocation.Text = locs[pos];
            }
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
            return locs?.Length ?? 0;
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
