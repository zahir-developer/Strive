using System;
using System.Drawing;
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
        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();
            var totalCellWidth = 150 * RolesCollectionView.NumberOfItemsInSection(0);
            var totalSpacingWidth = 20 * (RolesCollectionView.NumberOfItemsInSection(0) - 1);

            var leftInset = RolesCollectionView.Layer.Frame.Size.Width - (totalCellWidth + totalSpacingWidth) / 2;
            var rightInset = leftInset;
            var layout = new UICollectionViewFlowLayout
            {
                SectionInset = new UIEdgeInsets(10, 5, 10, 5),
                MinimumInteritemSpacing = 15,
                MinimumLineSpacing = 15,
                ItemSize = new SizeF(150, 150) 
            };

            RolesCollectionView.SetCollectionViewLayout(layout, true);
        }
    }
}

