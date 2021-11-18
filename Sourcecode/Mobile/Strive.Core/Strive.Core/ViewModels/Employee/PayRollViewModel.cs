using System;
using System.Linq;
using System.Threading.Tasks;
using Strive.Core.Models.Employee.PayRoll;

namespace Strive.Core.ViewModels.Employee
{
    public class PayRollViewModel: BaseViewModel
    {
        public PayRollRateViewModel PayRoll { get; set; }
        public int Location { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public int employeeid { get; set; }
        public PayRollViewModel()
        {

        }

        public async Task GetPayRollProcess()
        {
            _userDialog.ShowLoading();
            var result = await AdminService.GetPayRoll(Fromdate,Todate,employeeid,Location);
            PayRoll = result.Result.PayRollRateViewModel.First(x => x.EmployeeId == employeeid);
            _userDialog.HideLoading();
            
        }

    }
}
