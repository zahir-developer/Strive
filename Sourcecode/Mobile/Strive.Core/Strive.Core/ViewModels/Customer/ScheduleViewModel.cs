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

        public PastClientServices pastClientServices { get; set; }
        public ScheduleModel pastServiceHistory { get; set; }
        public ScheduleModel pastServiceHistory1 { get; set; }

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
            if (scheduleVehicleList == null || scheduleVehicleList.Status.Count == 0)
            {
                _userDialog.Alert("No associated vehicles were found.");
            }
            else
            {
                CustomerVehiclesInformation.vehiclesList = scheduleVehicleList;
            }
            _userDialog.HideLoading();
        }

        public async Task<PastClientServices> GetPastDetailsServices()
        {
            var result = await AdminService.GetPastClientServices(CustomerInfo.ClientID);
            if (result == null)
            {
                return result = null;
            }
            else
            {
                pastClientServices = new PastClientServices();
                pastClientServices.PastClientDetails = new List<PastClientDetails>();
                pastClientServices = result;
                return pastClientServices;
            }
        }


        public async Task GetPastServiceDetails()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetSchedulePastService(0);
            if(result == null)
            {
                _userDialog.Toast("No Schedules have been found !");
            }
            else
            {
                pastServiceHistory = result;
            }
            _userDialog.HideLoading();
        }

        #endregion Commands

    }
}
