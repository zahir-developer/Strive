﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.ViewModels.Employee
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {

        }

        public void NavigateTO()
        {
            _navigationService.Navigate<BaseViewExampleViewModel>();
        }
    }
}
