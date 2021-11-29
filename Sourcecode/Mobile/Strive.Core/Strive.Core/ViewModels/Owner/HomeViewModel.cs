using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Owner;
using Strive.Core.Resources;
using Strive.Core.Services.Interfaces;

namespace Strive.Core.ViewModels.Owner
{
    public class HomeViewModel : BaseViewModel
    {
        public GetDashboardStatisticsForLocationId statisticsData { get; set; }
        public ICarwashLocationService carWashLocationService = Mvx.IoCProvider.Resolve<ICarwashLocationService>();
        public Locations Locations;
        public ScheduleModel dbSchedule { get; set; }

        public async Task getStatistics(int locationId)
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);

            var result = await AdminService.getDashboardServices(new StatisticRequest
            {
                locationId = locationId,
                fromDate = (System.DateTime.Now).ToString("yyy-MM-dd"),
                toDate = (System.DateTime.Now).ToString("yyy-MM-dd"),
            });
               
            if (result == null)
            {
                _userDialog.Toast("No relatable data");
            }
            else
            {
                statisticsData = new GetDashboardStatisticsForLocationId();
                statisticsData = result.GetDashboardStatisticsForLocationId[0];
            }
            _userDialog.HideLoading();
        }

        public async Task getDashboardSchedule(int locationId)
        {
            _userDialog.ShowLoading(Strings.Loading, MaskType.Gradient);

            var jobDate = (System.DateTime.Now).ToString("yyy-MM-dd");
            var result = await AdminService.getDashboardSchedule(jobDate,locationId);

            if(result == null)
            {
                _userDialog.Toast("No relatable data");

            }
            else
            {
                dbSchedule = new ScheduleModel();
                dbSchedule = result;
            }
            _userDialog.HideLoading();
        }
        

        #region Commands
        public async Task<Locations> GetAllLocationsCommand()
        {
            var washLocations = await carWashLocationService.GetAllCarWashLocations();

            if (washLocations == null)
            {
                Locations = new Locations();

                return Locations;
            }
            else
            {
                Locations = new Locations();
                Locations.Location = new List<Location>();
                foreach (var locations in washLocations.Location)
                {
                    if (locations.Latitude != null && locations.Longitude != null)
                    {
                        Locations.Location.Add(locations);
                    }
                }
                return Locations;
            }
        }
        #endregion Commands 
    }
}
