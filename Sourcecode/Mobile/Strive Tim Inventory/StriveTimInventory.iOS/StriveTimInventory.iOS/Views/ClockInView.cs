using System.Drawing;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
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
            var topInset = 10;
            var bottomInset = 10;
            var leftInset = 300;
            var itemsCount = RolesCollectionView.NumberOfItemsInSection(0);

            if (itemsCount > 2)
            {
                if (itemsCount % 2 != 0)
                    itemsCount++;
                leftInset = (int)(leftInset - ((itemsCount / 1.35) * 50));
            }
            else
            {
                topInset = bottomInset = 75;
                leftInset = 150;
            }
            if (itemsCount == 1)
            {
                leftInset = 300;
            }
            var rightInset = leftInset;
            var layout = new UICollectionViewFlowLayout
            {
                SectionInset = new UIEdgeInsets(topInset, leftInset, bottomInset, rightInset),
                MinimumInteritemSpacing = 15,
                MinimumLineSpacing = 15,
                ItemSize = new SizeF(150, 150)
            };

            RolesCollectionView.SetCollectionViewLayout(layout, true);
        }
    }
}

