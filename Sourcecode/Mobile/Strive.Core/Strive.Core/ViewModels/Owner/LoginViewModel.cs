using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Owner
{
    public class LoginViewModel : BaseViewModel
    {


        public async void getNavigation()
        {
            await _navigationService.Navigate<SampleViewModel>();
        }
    }
}
