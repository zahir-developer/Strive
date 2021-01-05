using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.Customer
{
    public class ScheduleViewModel : BaseViewModel
    {
        #region Properties

        public VehicleList scheduleVehicleList { get; set; }

        #endregion Properties

        #region Commands

        public async Task GetScheduleVehicleList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            scheduleVehicleList = new VehicleList();
            scheduleVehicleList.Status = new List<VehicleDetail>();
            CustomerVehiclesInformation.vehiclesList = new VehicleList();
            CustomerVehiclesInformation.vehiclesList.Status = new List<VehicleDetail>();
            scheduleVehicleList = await AdminService.GetClientVehicle(CustomerInfo.ClientID);
            CustomerVehiclesInformation.vehiclesList = scheduleVehicleList;
            if (scheduleVehicleList == null || scheduleVehicleList.Status.Count == 0)
            {
                _userDialog.Alert("No associated vehicles were found.");
            }
            _userDialog.HideLoading();
        }


        #endregion Commands

    }
}
