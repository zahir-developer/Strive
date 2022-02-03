using System;

using UIKit;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory.Membership;
using CoreGraphics;
using StriveTimInventory.iOS.Views.CardList;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class VehicleMembershipDetailView : MvxViewController<VehicleMembershipDetailViewModel>
    {
        public VehicleMembershipDetailView() : base("VehicleMembershipDetailView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;

            var set = this.CreateBindingSet<VehicleMembershipDetailView, VehicleMembershipDetailViewModel>();
            set.Bind(MembershipName).To(vm => vm.MembershipName);
            set.Bind(ActivatedDate).To(vm => vm.ActivatedDate);
            set.Bind(CancelledDate).To(vm => vm.CancelledDate);
            set.Bind(Status).To(vm => vm.Status);
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(ChangeButton).To(vm => vm.Commands["ChangeMembership"]);
            set.Bind(CancelButton).To(vm => vm.Commands["CancelMembership"]);
            set.Apply();
            CardDetailsTable.RegisterNibForCellReuse(CardListViewCell.Nib, CardListViewCell.Key);
            CardDetailsTable.BackgroundColor = UIColor.Clear;
            CardDetailsTable.ReloadData();
            GetCardList();
        }

        private  void GetCardList()
        {
            ViewModel.FetchCardDetails();

            if (ViewModel.response != null)
            {
                CardDetailsTable.Hidden = false;
                _NoRelatableData.Hidden = true;
            }
            else
            {
                CardDetailsTable.Hidden = false;
                _NoRelatableData.Hidden = true;
            }

            if (this.ViewModel.response != null)
            {
                var CardTableSource = new CardListTableSource(ViewModel);
                CardDetailsTable.Source = CardTableSource;
                CardDetailsTable.TableFooterView = new UIView(CGRect.Empty);
                CardDetailsTable.DelaysContentTouches = false;
                CardDetailsTable.ReloadData();
            }

        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

