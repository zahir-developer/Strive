﻿using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
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

        public async void SelectMembership()
        {
            if(MembershipDetails.clientVehicleID != 0)
            {
                await _navigationService.Navigate<VehicleMembershipViewModel>();
            }
            else
            {
                _userDialog.Alert("Please select the vehicle specifications");
            }
        }

        public async void NavigateBack()
        {
            MembershipDetails.clearMembershipData();
            await _navigationService.Close(this);
        }

        public bool VehicleDetailsCheck()
        {
           
            if (MembershipDetails.selectedMake == 0)
            {
                _userDialog.Alert("Select the vehicle manufacturer");
                return false;
            }
            else if (MembershipDetails.selectedColor == 0)
            {
                _userDialog.Alert("Select the vehicle color");
                return false;
            }
            else if(MembershipDetails.selectedModel == 0)
            {
                _userDialog.Alert("Select the vehicle model");
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task SaveVehicle()
        {
            _userDialog.ShowLoading(Strings.Loading);
            clientVehicle = new clientVehicle();
            updateVehicle = new CustomerUpdateVehicle();
            updateVehicle.client = null;
            if(VehicleDetailsCheck())
            {
                updateVehicle.clientVehicle = new List<clientVehicle>();
                clientVehicle.clientId = CustomerInfo.ClientID;
                clientVehicle.locationId = 1;
                clientVehicle.vehicleMfr = MembershipDetails.vehicleMakeNumber;
                clientVehicle.vehicleModel = MembershipDetails.modelNumber;
                clientVehicle.vehicleColor = MembershipDetails.colorNumber;
                clientVehicle.createdDate = DateUtils.ConvertDateTimeWithZ();
                clientVehicle.updatedDate = DateUtils.ConvertDateTimeWithZ();
                updateVehicle.clientVehicle.Add(clientVehicle);
                var data = await AdminService.UpdateCustomerVehicle(updateVehicle);
                if (data == null)
                {
                    _userDialog.Alert("Information not added,try again");
                    return;
                }
                await GetRegisteredVehicleID();
                _userDialog.HideLoading();
                _userDialog.Toast("Information has been entered successfully");
            }
            else
            {
                _userDialog.HideLoading();
                _userDialog.Toast("Information save unsuccessful");
            }
        }

        public async Task GetRegisteredVehicleID()
        {
            vehicleDetails = new VehicleList();
            vehicleDetails.Status = new List<VehicleDetail>();
            vehicleDetails = await AdminService.GetClientVehicle(CustomerInfo.ClientID);
            if(vehicleDetails == null || vehicleDetails.Status.Count == 0 )
            {
                _userDialog.Alert("No available vehicles were found");
                return;
            }
            MembershipDetails.clientVehicleID = vehicleDetails.Status.LastOrDefault().VehicleId;
        }

        public void ShowAlert()
        {
            _userDialog.Alert("Please select the vehicle specifications");
        }


        #endregion Commands

    }
}
