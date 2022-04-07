using System;
using Foundation;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views.Inventory
{
    public class IconsViewDelegate : UICollectionViewDelegate
    {
        #region Computed Properties
        public UICollectionView CollectionView { get; set; }

        public IconsViewModel viewModel { get; set; }
        #endregion

        #region Constructors
        public IconsViewDelegate(UICollectionView collectionView, IconsViewModel viewModel)
        {

            // Initialize
            CollectionView = collectionView;
            this.viewModel = viewModel;
        }
        #endregion

        #region Overrides Methods
        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {

            viewModel.SelectedIconCommand(indexPath.Row);
        }


        #endregion
    }
}
