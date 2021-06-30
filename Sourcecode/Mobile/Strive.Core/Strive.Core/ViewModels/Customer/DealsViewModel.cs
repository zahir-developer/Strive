using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Strive.Core.Models.Customer;

namespace Strive.Core.ViewModels.Customer
{
    public class DealsViewModel : BaseViewModel
    {
        public ObservableCollection<GetAllDeal> Deals { get; set; } = new ObservableCollection<GetAllDeal>();
        //public DealsList Deals { get; set; } = new DealsList();
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

    } 
}
