using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace StriveTimInventory.iOS.SupportView
{
    public class FlowDelegate : UICollectionViewDelegateFlowLayout
    {

        public override UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            var totalCellWidth = 150 * collectionView.NumberOfItemsInSection(0);
            var totalSpacingWidth = 20 * (collectionView.NumberOfItemsInSection(0) - 1);

            var leftInset = collectionView.Layer.Frame.Size.Width - (totalCellWidth + totalSpacingWidth) / 2;
            var rightInset = leftInset;
            return new UIEdgeInsets(0, leftInset, 0, rightInset);
        }

        public override CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
        {
            var lay = layout as UICollectionViewFlowLayout;
            var widthPerItem = 150;
            lay.MinimumLineSpacing = 20;
            return new CGSize(widthPerItem, 150);
        }
    }
}
