using Strive.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer
{
    public class MyProfileInfoViewModel : BaseViewModel
    {
        public CustomerPersonalInfo customerInfo { get; set; }

        #region Properties
        public int ClientId { get; set; } = 59;
        #endregion Properties

        #region Commands
        public async void getClientById()
        {
           
            customerInfo = await AdminService.GetClientById(ClientId);
            if (customerInfo == null)
            {
               
            }
        }
        #endregion Commands
    }
}
