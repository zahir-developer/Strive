using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory.Membership;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class SelectUpchargeView : MvxViewController<SelectUpchargeViewModel>
    {
        public SelectUpchargeView() : base("SelectUpchargeView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;
            var UpchargeTableSource = new SelectUpchargeSource(UpchargeTable, ViewModel);

            var set = this.CreateBindingSet<SelectUpchargeView, SelectUpchargeViewModel>();
            set.Bind(UpchargeTableSource).To(vm => vm.UpchargesList);
            set.Bind(NextButton).To(vm => vm.Commands["Next"]);
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Apply();

            UpchargeTable.Source = UpchargeTableSource;
            UpchargeTable.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            UpchargeTable.SeparatorInset = new UIEdgeInsets(0, 10, 0, 10);
            UpchargeTable.SeparatorColor = UIColor.Gray;
            UpchargeTable.TableFooterView = new UIView(CGRect.Empty);
            UpchargeTable.DelaysContentTouches = false;
            UpchargeTable.ReloadData();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

