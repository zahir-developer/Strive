﻿using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleUpchargeViewModel : BaseViewModel
    {
        public VehicleUpchargeViewModel()
        {
        }

        #region Properties

        public SelectedServiceList selectedMembership { get; set; }
        

        #endregion Properties

        #region Commands
        public async Task getServiceList(int membershipId)
        {
            _userDialog.ShowLoading(Strings.Loading);
            MembershipDetails.customerVehicleDetails.clientVehicleMembershipModel = new ClientVehicleMembershipModel();

            selectedMembership = new SelectedServiceList();
            var data = await AdminService.GetSelectedMembershipServices(membershipId);
            if (data == null)
            {
                _userDialog.HideLoading();
                _userDialog.Alert("Error !");
                return;
            }
            selectedMembership.MembershipDetail = new List<ServiceDetail>();
            foreach (var result in data.MembershipDetail)
            {
                if (!String.Equals(result.ServiceType.ToString(), "Washes"))
                {
                    selectedMembership.MembershipDetail.Add(result);
                }
            }
            _userDialog.HideLoading();
        }

        public async Task getAllServiceList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var completeList = await AdminService.GetVehicleServices();
            if (completeList == null)
            {
                _userDialog.HideLoading();
            }
            MembershipDetails.filteredList = new ServiceList();
            MembershipDetails.filteredList.ServicesWithPrice = new List<ServiceDetail>();
            foreach (var upcharges in selectedMembership.MembershipDetail)
            {
                MembershipDetails.filteredList.
                    ServicesWithPrice.
                    Add(
                     completeList.
                     ServicesWithPrice.
                     Find(c => c.ServiceId == upcharges.ServiceId)
                    );

            }
            _userDialog.HideLoading();

        }

        public bool VehicleUpchargeCheck()
        {
            if(MembershipDetails.selectedUpCharge == 0)
            {
                _userDialog.Alert("Please select an upcharge");
                return false;
            }
            return true;
        }

        public async Task NextCommand()
        {
            if(VehicleUpchargeCheck())
            {
               await _navigationService.Navigate<MembershipSignatureViewModel>();
            }
        }
        public async Task BackCommand()
        {
            await _navigationService.Navigate<VehicleMembershipViewModel>();
        }

        #endregion Commands

    }
}
