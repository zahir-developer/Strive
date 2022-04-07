using System;
using MvvmCross.Platforms.Ios.Presenters.Attributes;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Owner;
using MvvmCross.Binding.BindingContext;
using UIKit;

namespace StriveOwner.iOS.Views.Inventory
{
    [MvxModalPresentation(
        ModalPresentationStyle = UIModalPresentationStyle.OverFullScreen,
        Animated = true,
        ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve)]

    public partial class IconsView : MvxViewController<IconsViewModel>
    {
        private IconsViewSource IconsCollectionViewSource;

        public IconsView() : base("IconsView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            IconCollectionView.Source = IconsCollectionViewSource = new IconsViewSource(IconCollectionView, ViewModel);

            IconCollectionView.Delegate = new IconsViewDelegate(IconCollectionView, ViewModel);

            var set = this.CreateBindingSet<IconsView, IconsViewModel>();
            set.Bind(IconsCollectionViewSource).For(v => v.ItemsSource).To(vm => vm.IconList);
            set.Bind(CancelButton).To(vm => vm.Commands["NavigateBack"]);
            set.Bind(SaveButton).To(vm => vm.Commands["Save"]);
            set.Apply();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

