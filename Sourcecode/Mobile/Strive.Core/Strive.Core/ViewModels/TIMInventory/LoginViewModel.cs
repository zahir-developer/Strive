﻿using System;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;
using Strive.Core.Utils.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
           
        }

        #region Properties
        private bool _isPasswordSecure = true;

        public bool isPasswordSecure {
            get
            {
                return _isPasswordSecure;
            }
            set
            {
                SetProperty(ref _isPasswordSecure, value);
            }
        }

        public string UserId { get; set; } = "caradmin@strive.com";

        public string Password { get; set; } = "pass@123";

        public string UserIdText
        {
            get
            {
                return Strings.UserId;
            }
        }
        public string PasswordText
        {
            get
            {
                return Strings.Password;
            }
        }

        public string Title
        {
            get
            {
                return Strings.TIM_APP_TITLE;
            }
            set
            { }
        }

        private EmployeeLoginRequest request;
        #endregion Properties

        #region Commands

        public async Task LoginCommand()
        {
            if (await ValidateCredentialsAsync())
            {
                _userDialog.ShowLoading(Strings.LoggingIn, Acr.UserDialogs.MaskType.Gradient);
                var response = await AdminService.EmployeeLogin(new EmployeeLoginRequest(UserId, Password));
                if(response.Token != null)
                {
                    EmployeeData.EmployeeDetails = response.EmployeeDetails;
                    ApiUtils.Token = response.Token;
                    var status = await AdminService.GetClockInStatus(11, DateUtils.GetTodayDateString());
                    if(status.TimeClock != null)
                    {
                        EmployeeData.ClockInStatus = status.TimeClock;
                    }
                    await _navigationService.Navigate<RootViewModel>();
                }
                _userDialog.HideLoading();
            }
        }

        async Task<bool> ValidateCredentialsAsync()
        {
            bool isValid = true;
            if(!Validations.validateEmail(UserId))
            {
                await _userDialog.AlertAsync(Strings.ValidEmail, Strings.Alert);
                return !isValid;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                await _userDialog.AlertAsync(Strings.EnterPassword, Strings.Alert);
                return !isValid;
            }
            return isValid;
        }

        public void PasswordToggleCommand()
        {
            isPasswordSecure = !_isPasswordSecure;
        }

        public void DoLogin()
        {
            AdminService.Login("Admin", "Admin"); 
        }

        #endregion Commands
    }
}
