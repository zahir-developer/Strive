using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer.Schedule
{
    public class ScheduleServicesViewModel : BaseViewModel
    {
        private List<string> upcharges = new List<string>();
        #region Properties
        
        public AvailableServicesModel scheduleServices { get; set; }
        public int uniqueServiceID { get; set; } = 0;

        #endregion Properties

        #region Commands

        public async Task GetScheduledServices()
        {
            int locationid = CustomerScheduleInformation.ScheduleLocationCode;
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);
            var result = await AdminService.GetScheduleServices(locationid);
            
            if(result == null)
            {
                _userDialog.Alert("No Services available");
            }
            else                
            {
                scheduleServices = new AvailableServicesModel();
                scheduleServices.AllServiceDetail = new List<AllServiceDetail>();
                
                foreach(var data in result.AllServiceDetail)
                {
                    if(uniqueServiceID != data.ServiceId && string.Equals(data.ServiceTypeName, "Detail Package"))
                    {
                        scheduleServices.AllServiceDetail.Add(data);
                    }
                    uniqueServiceID = data.ServiceId;
                    
                }
            }
            _userDialog.HideLoading();
        }
        public async void NavToSelect_Appoitment()
        {

            var upchargeRequest = new modelUpcharge()
            {
                upchargeServiceType = CustomerScheduleInformation.IsCeramic ? 11778 : 11780,
                modelId = CustomerScheduleInformation.ScheduleSelectedVehicle.VehicleModelId
            };
                var modelUpcharge = new modelUpchargeResponse();
                modelUpcharge = await AdminService.GetModelUpcharge(upchargeRequest);
                MembershipDetails.modelUpcharge = modelUpcharge;


            if (checkSelectedService())
            {
                await _navigationService.Navigate<ScheduleAppointmentDateViewModel>();
            }
        }
        public async void NavToSelect_Location()
        {
            await _navigationService.Navigate<ScheduleLocationsViewModel>();  
        }

        public async void NavToSchedule()
        {
            await _navigationService.Navigate<ScheduleViewModel>();
        }

        public bool checkSelectedService()
        {
            var selected = false;
            if(CustomerScheduleInformation.ScheduleServiceID != -1)
            {
                selected = true;  
            }
            else
            {
                _userDialog.Alert("Please select a service to proceed.");
                selected = false;
            }
            return selected;
        }
        #endregion Commands

    }
}
