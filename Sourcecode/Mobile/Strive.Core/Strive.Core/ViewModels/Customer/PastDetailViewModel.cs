using Strive.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class PastDetailViewModel : BaseViewModel
    {
        #region Properties
        
        
        #endregion Properties

        #region Commands

        public async Task<PastClientServices> GetPastDetailsServices()
        {
            var result = await AdminService.GetPastClientServices(CustomerInfo.ClientID);
            if(result == null)
            {
                return result = null;
            }
            else
            {
                return result;
            }


        }

        #endregion Commands


    }
}