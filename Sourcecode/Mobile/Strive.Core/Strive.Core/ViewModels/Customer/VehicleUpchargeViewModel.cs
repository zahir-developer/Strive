using Strive.Core.Models.Customer;
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
        public ServiceList upchargeFullList { get; set; } = new ServiceList();
        
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
                if (!String.Equals(result.ServiceTypeId.ToString(), "Washes"))
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
            if (completeList != null)
            {
                upchargeFullList.ServicesWithPrice = new List<ServiceDetail>();
                foreach(var item in completeList.ServicesWithPrice)
                {
                    if( item.ServiceTypeName == "Wash-Upcharge")
                    {
                        upchargeFullList.ServicesWithPrice.Add(item);
                    }
                }
                _userDialog.HideLoading();
            }
            MembershipDetails.filteredList = new ServiceList();
            MembershipDetails.filteredList.ServicesWithPrice = new List<ServiceDetail>();
            foreach (var upcharges in selectedMembership.MembershipDetail)
            {
                if (upcharges.Upcharges != null)
                {
                    MembershipDetails.filteredList.
                    ServicesWithPrice.
                    Add(
                     completeList.
                     ServicesWithPrice.
                     Find(c => c.ServiceId == upcharges.ServiceId)
                    );
                }               
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

        public async void NavToAdditionalServices()
        {
            await _navigationService.Navigate<VehicleAdditionalServiceViewModel>();
        }
        #endregion Commands

    }
}
