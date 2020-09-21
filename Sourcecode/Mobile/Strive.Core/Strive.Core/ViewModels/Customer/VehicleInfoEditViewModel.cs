using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
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
            if(VehicleDetailsCheck())
            {
                await _navigationService.Navigate<VehicleMembershipViewModel>();
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


        #endregion Commands

    }
}
