using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory.Membership;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class ExtraServiceView : MvxViewController<ExtraServiceViewModel>
    {
        public ExtraServiceView() : base("ExtraServiceView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;
            var MembershipTableSource = new ExtraServiceSource(ExtraServiceTable, ViewModel);

            var set = this.CreateBindingSet<ExtraServiceView, ExtraServiceViewModel>();
            set.Bind(MembershipTableSource).To(vm => vm.serviceList);
            set.Bind(NextButton).To(vm => vm.Commands["Next"]);
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Apply();

            ExtraServiceTable.Source = MembershipTableSource;
            ExtraServiceTable.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            ExtraServiceTable.SeparatorInset = new UIEdgeInsets(0, 10, 0, 10);
            ExtraServiceTable.SeparatorColor = UIColor.Gray;
            ExtraServiceTable.TableFooterView = new UIView(CGRect.Empty);
            ExtraServiceTable.DelaysContentTouches = false;
            ExtraServiceTable.ReloadData();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

