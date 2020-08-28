using System;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class MembershipClientDetailViewModel : BaseViewModel
    {
        public MembershipClientDetailViewModel()
        {
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }
    }
}
