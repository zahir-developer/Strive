using System;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;
using MvvmCross.Binding.BindingContext;
using CoreGraphics;

namespace StriveTimInventory.iOS.Views.MembershipView
{
    public partial class ClientDetailView : MvxViewController<MembershipClientDetailViewModel>
    {
        public ClientDetailView() : base("ClientDetailView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationController.NavigationBarHidden = true;

            var VehicleSource = new ClientDetailSource(VehicleTableView, ViewModel);

            var set = this.CreateBindingSet<ClientDetailView, MembershipClientDetailViewModel>();
            set.Bind(VehicleSource).To(vm => vm.VehicleList);
            set.Bind(VehicleSource).For(s => s.SelectionChangedCommand).To(vm => vm.Commands["NavigateToDetail"]);
            set.Bind(BackButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(NameLabel).To(vm => vm.Name);
            set.Bind(ContactLabel).To(vm => vm.Contact);
            set.Bind(EmailLabel).To(vm => vm.Email);
            set.Apply();

            VehicleTableView.Source = VehicleSource;
            VehicleTableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
            VehicleTableView.SeparatorInset = new UIEdgeInsets(0, 10, 0, 10);
            VehicleTableView.SeparatorColor = UIColor.Gray;
            VehicleTableView.TableFooterView = new UIView(CGRect.Empty);
            VehicleTableView.DelaysContentTouches = false;
            
            VehicleTableView.ReloadData();
        }
        public override void ViewDidAppear(bool animated)
        {
            ViewDidLoad();
        }
        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

