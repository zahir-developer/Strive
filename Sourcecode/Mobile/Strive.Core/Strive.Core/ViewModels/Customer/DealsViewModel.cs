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
        public static int SelectedDealId { get; set; }
        public static string startdate { get; set; }
        public static string enddate { get; set; }
        public static int TimePeriod { get; set; }
        public static string CouponName;
        public async Task GetAllDealsCommand()
        {
            _userDialog.ShowLoading("Loading");
            if(Deals.Count == 0)
            {
                var result = await AdminService.GetAllDeals();
                if (result != null)
                {
                    foreach (var item in result.GetAllDeals)
                    {
                        Deals.Add(item);
                    }
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
            //foreach (var element in Deals)
            //{
            //    if (element.DealId == SelectedDealId)
            //    {
            //        CouponName = element.DealName;
            //        //startdate = element.StartDate;
            //        enddate = element.EndDate;
            //        TimePeriod = element.TimePeriod;
            //    }

            //}
            //_userDialog.ShowLoading();
            //var result2 = await AdminService.GetClientDeal(CustomerInfo.ClientID, DateTime.Today.ToString("yyyy-MM-dd"), SelectedDealId);
            ////_userDialog.HideLoading();
            //if (result2.ClientDeal.ClientDealDetail != null)
            //{
            //    DealsPageViewModel.clientDeal = result2;
            //    CouponName = result2.ClientDeal.ClientDealDetail[0].DealName;
            //    enddate = result2.ClientDeal.ClientDealDetail[0].EndDate;
            //}
            //else
            //{
            //    DealsViewModel.enddate = null;
            //}
            await GetClientDeals(); 
            await _navigationService.Navigate<DealsPageViewModel>();

        }
        public async Task GetClientDeals()
        {
            foreach (var element in Deals)
            {
                if (element.DealId == SelectedDealId)
                {
                    CouponName = element.DealName;
                    //startdate = element.StartDate;
                    enddate = element.EndDate;
                    TimePeriod = element.TimePeriod;
                }

            }
            _userDialog.ShowLoading();
            var result2 = await AdminService.GetClientDeal(CustomerInfo.ClientID, DateTime.Today.ToString("yyyy-MM-dd"), SelectedDealId);
            //_userDialog.HideLoading();
            if (result2.ClientDeal.ClientDealDetail != null)
            {
                DealsPageViewModel.clientDeal = result2;
                CouponName = result2.ClientDeal.ClientDealDetail[0].DealName;
                enddate = result2.ClientDeal.ClientDealDetail[0].EndDate;
            }
            else
            {
                DealsPageViewModel.clientDeal = null;
                DealsViewModel.enddate = null;
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
