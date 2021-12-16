using System;
using System.Threading.Tasks;
using Strive.Core.Models.Customer;

namespace Strive.Core.ViewModels.Customer
{
    public class DealsPageViewModel:BaseViewModel
    {

        public int  dealId;
        public int clientID;
        public string Date;

        public static ClientDeals clientDeal;
        public DealsPageViewModel()
        {
        }
        
        public  async Task AddClientDeals()
        {
            var clientDeal = new AddClientDeal()
            {
                dealId = dealId,
                clientId = clientID,
                date = Date

            };

            var data = await AdminService.AddClientDeal(clientDeal);
            Console.WriteLine(data);
            if (data.Status)
            {
                if (dealId != 0)
                {
                    var result2 = await AdminService.GetClientDeal(CustomerInfo.ClientID, DateTime.Today.ToString("yyyy-MM-dd"),dealId);
                    if (result2.ClientDeal.ClientDealDetail != null)
                    {
                        DealsPageViewModel.clientDeal = result2;
                        DealsViewModel.CouponName = result2.ClientDeal.ClientDealDetail[0].DealName;
                    }
                    else
                    {
                        DealsPageViewModel.clientDeal = null;
                    }
                }
                //else
                //{
                //    var result = await AdminService.GetClientDeal(CustomerInfo.ClientID, DateTime.Today.ToString("yyyy-MM-dd"), DealsViewModel.SelectedDealId);
                //    if (result != null)
                //    {
                //        DealsPageViewModel.clientDeal = result;
 
                //        DealsViewModel.CouponName = result.ClientDeal.ClientDealDetail[0].DealName;
                //    }
                //}
            }
            return ;
        }
    }
}
