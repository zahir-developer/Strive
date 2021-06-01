using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class DealsViewModel : BaseViewModel
    {
        public DealsViewModel()
        {
            for(int i =1;i<=2;i++)
            {
                DealsList.Add("Deals " + i);
            }
        }

        public ObservableCollection<string> DealsList { get; set; } = new ObservableCollection<string>();


        public async Task NavigateToDetailCommand(string item)
        {
            await _navigationService.Navigate<DealsDetailViewModel>();
        }

    } 
}
