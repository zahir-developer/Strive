using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class SelectUpchargeViewModel : BaseViewModel
    {
        public ObservableCollection<string> UpchargesList { get; set; } = new ObservableCollection<string>();

        public SelectUpchargeViewModel()
        {
            UpchargesList.Add("None");
            UpchargesList.Add("A-$15/75");
            UpchargesList.Add("A-$15/150");
            UpchargesList.Add("C-$25/300");
            UpchargesList.Add("D-$35/500");
            UpchargesList.Add("E-$50/600");
            RaiseAllPropertiesChanged();
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }

        public void NextCommand()
        {
            _navigationService.Navigate<ExtraServiceViewModel>();
        }
    }
}
