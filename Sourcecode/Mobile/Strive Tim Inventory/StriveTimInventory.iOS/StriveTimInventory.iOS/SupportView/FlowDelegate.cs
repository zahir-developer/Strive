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
            return new UIEdgeInsets(0, 0, 0,0);
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, Foundation.NSIndexPath indexPath)
        {
            var lay = layout as UICollectionViewFlowLayout;
            var widthPerItem = 150;
            lay.MinimumLineSpacing = 20;
            return new CGSize(widthPerItem, 150);
        }
    }
}
