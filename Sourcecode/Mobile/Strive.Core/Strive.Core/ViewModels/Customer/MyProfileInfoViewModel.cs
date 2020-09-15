using Strive.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class MyProfileInfoViewModel : BaseViewModel
    {
        public CustomerPersonalInfo customerInfo { get; set; }

        #region Properties
        public int ClientId { get; set; } = 59;
        #endregion Properties

        #region Commands
        public async Task<CustomerPersonalInfo> getClientById()
        {
            customerInfo = await AdminService.GetClientById(ClientId);
            if (customerInfo == null)
            {
               
            }
            return customerInfo;
        }
        #endregion Commands
    }
}
