using System;
using MvvmCross;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Greeter.Modules.Base
{
    public class BaseViewModel : MvxViewModel
    {
        public static IMvxNavigationService navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();

        public bool IsLoading { get; set; }
    }
}
