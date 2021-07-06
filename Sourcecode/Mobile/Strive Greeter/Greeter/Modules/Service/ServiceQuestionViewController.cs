// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Network;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class ServiceQuestionViewController : BaseViewController, IUIPickerViewDelegate, IUIPickerViewDataSource
    {
        string[] sampleData = new string[] {
            "Main Street 1",
            "Main Street 2",
            "Main Street 3"
        };

        string[] data;

        string[] SCREEN_TITLES = new string[] { "Wash", "Detail" };

        public ServiceType ServiceType;
        ChoiceType choiceType;

        //Selected Items
        public string Barcode;
        public long MakeID;
        public long ModelID;
        public string Model;
        public long ColorID;
        public long ClientID;
        public long VehicleID;

        //int modelId;
        string make;
        string color;
        //long jobStatusId;
        long jobTypeId;
        JobItem mainService;
        JobItem upcharge;
        JobItem additional;
        JobItem airFreshner;

        // Data
        List<Make> Makes;
        List<Model> Models;
        List<Code> Colors;
        List<ServiceDetail> WashPackages;
        List<ServiceDetail> DetailPackages;
        List<ServiceDetail> Upcharges;
        List<ServiceDetail> AdditionalServices;
        List<ServiceDetail> AirFreshners;

        string[] makes;
        string[] models;
        string[] colors;
        string[] washPackages;
        string[] detailPackages;
        string[] upcharges;
        string[] additionalServices;
        string[] airFreshners;

        //Views
        UIPickerView pv = new UIPickerView();

        public ServiceQuestionViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Initialise();

            _ = GetData();

            //Clicks
            btnNext.TouchUpInside += delegate
            {
                _ = CreateService(MakeID, ModelID, ColorID, jobTypeId, mainService, upcharge, additional, airFreshner, ClientID, VehicleID);
            };

            //Choice type change
            tfMake.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Type;
                UpdatePickerView(makes, pv, tfMake.Text);
            };

            btnTypeDropdown.TouchUpInside += delegate
            {
                tfMake.BecomeFirstResponder();
            };

            tfModel.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Make;
                UpdatePickerView(models, pv, tfModel.Text, false);
            };

            btnMakeDropdown.TouchUpInside += delegate
            {
                tfMake.BecomeFirstResponder();
            };

            tfColor.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Color;
                UpdatePickerView(colors, pv, tfColor.Text);
            };

            btnColorDropdown.TouchUpInside += delegate
            {
                tfColor.BecomeFirstResponder();
            };

            //tfBarcode.EditingDidBegin += delegate
            //{
            //    choiceType = ChoiceType.Barcode;
            //    data = sampleData;
            //    //cityCountryPickerView.DataSource = new MyPikcerSource(cities);
            //    //cityCountryPickerView.Delegate = new CityCountryPikcerDelegate(cities, this);
            //    //int pos = cities.IndexOf(cityTextField.Text);
            //    //cityCountryPickerView.Select(pos, Constants.ZERO, false);
            //};

            tfWashPkg.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Washpackage;
                UpdatePickerView(washPackages, pv, tfWashPkg.Text);
            };

            btnWashPkgDropdown.TouchUpInside += delegate
            {
                tfWashPkg.BecomeFirstResponder();
            };

            tfDetailPkg.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.DetailPackage;
                UpdatePickerView(detailPackages, pv, tfDetailPkg.Text);
            };

            btnDetailPkgDropdown.TouchUpInside += delegate
            {
                tfDetailPkg.BecomeFirstResponder();
            };

            tfUpcharge.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Upcharge;
                UpdatePickerView(upcharges, pv, tfUpcharge.Text);
            };

            btnUpchargeDropdown.TouchUpInside += delegate
            {
                tfUpcharge.BecomeFirstResponder();
            };

            tfAdditionalService.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.AdditionalService;
                UpdatePickerView(additionalServices, pv, tfAdditionalService.Text);
            };

            btnAddtionalDropdown.TouchUpInside += delegate
            {
                tfAdditionalService.BecomeFirstResponder();
            };

            tfAirFreshner.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.AirFreshner;
                UpdatePickerView(airFreshners, pv, tfAirFreshner.Text);
            };

            btnAirFReshnersDropdown.TouchUpInside += delegate
            {
                tfAirFreshner.BecomeFirstResponder();
            };

            btnCancel.TouchUpInside += delegate
            {
                this.NavigationController.PopViewController(true);
            };
        }

        void UpdatePickerView(string[] data, UIPickerView pv, string selectedText, bool isPreviousValueSelect = true)
        {
            this.data = data;
            pv.ReloadComponent(0);
            int pos = 0;
            if (isPreviousValueSelect && data != null)
            {
                pos = Array.IndexOf(data, selectedText);
            }

            if (pos != -1 && pos < data.Length)
                pv.Select(pos, 0, false);
        }

        //void SelectItem(UIPickerView pv, int pos)
        //{

        //}

        async Task GetModlesByMake(int makeId)
        {
            ShowActivityIndicator();
            var modelsResponse = await new ApiService(new NetworkService()).GetModelsByMake(makeId);
            Models = modelsResponse.ModelList;
            models = Models?.Select(x => x.Name).ToArray();
            HideActivityIndicator();
        }

        async Task GetData()
        {
            ShowActivityIndicator();
            var apiService = new ApiService(new NetworkService());

            var makesResponse = await apiService.GetAllMake();
            Makes = makesResponse?.MakeList;
            makes = Makes?.Select(x => x.Name).ToArray();

            var colorResponse = await apiService.GetGlobalData("VEHICLECOLOR");
            Colors = colorResponse?.Codes;
            colors = colorResponse?.Codes.Select(x => x.Name).ToArray();

            var jobTypeResponse = await apiService.GetGlobalData("JOBTYPE");

            var allServiceResponse = await apiService.GetAllSericeDetails(AppSettings.LocationID);
            if (ServiceType == ServiceType.Wash)
            {
                WashPackages = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.WASH_PACKAGE)).ToList();
                washPackages = WashPackages.Select(x => x.Name).ToArray();

                Upcharges = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.WASH_UPCHARGE)).ToList();

                jobTypeId = jobTypeResponse?.Codes.Where(x => x.Name.Equals("Wash")).FirstOrDefault().ID ?? -1;
            }
            else
            {
                DetailPackages = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.DETAIL_PACKAGE)).ToList();
                detailPackages = DetailPackages.Select(x => x.Name).ToArray();

                Upcharges = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.DETAIL_UPCHARGE)).ToList();

                jobTypeId = jobTypeResponse?.Codes.Where(x => x.Name.Equals("Detail")).FirstOrDefault().ID ?? -1;
            }

            upcharges = Upcharges.Select(x => x.Name).ToArray();

            AdditionalServices = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.ADDITIONAL_SERVICES)).ToList();
            additionalServices = AdditionalServices.Select(x => x.Name).ToArray();

            AirFreshners = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.AIR_FRESHNERS)).ToList();
            airFreshners = AirFreshners.Select(x => x.Name).ToArray();

            if (!String.IsNullOrEmpty(Barcode))
            {
                make = Makes.Where(x => x.ID == MakeID).FirstOrDefault().Name;
                color = Colors.Where(x => x.ID == ColorID).FirstOrDefault().Name;
                UpdateBarcodeData();
            }

            HideActivityIndicator();
        }

        void UpdateBarcodeData()
        {
            if (!String.IsNullOrEmpty(Barcode))
            {
                btnTypeDropdown.Hidden = true;
                btnMakeDropdown.Hidden = true;
                btnColorDropdown.Hidden = true;

                tfMake.UserInteractionEnabled = false;
                tfModel.UserInteractionEnabled = false;
                tfColor.UserInteractionEnabled = false;

                tfBarcode.Text = Barcode;
                tfMake.Text = make;
                tfModel.Text = Model;
                tfColor.Text = color;
            }
        }

        async Task CreateService(long makeId, long modelId, long colorId, long jobTypeId, JobItem mainService, JobItem upcharge, JobItem additional, JobItem airFreshners, long vehicleId = 0, long clientId = 0)
        {
            if (makeId == 0 || modelId == 0 || colorId == 0 || mainService == null)
            {
                string msg = string.Empty;
                if (ServiceType == ServiceType.Wash)
                    msg = string.Format(Common.Messages.WARNING_FOR_MANDATORY_FILEDS_IN_SERVICE, Common.Messages.WAH_PACKAGE);
                else
                    msg = string.Format(Common.Messages.WARNING_FOR_MANDATORY_FILEDS_IN_SERVICE, Common.Messages.DETAIL_PACKAGE);
                ShowAlertMsg(msg);
                return;
            }

            NavigateToVerifyScreen();
        }

        void Initialise()
        {
            string[] SCREEN_TITLES = new string[] { "Wash", "Detail" };

            NavigationController.NavigationBar.Hidden = false;
            Title = ServiceType == ServiceType.Wash ? SCREEN_TITLES[0] : SCREEN_TITLES[1];

            viewHeader.AddHearderViewShadow();

            DateTime dt = GetCurrentDate();
            lblDate.Text = dt.ToString(Constants.DATE_FORMAT);
            lblTime.Text = dt.ToString(Constants.TIME_FORMAT);

            tfMake.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfMake.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
            tfModel.AddLeftPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
            tfModel.AddRightPadding(UIConstants.TEXT_FIELD_RIGHT_BUTTON_PADDING);
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

            AddPickerToolbar(tfMake, tfMake.Placeholder, PickerDone);
            AddPickerToolbar(tfModel, tfModel.Placeholder, PickerDone);
            AddPickerToolbar(tfColor, tfColor.Placeholder, PickerDone);
            //AddPickerToolbar(tfBarcode, tfBarcode.Placeholder, PickerDone);
            AddPickerToolbar(tfWashPkg, tfWashPkg.Placeholder, PickerDone);
            AddPickerToolbar(tfDetailPkg, tfDetailPkg.Placeholder, PickerDone);
            AddPickerToolbar(tfUpcharge, tfUpcharge.Placeholder, PickerDone);
            AddPickerToolbar(tfAdditionalService, tfAdditionalService.Placeholder, PickerDone);
            AddPickerToolbar(tfAirFreshner, tfAirFreshner.Placeholder, PickerDone);

            tfMake.InputView = pv;
            tfModel.InputView = pv;
            tfColor.InputView = pv;
            //tfBarcode.InputView = pv;
            tfUpcharge.InputView = pv;
            tfWashPkg.InputView = pv;
            tfDetailPkg.InputView = pv;
            tfAdditionalService.InputView = pv;
            tfAirFreshner.InputView = pv;

            // For Restricting typing in the location field
            tfMake.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            tfModel.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            tfColor.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            tfBarcode.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            tfUpcharge.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            tfWashPkg.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            tfDetailPkg.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            tfAdditionalService.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            tfAirFreshner.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            pv.DataSource = this;
            pv.Delegate = this;
        }

        DateTime GetCurrentDate()
        {
            return DateTime.Now;
        }

        void PickerDone()
        {
            int pos = (int)pv.SelectedRowInComponent(0);

            if (data?.Length > 0)
                switch (choiceType)
                {
                    case ChoiceType.Type:
                        if (tfMake.Text != data[pos])
                        {
                            tfMake.Text = data[pos];
                            tfModel.Text = string.Empty;
                            _ = GetModlesByMake(Makes[pos].ID);
                            MakeID = Makes[pos].ID;
                        }
                        break;
                    case ChoiceType.Make:
                        tfModel.Text = data[pos];
                        ModelID = Models[pos].ID;
                        break;
                    case ChoiceType.Color:
                        tfColor.Text = data[pos];
                        ColorID = Colors[pos].ID;
                        break;
                    case ChoiceType.Barcode:
                        tfBarcode.Text = data[pos];
                        break;
                    case ChoiceType.Upcharge:
                        tfUpcharge.Text = data[pos];
                        upcharge = upcharge ?? new JobItem();
                        upcharge.ServiceId = Upcharges[pos].ID;
                        break;
                    case ChoiceType.AdditionalService:
                        tfAdditionalService.Text = data[pos];
                        additional = additional ?? new JobItem();
                        additional.ServiceId = AdditionalServices[pos].ID;
                        break;
                    case ChoiceType.AirFreshner:
                        tfAirFreshner.Text = data[pos];
                        airFreshner = airFreshner ?? new JobItem();
                        airFreshner.ServiceId = AirFreshners[pos].ID;
                        break;
                    case ChoiceType.Washpackage:
                        tfWashPkg.Text = data[pos];
                        mainService = mainService ?? new JobItem();
                        mainService.ServiceId = WashPackages[pos].ID;
                        //price = washPackages[pos].pr,
                        break;
                    case ChoiceType.DetailPackage:
                        tfDetailPkg.Text = data[pos];
                        mainService = mainService ?? new JobItem();
                        mainService.ServiceId = DetailPackages[pos].ID;
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
            var vc = (VerifyVehicleInfoViewController)GetViewController(GetHomeStorybpard(), nameof(VerifyVehicleInfoViewController));
            vc.MakeID = MakeID;
            vc.ModelID = ModelID;
            vc.ColorID = ColorID;
            vc.Make = tfMake.Text;
            vc.Model = tfModel.Text;
            vc.Color = tfColor.Text;
            vc.Barcode = Barcode;
            vc.JobTypeID = jobTypeId;
            vc.MainService = mainService;
            vc.Upcharge = upcharge;
            vc.Additional = additional;
            vc.AirFreshner = airFreshner;
            NavigateToWithAnim(vc);
        }

        public nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return data?.Length ?? 0;
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

    //enum fds
    //{
    //    [EnumMember(Value = "AirFresheners")]
    //    AIR_FRESHNERS,
    //}

    //enum fsd
    //{
    //    "Air Fresheners"
    //}

    class ServiceTypes
    {
        public const string AIR_FRESHNERS = "Air Fresheners";
        public const string ADDITIONAL_SERVICES = "Additional Services";
        public const string WASH_PACKAGE = "Wash Package";
        public const string DETAIL_PACKAGE = "Detail Package";
        public const string WASH_UPCHARGE = "Wash-Upcharge";
        public const string DETAIL_UPCHARGE = "Detail-Upcharge";
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
