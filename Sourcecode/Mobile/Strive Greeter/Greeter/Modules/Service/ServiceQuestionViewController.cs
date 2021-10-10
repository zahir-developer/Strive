// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundation;
using Greeter.Common;
using Greeter.CustomView;
using Greeter.DTOs;
using Greeter.Extensions;
using Greeter.Services.Api;
using UIKit;

namespace Greeter.Storyboards
{
    public partial class ServiceQuestionViewController : BaseViewController, IUIPickerViewDelegate, IUIPickerViewDataSource, IUITextFieldDelegate, IMultiSelectPickerDelegate
    {
        //string[] sampleData = new string[] {
        //    "Main Street 1",
        //    "Main Street 2",
        //    "Main Street 3"
        //};

        // Common data for picker (Note : whenever dropdown is clicked we need to assign/change this array values)
        string[] data;

        public ServiceType ServiceType;
        ChoiceType choiceType;

        //Selected Items
        public string Barcode;
        public string CustName;
        public long MakeID;
        public long ModelID;
        public string Model;
        public long ColorID;
        public long ClientID;
        public long VehicleID;
        public long UpchargeID;
        public bool IsNewBarcode;

        //int modelId;
        string make;
        string color;
        //long jobStatusId;
        long jobTypeId;
        JobItem mainService;
        JobItem upcharge;
        //JobItem additional;
        List<JobItem> additionalServcies;
        JobItem airFreshner;

        // Data
        List<Make> Makes;
        List<Model> Models;
        List<Code> Colors;
        List<ServiceDetail> WashPackages;
        List<ServiceDetail> DetailPackages;
        List<ServiceDetail> Upcharges;
        List<ServiceDetail> AdditionalServices;
        List<int> AdditionalServicesSelectedPositions;
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
                CreateService(MakeID, ModelID, ColorID, jobTypeId, mainService, upcharge, additionalServcies?.ToArray(), airFreshner, ClientID, VehicleID);
                //ShowMultiselectOptions();
            };

            //Choice type change
            tfMake.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Type;
                UpdatePickerView(makes, pv, tfMake.Text);
            };

            btnMakeDropdown.TouchUpInside += delegate
            {
                tfMake.BecomeFirstResponder();
            };

            tfModel.EditingDidBegin += delegate
            {
                choiceType = ChoiceType.Make;
                UpdatePickerView(models, pv, tfModel.Text);
            };

            btnTypeDropdown.TouchUpInside += delegate
            {
                tfModel.BecomeFirstResponder();
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
                //tfAdditionalService.BecomeFirstResponder();
                
                ShowMultiselectOptions(additionalServices.ToList());
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

        [Export("textFieldShouldReturn:")]
        public bool ShouldReturn(UITextField textField)
        {
            textField.EndEditing(true);
            return true;
        }

        void ShowMultiselectOptions(List<string> options)
        {
            var nc = new UINavigationController();
            var mc = new MultiSelectPicker();
            nc.ViewControllers = new UIViewController[] { mc };
            //mc.Options = new List<string>() { "dfsa", "fsaf" };
            mc.Options = options;
            mc.PickerDelegate = this;

            if (additionalServcies is not null && additionalServcies.Count > 0)
            {
                mc.DefaultSelectedIndex = AdditionalServicesSelectedPositions;
            }

            PresentViewController(nc, true, null);
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

            if (pos != -1 && pos < data?.Length)
                pv.Select(pos, 0, false);
            else
                pv.Select(0, 0, false);
        }

        async Task UpdateModelsByMakeWithoutLoader(long makeId)
        {
            var modelsResponse = await new VehicleApiService().GetModelsByMake(makeId);
            Models = modelsResponse.ModelList;
            models = Models?.Select(x => x.Name).ToArray();
        }

        async Task GetModlesByMake(long makeId)
        {
            ShowActivityIndicator();
            var modelsResponse = await new VehicleApiService().GetModelsByMake(makeId);
            Models = modelsResponse.ModelList;
            models = Models?.Select(x => x.Name).ToArray();
            HideActivityIndicator();
        }

        async Task GetData()
        {
            ShowActivityIndicator();
            var apiService = new GeneralApiService();

            var makesResponse = await apiService.GetAllMake();
            Makes = makesResponse?.MakeList;
            makes = Makes?.Select(x => x.Name).ToArray();

            var colorResponse = await apiService.GetGlobalData("VEHICLECOLOR");
            Colors = colorResponse?.Codes;
            colors = colorResponse?.Codes.Select(x => x.Name).ToArray();

            var jobTypeResponse = await apiService.GetGlobalData("JOBTYPE");

            var washApiService = new WashApiService();

            var allServiceResponse = await washApiService.GetAllSericeDetails(AppSettings.LocationID);
            if (ServiceType == ServiceType.Wash)
            {
                WashPackages = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.WASH_PACKAGE)).ToList();
                washPackages = WashPackages.Select(x => x.Name).ToArray();

                Upcharges = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.WASH_UPCHARGE)).ToList();

                ServiceDetail serviceDetail = new();

                jobTypeId = jobTypeResponse?.Codes.Where(x => x.Name.Equals(ServiceType.Wash.ToString())).FirstOrDefault().ID ?? -1;
            }
            else
            {
                DetailPackages = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.DETAIL_PACKAGE)).ToList();
                detailPackages = DetailPackages.Select(x => x.Name).ToArray();

                Upcharges = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.DETAIL_UPCHARGE)).ToList();

                jobTypeId = jobTypeResponse?.Codes.Where(x => x.Name.Equals(ServiceType.Detail.ToString())).FirstOrDefault().ID ?? -1;
            }

            var upchargesList = Upcharges.Select(x => x.Name + " - " + x.Upcharges).ToList();
            upchargesList.Insert(0, "None");
            upcharges = upchargesList.ToArray();

            AdditionalServices = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.ADDITIONAL_SERVICES)).ToList();
            var additionalServicesList = AdditionalServices.Select(x => x.Name).ToList();
            //additionalServicesList.Insert(0, "None");
            additionalServices = additionalServicesList.ToArray();
            UpdateAdditionalServiceAsNone();

            AirFreshners = allServiceResponse?.ServiceDetailList.Where(x => x.Type.Equals(ServiceTypes.AIR_FRESHNERS)).ToList();
            var airFreshnersList = AirFreshners.Select(x => x.Name).ToList();
            airFreshnersList.Insert(0, "None");
            airFreshners = airFreshnersList.ToArray();
            UpdateAirfreshnerAsNone();

            if (!IsNewBarcode)
            {
                make = Makes.Where(x => x.ID == MakeID).FirstOrDefault().Name;
                color = Colors.Where(x => x.ID == ColorID).FirstOrDefault().Name;

                if (ServiceType == ServiceType.Wash)
                {
                    await UpdateUpchargeForModel(ModelID);
                }
                else
                {
                    UpdateUpchargeAsNone();
                }

                if (string.IsNullOrEmpty(Model))
                {
                    await UpdateModelsByMakeWithoutLoader(MakeID);
                    int index = Array.FindIndex(models, t => t.Equals("unk", StringComparison.InvariantCultureIgnoreCase));
                    if (index != -1)
                    {
                        ModelID = Models[index].ID;
                        Model = models[index];
                    }
                }

                UpdateBarcodeData(Barcode);

                //var barcodeUpcharge = Upcharges?.Where(x => x.ID == UpchargeID).FirstOrDefault();
                //if (barcodeUpcharge is not null)
                //{
                //    upcharge = upcharge ?? new JobItem();
                //    upcharge.ServiceId = barcodeUpcharge.ID;
                //    upcharge.SeriveName = barcodeUpcharge.Name + " - " + barcodeUpcharge.Upcharges;
                //    upcharge.Price = barcodeUpcharge.Price;
                //    upcharge.Time = barcodeUpcharge.Time;
                //}
            }
            else
            {
                //viewCustomerName.Hidden = true;
                lblCustName.Text = Common.Messages.DRIVE_UP;
                UpdateBarcodeText(Barcode);
                UpdateUpchargeAsNone();
            }

            HideActivityIndicator();

#if DEBUG

#endif
        }

        void UpdateBarcodeData(string barcode)
        {
            if (!String.IsNullOrEmpty(barcode))
            {
                btnTypeDropdown.Hidden = true;
                btnMakeDropdown.Hidden = true;
                btnColorDropdown.Hidden = true;

                tfMake.UserInteractionEnabled = false;
                tfModel.UserInteractionEnabled = false;
                tfColor.UserInteractionEnabled = false;

                tfBarcode.Text = barcode;
                tfMake.Text = make;
                tfModel.Text = Model;
                tfColor.Text = color;

                if (!string.IsNullOrEmpty(CustName))
                {
                    lblCustName.Text = CustName;
                    viewCustomerName.Hidden = false;
                }
                else
                {
                    viewCustomerName.Hidden = true;
                }
            }
        }

        void UpdateBarcodeText(string barcode)
        {
            tfBarcode.Text = barcode;
        }

        async Task LoadUpchargeByModel(long modelId)
        {
            ShowActivityIndicator();
            await UpdateUpchargeForModel(modelId);
            HideActivityIndicator();
        }

        void UpdateUpchargeAsNone()
        {
            tfUpcharge.Text = upcharges[0];
        }

        void UpdateAdditionalServiceAsNone()
        {
            //tfAdditionalService.Text = additionalServices[0];
            tfAdditionalService.Text = "None";
        }

        void UpdateAirfreshnerAsNone()
        {
            tfAirFreshner.Text = airFreshners[0];
        }

        async Task UpdateUpchargeForDetailAndModel(long modelId)
        {
            var serviceTypeResponse = await SingleTon.GeneralApiService.GetGlobalData("SERVICETYPE");
            var upchargeServiceTypeId = serviceTypeResponse?.Codes?.Where(x => x.Name.Equals(ServiceTypes.DETAIL_UPCHARGE.ToString())).FirstOrDefault().ID ?? -1;

            if (upchargeServiceTypeId != -1)
            {
                var req = new GetUpchargeReq() { ModelID = ModelID, UpchargeServiceTypeID = upchargeServiceTypeId };
                var upchargeResponse = await SingleTon.WashApiService.GetUpcharge(req);
                if (upchargeResponse is not null && upchargeResponse.Upcharges is not null && upchargeResponse.Upcharges.Length == 1)
                {
                    var selectedUpcharge = upchargeResponse.Upcharges[0];
                    upcharge = upcharge ?? new JobItem();
                    upcharge.ServiceId = selectedUpcharge.ServiceID;
                    upcharge.SeriveName = selectedUpcharge.ServiceName + " - " + selectedUpcharge.Upcharges;
                    upcharge.Price = selectedUpcharge.Price;
                    //upcharge.Time = upchargeResponse.Upcharge.;
                    tfUpcharge.Text = upcharge?.SeriveName;
                }
            }
            else
            {
                UpdateUpchargeAsNone();
            }
        }

        async Task UpdateUpchargeForModel(long modelId)
        {
            var serviceTypeResponse = await SingleTon.GeneralApiService.GetGlobalData("SERVICETYPE");
            var upchargeServiceId = serviceTypeResponse?.Codes?.Where(x => x.Name.Equals(ServiceTypes.WASH_UPCHARGE.ToString())).FirstOrDefault().ID ?? -1;

            if (upchargeServiceId != -1 && modelId != 0)
            {
                var req = new GetUpchargeReq() { ModelID = ModelID, UpchargeServiceTypeID = upchargeServiceId };
                var upchargeResponse = await SingleTon.WashApiService.GetUpcharge(req);
                if (upchargeResponse is not null && upchargeResponse.Upcharges is not null && upchargeResponse.Upcharges.Length == 1)
                {
                    var selectedUpcharge = upchargeResponse.Upcharges[0];
                    upcharge = upcharge ?? new JobItem();
                    upcharge.ServiceId = selectedUpcharge.ServiceID;
                    upcharge.SeriveName = selectedUpcharge.ServiceName + " - " + selectedUpcharge.Upcharges;
                    upcharge.Price = selectedUpcharge.Price;
                    //upcharge.Time = upchargeResponse.Upcharge.;
                    tfUpcharge.Text = upcharge?.SeriveName;
                }
            }
            else
            {
                UpdateUpchargeAsNone();
            }
        }

        void CreateService(long makeId, long modelId, long colorId, long jobTypeId, JobItem mainService, JobItem upcharge, JobItem[] additional, JobItem airFreshners, long vehicleId = 0, long clientId = 0)
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

            //viewCustomerName.Hidden = true;

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
            tfBarcode.AddLeftRightPadding(UIConstants.TEXT_FIELD_HORIZONTAL_PADDING);
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
            //AddPickerToolbar(tfAdditionalService, tfAdditionalService.Placeholder, PickerDone);
            AddPickerToolbar(tfAirFreshner, tfAirFreshner.Placeholder, PickerDone);

            tfMake.InputView = pv;
            tfModel.InputView = pv;
            tfColor.InputView = pv;
            //tfBarcode.InputView = pv;
            tfUpcharge.InputView = pv;
            tfWashPkg.InputView = pv;
            tfDetailPkg.InputView = pv;
            //tfAdditionalService.InputView = pv;
            tfAdditionalService.Delegate = this;
            tfAdditionalService.AddTarget((sender, e) => { ShowMultiselectOptions(additionalServices.ToList()); }, UIControlEvent.EditingDidBegin);
            tfAirFreshner.InputView = pv;

            tfBarcode.WeakDelegate = this;

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

            //tfAdditionalService.ShouldChangeCharacters = (textField, range, replacementString) =>
            //{
            //    return false;
            //};

            tfAirFreshner.ShouldChangeCharacters = (textField, range, replacementString) =>
            {
                return false;
            };

            pv.DataSource = this;
            pv.Delegate = this;
        }

        [Export("textField:shouldChangeCharactersInRange:replacementString:")]
        public bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {
            if (textField == tfBarcode)
            {
                if (!IsNewBarcode)
                {
                    return false;
                }
            }
            else if (textField == tfAdditionalService)
            {
                return false;
            }

            return true;
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
                        if (ServiceType == ServiceType.Wash)
                            _ = LoadUpchargeByModel(ModelID);
                        else if (mainService == null)
                            return;
                        else
                            _ = UpdateUpchargeForDetailAndModel(ModelID);
                        break;
                    case ChoiceType.Color:
                        tfColor.Text = data[pos];
                        ColorID = Colors[pos].ID;
                        break;
                    case ChoiceType.Barcode:
                        //tfBarcode.Text = data[pos];
                        break;
                    case ChoiceType.Upcharge:
                        tfUpcharge.Text = data[pos];
                        if (pos == 0) // For None
                            return;
                        pos -= 1;
                        upcharge = upcharge ?? new JobItem();
                        upcharge.ServiceId = Upcharges[pos].ID;
                        upcharge.SeriveName = Upcharges[pos].Name;
                        upcharge.Price = Upcharges[pos].Price;
                        upcharge.Time = Upcharges[pos].Time;
                        break;
                    case ChoiceType.AdditionalService:
                        tfAdditionalService.Text = data[pos];
                        if (pos == 0) // For None
                            return;
                        pos -= 1;
                        //additional = additional ?? new JobItem();
                        //additional.ServiceId = AdditionalServices[pos].ID;
                        //additional.SeriveName = AdditionalServices[pos].Name;
                        //additional.Price = AdditionalServices[pos].Price;
                        //additional.Time = AdditionalServices[pos].Time;
                        break;
                    case ChoiceType.AirFreshner:
                        tfAirFreshner.Text = data[pos];
                        if (pos == 0) // For None
                            return;
                        pos -= 1;
                        airFreshner = airFreshner ?? new JobItem();
                        airFreshner.ServiceId = AirFreshners[pos].ID;
                        airFreshner.SeriveName = AirFreshners[pos].Name;
                        airFreshner.Price = AirFreshners[pos].Price;
                        airFreshner.Time = AirFreshners[pos].Time;
                        break;
                    case ChoiceType.Washpackage:
                        tfWashPkg.Text = data[pos];
                        mainService = mainService ?? new JobItem();
                        mainService.IsMainService = true;
                        mainService.ServiceId = WashPackages[pos].ID;
                        mainService.SeriveName = WashPackages[pos].Name;
                        mainService.Price = WashPackages[pos].Price;
                        mainService.Time = WashPackages[pos].Time;
                        break;
                    case ChoiceType.DetailPackage:
                        tfDetailPkg.Text = data[pos];
                        mainService = mainService ?? new JobItem();
                        mainService.IsMainService = true;
                        mainService.ServiceId = DetailPackages[pos].ID;
                        mainService.SeriveName = DetailPackages[pos].Name;
                        mainService.ServiceTypeID = DetailPackages[pos].TypeId;
                        mainService.Price = DetailPackages[pos].Price;
                        mainService.Time = DetailPackages[pos].Time;
                        if(ModelID != 0)
                            _ = UpdateUpchargeForDetailAndModel(ModelID);
                        break;
                }
        }

        void ChangeScreenType(ServiceType type)
        {
            switch (type)
            {
                case ServiceType.Wash:
                    //tfdetailHeight.Constant = 0;
                    //tfdetailTop.Constant = 0;
                    viewDetailPkg.Hidden = true;
                    break;
                case ServiceType.Detail:
                    //tfwashHeight.Constant = 0;
                    //tfwashTop.Constant = 0;
                    viewWashPkg.Hidden = true;
                    break;
            }
        }

        void NavigateToVerifyScreen()
        {
            Barcode = tfBarcode.Text;

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
            vc.UpchargeTypeName = tfUpcharge.Text;
            vc.AdditionalServices = additionalServcies?.ToArray();
            vc.AirFreshner = airFreshner;
            vc.CustName = CustName;
            vc.ClientID = ClientID;
            vc.VehicleID = VehicleID;
            vc.ServiceType = ServiceType;
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

        public void DidCompleted(MultiSelectPicker pickerView, List<int> selectedIndex)
        {
            UpdateSelectedAdditionalServices(selectedIndex);
        }

        void UpdateSelectedAdditionalServices(List<int> selectedIndexList)
        {
            if (selectedIndexList is null || selectedIndexList.Count == 0)
                return;

            AdditionalServicesSelectedPositions = selectedIndexList;
            //if (additionalServcies is null)
                additionalServcies = new();

            for (int i = 0; i < selectedIndexList.Count; i++)
            {
                int selectedPos = selectedIndexList[i];

                //if (selectedPos == 0)
                //{
                //    return;
                //}

                var additional = new JobItem();
                additional.ServiceId = AdditionalServices[selectedPos].ID;
                additional.SeriveName = AdditionalServices[selectedPos].Name;
                additional.Price = AdditionalServices[selectedPos].Price;
                additional.Time = AdditionalServices[selectedPos].Time;
                additionalServcies.Add(additional);
            }

            var names = additionalServcies.Select(x => x.SeriveName);
            tfAdditionalService.Text = String.Join(",", names);
        }

        public void DidCancel(MultiSelectPicker pickerView)
        {
            
        }
    }

    //enum fds
    //{
    //    [EnumMember(Value = "Air Fresheners")]
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
