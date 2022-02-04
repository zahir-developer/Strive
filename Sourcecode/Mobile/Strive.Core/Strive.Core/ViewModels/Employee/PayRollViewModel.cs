using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Strive.Core.Models.Employee.PayRoll;
//using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.Employee;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.Employee
{
    public class PayRollViewModel: BaseViewModel
    {
        public PayRollRateViewModel PayRoll { get; set; }
        public int Location { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int employeeid { get; set; }
        public List<EmployeeLocation> EmployeeLocations { get; set; }
        public PayRollViewModel()
        {

        }
        public string ItemLocation
        {
            get
            {
                return _selectedLocation;
            }
            set { SetProperty(ref _selectedLocation, value); }
        }


        public string _selectedLocation;

        public void locationCommand(EmployeeLocation location )
        {
            ItemLocation = location.LocationName;
            Location = location.LocationId;
           // EmployeeData.selectedLocationId = location.LocationId;
            //EmployeeData.selectedLocationId = EmployeeData.EmployeeDetails.EmployeeLocations[0].LocationId;
        }

        public async Task GetPayRollProcess()
        {
            _userDialog.ShowLoading();
            var result = await AdminService.GetPayRoll(Fromdate,Todate,employeeid,Location);

            if (result.Result.PayRollRateViewModel != null)
            {
                if (result.Result.PayRollRateViewModel.Any(x => x.EmployeeId == employeeid))
                    PayRoll = result.Result.PayRollRateViewModel.First(x => x.EmployeeId == employeeid);
                else
                {
                    PayRoll = null;
                }

            }            
            else
            {
                PayRoll = null;
                _userDialog.Alert("No payroll information found for this location / date");
            }
                

            _userDialog.HideLoading();

            
        }

    }
}
