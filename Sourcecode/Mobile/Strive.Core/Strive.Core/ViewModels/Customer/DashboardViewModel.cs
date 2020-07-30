using MvvmCross.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer
{
    public class DashboardViewModel : BaseViewModel
    {
        public IMvxCommand ShowMapCommand { get; private set; }

        #region Commands

        public void navigateToMap()
        {
            ShowMapCommand = new MvxAsyncCommand(async () => await _navigationService.Navigate<MapViewModel>());
        }

        #endregion Commands

    }
}
