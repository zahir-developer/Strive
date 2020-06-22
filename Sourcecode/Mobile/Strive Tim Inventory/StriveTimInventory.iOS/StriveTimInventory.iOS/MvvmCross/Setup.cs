using System;
using MvvmCross.IoC;
using MvvmCross.Platforms.Ios.Core;

namespace StriveTimInventory.iOS.MvvmCross
{
    public class Setup : MvxIosSetup<App>
    {
        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();
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
