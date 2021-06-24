// This file has been autogenerated from a class added in the UI designer.

using System;
using Foundation;
using Greeter.Common;
using Greeter.Extensions;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class ServiceQuestionViewController : BaseViewController, IUIPickerViewDelegate, IUIPickerViewDataSource
    {
        // Data
        public string[] data = new string[] {
            "Main Street 1",
            "Main Street 2",
            "Main Street 3"
        };
        string[] SCREEN_TITLES = new string[] { "Wash", "Detail" };

        public ServiceType ServiceType;
        ChoiceType choiceType;

        //Views
        UIPickerView pv = new UIPickerView();

        public ServiceQuestionViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Initialise();

            //Clicks
            btnNext.TouchUpInside += delegate
            {
                NavigateToVerifyScreen();
            };

            //Choice type change
            tfType.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Type;
                //cityCountryPickerView.DataSource = new MyPikcerSource(cities);
                //cityCountryPickerView.Delegate = new CityCountryPikcerDelegate(cities, this);
                //int pos = cities.IndexOf(cityTextField.Text);
                //cityCountryPickerView.Select(pos, Constants.ZERO, false);
            };

            tfMake.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Make;
                //cityCountryPickerView.DataSource = new MyPikcerSource(cities);
                //cityCountryPickerView.Delegate = new CityCountryPikcerDelegate(cities, this);
                //int pos = cities.IndexOf(cityTextField.Text);
                //cityCountryPickerView.Select(pos, Constants.ZERO, false);
            };

            tfColor.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Color;
                //cityCountryPickerView.DataSource = new MyPikcerSource(cities);
                //cityCountryPickerView.Delegate = new CityCountryPikcerDelegate(cities, this);
                //int pos = cities.IndexOf(cityTextField.Text);
                //cityCountryPickerView.Select(pos, Constants.ZERO, false);
            };

            tfBarcode.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Barcode;
                //cityCountryPickerView.DataSource = new MyPikcerSource(cities);
                //cityCountryPickerView.Delegate = new CityCountryPikcerDelegate(cities, this);
                //int pos = cities.IndexOf(cityTextField.Text);
                //cityCountryPickerView.Select(pos, Constants.ZERO, false);
            };

            tfWashPkg.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Washpackage;
                //cityCountryPickerView.DataSource = new MyPikcerSource(cities);
                //cityCountryPickerView.Delegate = new CityCountryPikcerDelegate(cities, this);
                //int pos = cities.IndexOf(cityTextField.Text);
                //cityCountryPickerView.Select(pos, Constants.ZERO, false);
            };

            tfDetailPkg.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.DetailPackage;
                //cityCountryPickerView.DataSource = new MyPikcerSource(cities);
                //cityCountryPickerView.Delegate = new CityCountryPikcerDelegate(cities, this);
                //int pos = cities.IndexOf(cityTextField.Text);
                //cityCountryPickerView.Select(pos, Constants.ZERO, false);
            };

            tfUpcharge.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Upcharge;
                //cityCountryPickerView.DataSource = new MyPikcerSource(cities);
                //cityCountryPickerView.Delegate = new CityCountryPikcerDelegate(cities, this);
                //int pos = cities.IndexOf(cityTextField.Text);
                //cityCountryPickerView.Select(pos, Constants.ZERO, false);
            };

            tfAdditionalService.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.AdditionalService;
                //cityCountryPickerView.DataSource = new MyPikcerSource(cities);
                //cityCountryPickerView.Delegate = new CityCountryPikcerDelegate(cities, this);
                //int pos = cities.IndexOf(cityTextField.Text);
                //cityCountryPickerView.Select(pos, Constants.ZERO, false);
            };

            tfAirFreshner.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.AirFreshner;
                //cityCountryPickerView.DataSource = new MyPikcerSource(cities);
                //cityCountryPickerView.Delegate = new CityCountryPikcerDelegate(cities, this);
                //int pos = cities.IndexOf(cityTextField.Text);
                //cityCountryPickerView.Select(pos, Constants.ZERO, false);
            };
        }

        void Initialise()
        {
            NavigationController.NavigationBar.Hidden = false;
            Title = ServiceType == ServiceType.Wash ? SCREEN_TITLES[0] : SCREEN_TITLES[1];

            viewHeader.AddHearderViewShadow();

            DateTime dt = GetCurrentDate();
            lblDate.Text = dt.ToString(Constants.DATE_FORMAT);
            lblTime.Text = dt.ToString(Constants.TIME_FORMAT);

            tfType.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfType.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            tfMake.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfMake.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            tfColor.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfColor.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            tfBarcode.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfBarcode.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            tfUpcharge.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfUpcharge.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            tfWashPkg.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfWashPkg.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            tfDetailPkg.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfDetailPkg.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            tfAdditionalService.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfAdditionalService.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            tfAirFreshner.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfAirFreshner.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);

            ChangeScreenType(ServiceType);

            AddPickerToolbar(tfType, tfType.Placeholder, PickerDone);
            AddPickerToolbar(tfMake, tfMake.Placeholder, PickerDone);
            AddPickerToolbar(tfColor, tfColor.Placeholder, PickerDone);
            AddPickerToolbar(tfBarcode, tfBarcode.Placeholder, PickerDone);
            AddPickerToolbar(tfWashPkg, tfWashPkg.Placeholder, PickerDone);
            AddPickerToolbar(tfDetailPkg, tfDetailPkg.Placeholder, PickerDone);
            AddPickerToolbar(tfUpcharge, tfUpcharge.Placeholder, PickerDone);
            AddPickerToolbar(tfAdditionalService, tfAdditionalService.Placeholder, PickerDone);
            AddPickerToolbar(tfAirFreshner, tfAirFreshner.Placeholder, PickerDone);

            tfType.InputView = pv;
            tfMake.InputView = pv;
            tfColor.InputView = pv;
            tfBarcode.InputView = pv;
            tfUpcharge.InputView = pv;
            tfWashPkg.InputView = pv;
            tfDetailPkg.InputView = pv;
            tfAdditionalService.InputView = pv;
            tfAirFreshner.InputView = pv;

            pv.DataSource = this;
            pv.Delegate = this;
        }

        //public override void ViewDidDisappear(bool animated)
        //{
        //    base.ViewDidDisappear(animated);
        //}

        DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        void PickerDone()
        {
            int pos = (int)pv.SelectedRowInComponent(0);

            switch (choiceType)
            {
                case ChoiceType.Type:
                    tfType.Text = data[pos];
                    break;
                case ChoiceType.Make:
                    tfMake.Text = data[pos];
                    break;
                case ChoiceType.Color:
                    tfColor.Text = data[pos];
                    break;
                case ChoiceType.Barcode:
                    tfBarcode.Text = data[pos];
                    break;
                case ChoiceType.Upcharge:
                    tfUpcharge.Text = data[pos];
                    break;
                case ChoiceType.AdditionalService:
                    tfAdditionalService.Text = data[pos];
                    break;
                case ChoiceType.AirFreshner:
                    tfAirFreshner.Text = data[pos];
                    break;
                case ChoiceType.Washpackage:
                    tfWashPkg.Text = data[pos];
                    break;
                case ChoiceType.DetailPackage:
                    tfDetailPkg.Text = data[pos];
                    break;
            }
        }

        void ChangeScreenType(ServiceType type)
        {
            switch (type)
            {
                case ServiceType.Wash:
                    tfdetailHeight.Constant = 0;
                    tfdetailTop.Constant = 0;
                    break;
                case ServiceType.Detail:
                    tfwashHeight.Constant = 0;
                    tfwashTop.Constant = 0;
                    break;
            }
        }

        void NavigateToVerifyScreen()
        {
            UIViewController vc = GetViewController(GetHomeStorybpard(), nameof(VerifyVehicleInfoViewController));
            NavigateToWithAnim(vc);
        }

        public nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return data.Length;
        }

        [Export("pickerView:didSelectRow:inComponent:")]
        public void Selected(UIPickerView pickerView, nint row, nint component)
        {

        }

        [Export("pickerView:titleForRow:forComponent:")]
        public string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            return data[row];
        }
    }

    public enum ServiceType
    {
        Wash,
        Detail
    }

    public enum ChoiceType
    {
        Type,
        Make,
        Color,
        vehicle,
        Barcode,
        Upcharge,
        AdditionalService,
        AirFreshner,
        Washpackage,
        DetailPackage
    }
}
