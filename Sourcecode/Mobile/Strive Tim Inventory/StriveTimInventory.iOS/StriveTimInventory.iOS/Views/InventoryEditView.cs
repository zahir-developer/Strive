using System;
using MvvmCross.Binding.BindingContext;
using UIKit;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;

namespace StriveTimInventory.iOS.Views
{
    public partial class InventoryEditView : MvxViewController<InventoryEditViewModel>
    {
        public InventoryEditView() : base("InventoryEditView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var set = this.CreateBindingSet<InventoryEditView, InventoryEditViewModel>();
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(ItemCode).To(vm => vm.ItemCode);
            set.Bind(ItemName).To(vm => vm.ItemName);
            set.Bind(ItemDescription).To(vm => vm.ItemDescription);
            set.Bind(ItemQuantity).To(vm => vm.ItemQuantity);
            set.Apply();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

