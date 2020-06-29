using System;
using System.Threading.Tasks;
using MvvmCross;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using Strive.Core.Services.Interfaces;

namespace Strive.Core.ViewModels
{
    public class BaseViewModel : MvxViewModel
    {
        public IMvxCommandCollection Commands { get; protected set; }
        public static IMvxNavigationService _navigationService = Mvx.IoCProvider.Resolve<IMvxNavigationService>();

        public IAdminService AdminService = Mvx.IoCProvider.Resolve<IAdminService>();

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
