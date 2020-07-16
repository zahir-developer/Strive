using System;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using StriveTimInventory.iOS.SupportView;
using StriveTimInventory.iOS.UIUtils;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    
    public partial class ClockInView : MvxViewController<ClockInViewModel>
    {
        private EmployeeRolesViewSource RolesCollectionViewSource;
        //UIBarButtonItem LogoutButton = new UIBarButtonItem();
        public ClockInView() : base("ClockInView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();
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

            RolesCollectionView.Delegate = new EmployeeRolesViewDelegate(RolesCollectionView, ViewModel);

            var set = this.CreateBindingSet<ClockInView, ClockInViewModel>();
            set.Bind(RolesCollectionViewSource).For(v => v.ItemsSource).To(vm => vm.RolesList);

            set.Bind(ClockinButton).To(vm => vm.Commands["NavigateClockedIn"]);
            set.Bind(Logoutbutton).To(vm => vm.Commands["NavigateBack"]);
            set.Apply();
        }

        private void DoInitialSetup()
        {
        //    NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes()
        //    {
        //        Font = DesignUtils.OpenSansBoldTitle(),
        //        ForegroundColor = UIColor.Clear.FromHex(0x24489A),
        //};
            
        //    LogoutButton.Title = "Logout";
        //    LogoutButton.SetTitleTextAttributes(new UITextAttributes()
        //    {
        //        Font = DesignUtils.OpenSansRegularText(),
        //        TextColor = UIColor.Clear.FromHex(0x24489A),
        //    }, UIControlState.Normal);
            //NavigationItem.LeftBarButtonItem = LogoutButton;
            NavigationController.NavigationBarHidden = true;
            ClockinButton.Layer.CornerRadius = 3;
        }
    }
}

