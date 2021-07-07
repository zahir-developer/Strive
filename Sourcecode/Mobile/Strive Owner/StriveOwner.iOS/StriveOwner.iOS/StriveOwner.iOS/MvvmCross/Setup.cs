using System;
using Acr.UserDialogs;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;
using MvvmCross.Plugin.Json;
using Strive.Core.Rest.Implementations;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Implementations;
using Strive.Core.Services.Interfaces;

namespace StriveOwner.iOS.MvvmCross
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.IoCProvider.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IRestClient, RestClient>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IAdminService, AdminService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<ILocationService, LocationService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMessengerService, MessengerService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<ICarwashLocationService, CarwashLocationService>();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }

        protected override IMvxIocOptions CreateIocOptions()
        {
            return new MvxIocOptions
            {
                PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
            };
        }
    }
}
