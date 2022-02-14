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
        readonly List<UIImage> imagePaths;
        readonly List<int> imageid;
        public ImagesSource(List<UIImage> imagePaths = null, List<int> imageid = null)
        { this.imagePaths = imagePaths; this.imageid = imageid; }

        public nint GetItemsCount(UICollectionView collectionView, nint section) => imagePaths?.Count ?? 3;

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (ImageCell)collectionView.DequeueReusableCell(ImageCell.Key, indexPath);
            cell.UpdateData(imagePaths[indexPath.Row]);
            return cell;
        }

        [Export("collectionView:didSelectItemAtIndexPath:")]
        public void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (ImageCell)collectionView.DequeueReusableCell(ImageCell.Key, indexPath);
            cell.ImageSelected(imageid[indexPath.Row]);
            
        }

        [Export("collectionView:layout:sizeForItemAtIndexPath:")]
        public CGSize GetSizeForItem(UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath)
            => new(56, 56);
    }
}
