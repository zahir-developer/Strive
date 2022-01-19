using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleInfoEditViewModel : BaseViewModel
    {
        #region Properties

        public VehicleCodes vehicleCodes { get; set; }
        public Dictionary<int, string> UpCharges = new Dictionary<int, string>();
        public Dictionary<int, string> manufacturerName = new Dictionary<int, string>();
        public Dictionary<int, string> modelName = new Dictionary<int, string>();
        public Dictionary<int, string> colorName = new Dictionary<int, string>();
        public CustomerUpdateVehicle updateVehicle { get; set; }
        public VehicleList vehicleDetails { get; set; }        
        public clientVehicle clientVehicle { get; set; }
        public CustomerVehicles clientVehicles { get; set; }
        public VehicleList vehicleLists { get; set; }
        public MakeList makeList { get; set; }
        public ModelList modelList { get; set; }

        public List<int> VehicleNumber = new List<int>();
        #endregion Properties


        #region Commands

        public async Task getVehicleDetails()
        {
            _userDialog.ShowLoading(Strings.Loading);

            vehicleCodes = await AdminService.GetVehicleCodes();
            foreach (var data  in vehicleCodes.VehicleDetails)
            {
                if (data.Category == "VehicleManufacturer")
                {
                    manufacturerName.Add(data.CodeId, data.CodeValue);
                }
                else if (data.Category == "VehicleModel")
                {
                    modelName.Add(data.CodeId, data.CodeValue);
                }
                else if (data.Category == "VehicleColor")
                {
                    colorName.Add(data.CodeId, data.CodeValue);
                }
                else
                {
                    break;
                }
            }

            if (vehicleCodes == null)
            {
                _userDialog.HideLoading();
            }
            _userDialog.HideLoading();
        }
        public async Task GetMakeList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            makeList = await AdminService.GetMakeList();
            _userDialog.HideLoading();
        }
        public async Task GetModelList(string selectedMake)
        {
            _userDialog.ShowLoading(Strings.Loading);
                        
            foreach(var item in makeList.Make)
            {
                string selectedMake1 = selectedMake.Replace(" ", "");
                if (selectedMake1 == item.MakeValue)
                {
                    var result = await AdminService.GetModelList(item.MakeId);
                    if(result != null)
                    {
                        modelList = result;
                    }
                }
            }
            _userDialog.HideLoading();
        }
        public async void SelectMembership()
        {
            if(MembershipDetails.clientVehicleID != 0)
            {
                await _navigationService.Navigate<VehicleMembershipViewModel>();
            }
            else
            {
                _userDialog.Alert("Please save the vehicle specifications");
            }
        }
        public async Task GetCustomerVehicleList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            vehicleLists = new VehicleList();
            vehicleLists.Status = new List<VehicleDetail>();
            CustomerVehiclesInformation.vehiclesList = new VehicleList();
            CustomerVehiclesInformation.vehiclesList.Status = new List<VehicleDetail>();
            vehicleLists = await AdminService.GetClientVehicle(CustomerInfo.ClientID);
            MembershipDetails.clientVehicleID = vehicleLists.Status.LastOrDefault().VehicleId;
            CustomerVehiclesInformation.vehiclesList = vehicleLists;
            CustomerVehiclesInformation.selectedVehicleInfo = CustomerVehiclesInformation.vehiclesList.Status.LastOrDefault().VehicleId;
            if (vehicleLists == null || vehicleLists.Status.Count == 0)
            {
                _userDialog.Alert("No associated vehicles were found.");
            }

            _userDialog.HideLoading();
        }
        public async void NavigateBack()
        {
            MembershipDetails.clearMembershipData();
            await _navigationService.Close(this);
        }
        public async void NavigateProfile()
        {
            await _navigationService.Navigate<MyProfileInfoViewModel>();
        }
        public bool VehicleDetailsCheck()
        {
           
            if (MembershipDetails.vehicleMakeName == null)
            {
                _userDialog.Alert("Select the vehicle manufacturer");
                return false;
            }
            else if (MembershipDetails.colorName == null)
            {
                _userDialog.Alert("Select the vehicle color");
                return false;
            }
            else if(MembershipDetails.modelName == null)
            {
                _userDialog.Alert("Select the vehicle model");
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckSaveVehicle()
        {
            var valid = false;
            if ((MembershipDetails.previousSelectedColor == MembershipDetails.selectedColor) && (MembershipDetails.previousSelectedMake == MembershipDetails.selectedMake) && (MembershipDetails.previousSelectedModel == MembershipDetails.selectedModel))
            {
                _userDialog.Alert("You have already created this vehicle");
                valid = false;
            }
            else
            {
                MembershipDetails.previousSelectedModel = MembershipDetails.selectedModel;
                MembershipDetails.previousSelectedColor = MembershipDetails.selectedColor;
                MembershipDetails.previousSelectedMake = MembershipDetails.selectedMake;
                valid = true;
            }
            return valid;
        }
        public async Task SaveVehicle()
        {  
            if (VehicleDetailsCheck())
            {
                if(CheckSaveVehicle())
                {
                    _userDialog.ShowLoading(Strings.Loading);
                    clientVehicles = new CustomerVehicles();
                    clientVehicles.clientVehicle = new clientVehicle();
                    clientVehicles.vehicleImage = new List<vehicleImage>();
                    clientVehicles.clientVehicle = new clientVehicle();
                   
                    clientVehicles.clientVehicle.clientId = CustomerInfo.ClientID;
                    clientVehicles.clientVehicle.locationId = 1;
                    clientVehicles.clientVehicle.vehicleModelNo = 0;
                    clientVehicles.clientVehicle.vehicleMfr = MembershipDetails.vehicleMakeNumber;
                    clientVehicles.clientVehicle.vehicleModel = MembershipDetails.modelNumber;
                    clientVehicles.clientVehicle.vehicleColor = MembershipDetails.colorNumber;
                    foreach (var element in CustomerVehiclesInformation.vehiclesList.Status)
                    {
                        if (element.VehicleNumber != null)
                        {
                            if (int.TryParse(element.VehicleNumber, out int val))
                                VehicleNumber.Add(val);

                        }
                    }                    
                    clientVehicles.clientVehicle.vehicleNumber = (VehicleNumber.Count() > 0) ? (VehicleNumber.Max()+1).ToString() : "1" ;
                    clientVehicles.clientVehicle.createdDate = DateUtils.ConvertDateTimeWithZ();
                    clientVehicles.clientVehicle.updatedDate = DateUtils.ConvertDateTimeWithZ();
                    clientVehicles.clientVehicle.isActive = true;
                    clientVehicles.clientVehicle.isDeleted = false;
                    //var vehicleImage = new vehicleImage();
                    //vehicleImage.vehicleImageId = 0;
                    //vehicleImage.vehicleId = 0;
                    //vehicleImage.imageName = "";
                    //vehicleImage.originalImageName = "";
                    //vehicleImage.thumbnailFileName = "";
                    //vehicleImage.filePath = "";
                    //vehicleImage.base64 = "";
                    //vehicleImage.isActive = true;
                    //vehicleImage.isDeleted = false;
                    //vehicleImage.createdDate = DateUtils.ConvertDateTimeWithZ();
                    //vehicleImage.createdBy = 0;
                    //vehicleImage.updatedDate = DateUtils.ConvertDateTimeWithZ();
                    //vehicleImage.updatedBy = 0;

                    //clientVehicles.vehicleImage.Add(vehicleImage);
                    MembershipDetails.vehicleNumber = clientVehicles.clientVehicle.vehicleNumber;
                    var data = await AdminService.AddCustomerVehicle(clientVehicles);
                    if (data == null)
                    {
                        _userDialog.Alert("Information not added,try again");
                        return;
                    }
                    await GetCustomerVehicleList();
                    _userDialog.HideLoading();
                    _userDialog.Toast("Information has been entered successfully");
                }
              
            }
        }

        public void ShowAlert()
        {
            _userDialog.Alert("Please save the vehicle specifications");
        }
        public async void NavToVehicleMembership()
        {
            await _navigationService.Navigate<VehicleMembershipViewModel>();
        }

        #endregion Commands

    }
}
