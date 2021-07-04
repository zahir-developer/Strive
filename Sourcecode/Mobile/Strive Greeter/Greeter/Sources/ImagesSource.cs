using System;
using System.Collections.Generic;
using CoreGraphics;
using Foundation;
using Greeter.Cells;
using UIKit;

namespace Greeter.Sources
{
    public class ImagesSource : NSObject, IUICollectionViewDataSource, IUICollectionViewDelegateFlowLayout
    {
        readonly List<string> imagePaths;

        public ImagesSource(List<string> imagePaths = null)
            => this.imagePaths = imagePaths;

        public nint GetItemsCount(UICollectionView collectionView, nint section) => imagePaths.Count;

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (ImageCell)collectionView.DequeueReusableCell(ImageCell.Key, indexPath);
            cell.UpdateData(imagePaths[indexPath.Row]);
            return cell;
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
            => new(56, 56);
    }
}
