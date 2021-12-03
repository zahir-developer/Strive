using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.Customer
{
    public class DealsViewModel : BaseViewModel
    {
        public ObservableCollection<GetAllDeal> Deals { get; set; } = new ObservableCollection<GetAllDeal>();
        public int SelectedDealId { get; set; }
        public async Task GetAllDealsCommand()
        {
            _userDialog.ShowLoading("Loading");
            if(Deals.Count == 0)
            {
                var result = await AdminService.GetAllDeals();
                foreach (var item in result.GetAllDeals)
                {
                    Deals.Add(item);
                }
            }
            _userDialog.HideLoading();
        }

        public async Task NavigateToDetailCommand(string item)
        {
            await _navigationService.Navigate<DealsDetailViewModel>();
        }
        public async Task NavigateToDealsPageCommand()
        {
            var result = await AdminService.GetClientDeal(CustomerInfo.ClientID,DateTime.Today.ToString("yyyy-MM-dd"),SelectedDealId);
            if( result!= null)
            {
                await _navigationService.Navigate<DealsPageViewModel>();
            }
           
        }

        public void LogoutCommand()
        {
            var confirmconfig = new ConfirmConfig
            {
                Title = Strings.LogoutTitle,
                Message = Strings.LogoutMessage,
                CancelText = Strings.LogoutCancelButton,
                OkText = Strings.LogoutSuccessButton,
                OnAction = success =>
                {
                    if (success)
                    {
                        CustomerInfo.Clear();
                        _navigationService.Close(this);
                        _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
                    }
                }

            };
            _userDialog.Confirm(confirmconfig);
        }
    } 
}
