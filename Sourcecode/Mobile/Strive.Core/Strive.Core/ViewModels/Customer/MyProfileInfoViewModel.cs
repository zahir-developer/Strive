using Acr.UserDialogs;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.Customer
{
    public class MyProfileInfoViewModel : BaseViewModel
    {



        #region Commands

        public void LogoutCommand()
        {
            var confirmconfig = new ConfirmConfig
            {
                Title = Strings.LogoutTitle,
                Message = Strings.LogoutMessage,
                CancelText = Strings.LogoutCancelButton,
                OkText = Strings.LogoutSuccessButton,
                OnAction = success =>
                {
                    if (success)
                    {
                        CustomerInfo.Clear();
                        _navigationService.Close(this);
                        _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
                    }
                }

            };
            _userDialog.Confirm(confirmconfig);
        }


        #endregion Commands
    }
}
