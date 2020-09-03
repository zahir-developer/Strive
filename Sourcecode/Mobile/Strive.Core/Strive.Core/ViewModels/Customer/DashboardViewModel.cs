using Acr.UserDialogs;
using MvvmCross.Commands;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer
{
    public class DashboardViewModel : BaseViewModel
    {

        #region Commands

        
        public void Logout()
        {
            var confirmconfig = new ConfirmConfig
            {
                Title= Strings.LogoutTitle,
                Message= Strings.LogoutMessage,
                CancelText= Strings.LogoutCancelButton,
                OkText= Strings.LogoutSuccessButton,
                OnAction= success => 
                { 
                    if(success)
                    {
                        CustomerInfo.Clear();
                      _navigationService.Navigate<LoginViewModel>();
                    }
                }

            };
            _userDialog.Confirm(confirmconfig);

        }

        #endregion Commands

    }
}   
