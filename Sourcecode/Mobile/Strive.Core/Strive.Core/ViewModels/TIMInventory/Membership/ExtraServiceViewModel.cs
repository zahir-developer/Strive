using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class ExtraServiceViewModel : BaseViewModel
    {

        public ObservableCollection<string> ExtraServiceList { get; set; } = new ObservableCollection<string>();

        public ExtraServiceViewModel()
        {
            ExtraServiceList.Add("No Silicone");
            ExtraServiceList.Add("No Brush/ No Sapilio");
            ExtraServiceList.Add("Wash Mitts");
            ExtraServiceList.Add("Rain X Protection");
            ExtraServiceList.Add("Celan Truck Bed");
            ExtraServiceList.Add("Bio Oder Remover");
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }

        public void NextCommand()
        {
            _navigationService.Navigate<TermsViewModel>();
        }
    }
}
