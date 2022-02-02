using System;
using System.Threading.Tasks;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.Plugin.Messenger;
using MvvmCross.ViewModels;
using Strive.Core.Services.Interfaces;

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

        public BaseViewModel()
        {
            Commands = new MvxCommandCollectionBuilder().BuildCollectionFor(this);
            
        }

        public override async Task Initialize()
        {
            await base.Initialize();
        }

    }
}
