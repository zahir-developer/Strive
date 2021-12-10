using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils.TimInventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class VehicleAdditionalServiceViewModel : BaseViewModel
    {
        public ObservableCollection<AllServiceDetail> serviceList { get; set; } = new ObservableCollection<AllServiceDetail>();
        public List<ServiceDetail> DefaultServices { get; set; } = new List<ServiceDetail>();
        public VehicleAdditionalServiceViewModel()
        {           
            
        }

        #region Properties


        #endregion Properties

        #region Commands

        public async Task AddUpchargesToServiceList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.GetScheduleServices(1);

            if (result != null)
            {
                foreach (var item in result.AllServiceDetail)
                {
                    if (item.ServiceTypeName == "Additional Services")
                    {
                        serviceList.Add(item);
                    }
                }
            }
        }
        public async void NavtoTermsCondition()
        {
            await _navigationService.Navigate<TermsAndConditionsViewModel>();
        }
        
        #endregion Commands
    }
}
