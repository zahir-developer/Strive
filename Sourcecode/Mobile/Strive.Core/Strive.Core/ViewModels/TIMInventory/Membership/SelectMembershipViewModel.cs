using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.TIMInventory.Membership
{
    public class SelectMembershipViewModel : BaseViewModel
    {

        public ObservableCollection<string> MembershipList { get; set; } = new ObservableCollection<string>();

        public SelectMembershipViewModel()
        {
            MembershipList.Add("Ultra Mammoth");
            MembershipList.Add("Mini Mammoth");
            MembershipList.Add("Mega Mammoth");
            MembershipList.Add("Mammoth");
            RaiseAllPropertiesChanged();
        }

        public void NextCommand()
        {
            _navigationService.Navigate<SelectUpchargeViewModel>();
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }
    }
}
