using System;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Strive.Core.Resources;
using Strive.Core.Rest.Implementations;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;
using Xamarin.Essentials;

namespace Strive.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        public IMvxCommandCollection Commands { get; protected set; }
        public static IMvxNavigationService _navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();
        public static IMvxMessenger _mvxMessenger = Mvx.IoCProvider.Resolve<IMvxMessenger>();
        public static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();
        public IAdminService AdminService = Mvx.IoCProvider.Resolve<IAdminService>();
        public IMessengerService MessengerService = Mvx.IoCProvider.Resolve<IMessengerService>();
        public static bool isExitApp;
        public BaseViewModel()
        {
            Commands = new MvxCommandCollectionBuilder().BuildCollectionFor(this);
            
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

        public  async Task LogoutAppAsync()
        {
            var appName = AppInfo.Name;
            var packageName = AppInfo.PackageName;
           
            if (appName.Equals(Strings.EmployeeAppName) && packageName.Equals(Strings.EmployeePackageName))
            {
                await _navigationService.Navigate<Employee.LoginViewModel>();
            }
            if (appName.Equals(Strings.OwnerAppName) && packageName.Equals(Strings.OwnerPackageName))
            {
                await _navigationService.Navigate<Owner.LoginViewModel>();
            }
            if (appName.Equals(Strings.CustomerAppName) && packageName.Equals(Strings.CustomerPackageName))
            {
                await _navigationService.Navigate<Customer.LoginViewModel>();
            }
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "exit!"));
        }
    }
}
