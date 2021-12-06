using System;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory.Membership;
using UIKit;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class SelectMembershipView : MvxViewController<SelectMembershipViewModel>
    {
        public SelectMembershipView() : base("SelectMembershipView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;
            var MembershipTableSource = new SelectMembershipSource(MembershipTableView, ViewModel);

            var set = this.CreateBindingSet<SelectMembershipView, SelectMembershipViewModel>();
            set.Bind(MembershipTableSource).To(vm => vm.MembershipServiceList);
            set.Bind(NextButton).To(vm => vm.Commands["Next"]);
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Apply();

            MembershipTableView.Source = MembershipTableSource;
            MembershipTableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            MembershipTableView.SeparatorInset = new UIEdgeInsets(0, 10, 0, 10);
            MembershipTableView.SeparatorColor = UIColor.Gray;
            MembershipTableView.TableFooterView = new UIView(CGRect.Empty);
            MembershipTableView.DelaysContentTouches = false;
            MembershipTableView.ReloadData();

            ViewModel.getMembershipDetails();

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

