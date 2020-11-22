using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class PersonalInfoViewModel : BaseViewModel
    {

        #region Properties

        public CustomerPersonalInfo customerInfo { get; set; }

        #endregion Properties

        #region Commands

        public async Task GetClientById()
        {
            _userDialog.ShowLoading(Strings.Loading, Acr.UserDialogs.MaskType.Gradient);
            customerInfo = new CustomerPersonalInfo();
            customerInfo.Status = new List<Status>();
            customerInfo = await AdminService.GetClientById(CustomerInfo.ClientID);
            if (customerInfo == null || customerInfo.Status.Count == 0)
            {
                _userDialog.Toast("No user information available");
            }
            _userDialog.HideLoading();
            return;
        }

        #endregion Commands



    }
}
