using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using StriveTimInventory.iOS.SupportView;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    [MvxTabPresentation(WrapInNavigationController = true, TabName = "Clock", TabIconName = "ic_planets")]
    public partial class ClockInView : MvxViewController<ClockInViewModel>
    {
        private EmployeeRolesViewSource RolesCollectionViewSource;
        public ClockInView() : base("ClockInView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CreateBindings();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        private void CreateBindings()
        {
            RolesCollectionView.Source = RolesCollectionViewSource = new EmployeeRolesViewSource(RolesCollectionView);
            RolesCollectionView.Delegate = new FlowDelegate();

            var set = this.CreateBindingSet<ClockInView, ClockInViewModel>();
            set.Bind(RolesCollectionViewSource).For(v => v.ItemsSource).To(vm => vm.RolesList);
            //set.Bind(booksCollectionViewSource).For(v => v.SelectionChangedCommand).To(vm => vm.ItemSelectedCmd);
            set.Apply();
        }
    }
}

