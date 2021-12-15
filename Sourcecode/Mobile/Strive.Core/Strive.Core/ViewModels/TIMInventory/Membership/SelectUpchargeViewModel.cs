using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Strive.Core.Utils;
using MvvmCross.Plugin.Messenger;
using Strive.Core.Resources;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Models.Customer;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class SelectUpchargeViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;

        public ObservableCollection<string> UpchargesList { get; set; } = new ObservableCollection<string>();
        public AvailableServicesModel serviceDetail = new AvailableServicesModel();

        public SelectUpchargeViewModel()
        {
            GetUpchargeList();
           
            RaiseAllPropertiesChanged();
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }

        public async void GetUpchargeList()
        {
            _userDialog.ShowLoading(Strings.Loading);
            serviceDetail = await AdminService.GetScheduleServices(EmployeeData.selectedLocationId);
            
            if(serviceDetail != null)
            {
                foreach(var item in serviceDetail.AllServiceDetail)
                {
                    if(item.ServiceTypeName == "Wash-Upcharge")
                    {
                        UpchargesList.Add(item.Upcharges);
                    }                    
                }
            }
        }
        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            if (message.Valuea == 5)
            {
                await _navigationService.Close(this);
                //_messageToken.Dispose();
            }
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }

        public void NextCommand()
        {
            if (MembershipDetails.modelUpcharge != null)
            {
                MembershipData.SelectedMembership.Price += MembershipDetails.modelUpcharge.upcharge[0].Price;
            }
            _navigationService.Navigate<ExtraServiceViewModel>();
        }
    }
}
