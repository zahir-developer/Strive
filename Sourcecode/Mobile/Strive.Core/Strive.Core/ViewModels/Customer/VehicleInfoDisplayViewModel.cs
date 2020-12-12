﻿using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleInfoDisplayViewModel : BaseViewModel
    {
        #region Properties

        public VehicleList selectedVehicleInfo { get; set; }
        public ClientVehicleRootView clientVehicleRoot { get; set; }

        public CustomerCompleteDetails clientVehicleDetail { get; set; }

        #endregion Properties


        #region Commands 

        public async Task GetSelectedVehicleInfo()
        {
            _userDialog.ShowLoading();

            selectedVehicleInfo = new VehicleList();
            selectedVehicleInfo.Status = new List<VehicleDetail>();
            foreach (var data in CustomerVehiclesInformation.vehiclesList.Status)
            {
                if(data.VehicleId == CustomerVehiclesInformation.selectedVehicleInfo)
                {
                    selectedVehicleInfo.Status.Add(data);
                }
            }
            CustomerVehiclesInformation.completeVehicleDetails = new ClientVehicleRootView();
            CustomerVehiclesInformation.completeVehicleDetails.VehicleMembershipDetails = new VehicleMembershipDetailsView();

            CustomerVehiclesInformation.completeVehicleDetails.
                VehicleMembershipDetails.ClientVehicle = new ClientVehicleView();

            CustomerVehiclesInformation.completeVehicleDetails.
                VehicleMembershipDetails.ClientVehicleMembership = new ClientVehicleMembershipView();

            CustomerVehiclesInformation.completeVehicleDetails.
                VehicleMembershipDetails.ClientVehicleMembershipService = new List<ClientVehicleMembershipServiceView>();

            CustomerVehiclesInformation.completeVehicleDetails = await AdminService.GetVehicleMembership(CustomerVehiclesInformation.selectedVehicleInfo);


            _userDialog.HideLoading();
        }

        public async Task GetCompleteVehicleDetails()
        {
            _userDialog.ShowLoading();

            clientVehicleDetail = new CustomerCompleteDetails();

            clientVehicleDetail = await AdminService.GetVehicleCompleteDetails(CustomerVehiclesInformation.selectedVehicleInfo);

            _userDialog.HideLoading();
        }

        public async Task<bool> MembershipExists()
        {
            var confirm = await _userDialog.ConfirmAsync("Do you want to edit your membership ?");
            var confirmed = false;
            if(confirm)
            {
                return confirmed = true;
            }
            else
            {
                return confirmed = false;
            }
        }

        public async void NavToVehicleMembership()
        {
            await _navigationService.Navigate<VehicleMembershipViewModel>();
        }
        #endregion Commands


    }
}
