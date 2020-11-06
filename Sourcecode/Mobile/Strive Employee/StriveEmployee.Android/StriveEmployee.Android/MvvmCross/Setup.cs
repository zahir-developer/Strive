﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Acr.UserDialogs;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Binding.Bindings.Target.Construction;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.IoC;
using MvvmCross.Platforms.Android.Presenters;
using MvvmCross.Plugin.Json;
using Strive.Core.Rest.Implementations;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Implementations;
using Strive.Core.Services.Interfaces;

namespace StriveEmployee.Android.MvvmCross
{
    public class Setup : MvxAppCompatSetup<App>
    {
        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(NavigationView).Assembly,
            typeof(CoordinatorLayout).Assembly,
            typeof(FloatingActionButton).Assembly,
            typeof(Toolbar).Assembly,
            typeof(DrawerLayout).Assembly,
            typeof(ViewPager).Assembly,
            typeof(MvxRecyclerView).Assembly,
            typeof(MvxSwipeRefreshLayout).Assembly,
        };

        protected override void InitializeFirstChance()
        {
            Mvx.IoCProvider.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
            Mvx.IoCProvider.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IRestClient, RestClient>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IAdminService, AdminService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<IMessengerService, MessengerService>();
            Mvx.IoCProvider.ConstructAndRegisterSingleton<ICarwashLocationService, CarwashLocationService>();

            base.InitializeFirstChance();
        }

        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            MvxAppCompatSetupHelper.FillTargetFactories(registry);
            base.FillTargetFactories(registry);
        }


        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            return new MvxAppCompatViewPresenter(AndroidViewAssemblies);
        }

    }
}