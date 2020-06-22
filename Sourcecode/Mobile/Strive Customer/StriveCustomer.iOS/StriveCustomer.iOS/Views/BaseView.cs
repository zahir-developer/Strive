using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels;
using UIKit;

namespace StriveCustomer.iOS.Views
{
    public partial class BaseView : MvxViewController<BaseViewModel>
    {
        public BaseView() : base("BaseView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var set = this.CreateBindingSet<BaseView, BaseViewModel>();
            set.Bind(lblTitle).To(vm => vm.Title);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

