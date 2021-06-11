using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Strive.Core.Models.Customer;

namespace Strive.Core.ViewModels.Customer
{
    public class DealsViewModel : BaseViewModel
    {
        public DealsList Deals { get; set; } = new DealsList();
        public DealsViewModel()
        {
            for(int i =1;i<=2;i++)
            {
                DealsList.Add("Deals " + i);
            }
        }

        public ObservableCollection<string> DealsList { get; set; } = new ObservableCollection<string>();

        public async Task GetAllDealsCommand()
        {
            Deals = await AdminService.GetAllDeals();
        }

        public async Task NavigateToDetailCommand(string item)
        {
            await _navigationService.Navigate<DealsDetailViewModel>();
        }

    } 
}
