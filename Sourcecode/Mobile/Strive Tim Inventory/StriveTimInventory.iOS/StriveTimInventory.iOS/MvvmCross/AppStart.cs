﻿using System;
using System.Threading.Tasks;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Strive.Core.ViewModels;
using Strive.Core.ViewModels.TIMInventory;

namespace StriveTimInventory.iOS.MvvmCross
{
    public class AppStart : MvxAppStart
    {
        public AppStart(IMvxApplication app, IMvxNavigationService mvxNavigationService)
           : base(app, mvxNavigationService)
        {
        }

        protected override Task NavigateToFirstViewModel(object hint = null)
        {
            return NavigationService.Navigate<LoginViewModel>();
        }
    }
}
