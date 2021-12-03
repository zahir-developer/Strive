using System;
using Strive.Core.Models.Customer;

namespace Strive.Core.ViewModels.Customer
{
    public class DealsPageViewModel:BaseViewModel
    {
        public DealsPageViewModel()
        {
        }
        
        public async void AddClientDeals()
        {
            var clientDeal = new AddClientDeal()
            {
                dealId = 2,
                clientId = 57398,
                date = "2021-12-03"

            };

            var data = await AdminService.AddClientDeal(clientDeal);
            Console.WriteLine(data);
        }
    }
}
