using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace StriveTimInventory.iOS.SupportView
{
    public class FlowDelegate : UICollectionViewDelegateFlowLayout
    {
        [Export("collectionView:layout:insetForSectionAtIndex:")]
        public UIEdgeInsets GetInsetForSection(UICollectionView collectionView, UICollectionViewLayout layout, nint section)
        {
            return new UIEdgeInsets(1.0f, 1.0f, 1.0f, 1.0f);
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, Foundation.NSIndexPath indexPath)
        {
            var lay = layout as UICollectionViewFlowLayout;
            var widthPerItem = collectionView.Frame.Width / 4 - lay.MinimumInteritemSpacing;

            return new CGSize(widthPerItem, 100);
        }
    }
}
