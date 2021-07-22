//using System;
//using Microsoft.Extensions.Logging;
//using MvvmCross.IoC;
//using MvvmCross.Platforms.Ios.Core;

//namespace Greeter.MvvmCross
//{
//    public class Setup : MvxIosSetup<App>
//    {
//        //protected override void InitializeFirstChance()
//        //{
//        //    base.InitializeFirstChance();

//        //    Mvx.IoCProvider.RegisterType<IMvxJsonConverter, MvxJsonConverter>();
//        //}

//        //protected override void InitializeLastChance()
//        //{
//        //    base.InitializeLastChance();

//        //    var registry = Mvx.IoCProvider.Resolve<IMvxTargetBindingFactoryRegistry>();
//        //    registry.RegisterFactory(new MvxCustomBindingFactory<UIViewController>("NetworkIndicator", (viewController) => new NetworkIndicatorTargetBinding(viewController)));
//        //}

//        //protected override IMvxIocOptions CreateIocOptions()
//        //{
//        //    return new MvxIocOptions
//        //    {
//        //        PropertyInjectorOptions = MvxPropertyInjectorOptions.MvxInject
//        //    };
//        //}

//        protected override ILoggerFactory CreateLogFactory()
//        {
//            return null;
//        }

//        protected override ILoggerProvider CreateLogProvider()
//        {
//            return null;
//        }

//        //protected override IMvxApplication CreateApp(IMvxIoCProvider iocProvider)
//        //{
//        //return base.CreateApp(iocProvider);
//        //    return new App();
//        //}

//        //protected override ILoggerProvider CreateLogProvider()
//        //{
//        //    throw new SerilogLoggerProvider();
//        //}

//        //protected override ILoggerFactory CreateLogFactory()
//        //{
//        //    Log.Logger = new LoggerConfiguration()
//        //       .MinimumLevel.Debug()
//        //       .CreateLogger();

//        //    return new SerilogLoggerFactory();
//        //}
//    }
//}
