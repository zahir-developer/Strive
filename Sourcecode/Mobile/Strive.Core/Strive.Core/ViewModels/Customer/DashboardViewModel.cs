using Acr.UserDialogs;
using MvvmCross.Commands;
using MvvmCross.Plugin.Messenger;
using Strive.Core.Models.Customer;
using Strive.Core.Resources;
using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Customer
{
    public class DashboardViewModel : BaseViewModel
    {
        private MvxSubscriptionToken _messageToken;
        public bool isAndroid = false;
        public DashboardViewModel()
        {
            _messageToken = _mvxMessenger.Subscribe<ValuesChangedMessage>(OnReceivedMessageAsync);
        }

        #region Commands

        private async void OnReceivedMessageAsync(ValuesChangedMessage message)
        {
            if (message.Valuea == 1)
            {
                await _navigationService.Close(this);
                _messageToken.Dispose();
            }
        }

        public override void ViewDisappeared()
        {
            _messageToken.Dispose();
        }

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
                        if (!isAndroid)
                        {
                            _navigationService.Close(this);
                        }
                        else
                        {
                             _navigationService.Navigate<LoginViewModel>();
                        }
                        
                    }
                }

            };
            _userDialog.Confirm(confirmconfig);

        }

        #endregion Commands

    }
}   
