using System;
using System.Collections.Generic;
using Foundation;
using Greeter.Cells;
using UIKit;

namespace Greeter.Sources
{
    public class ImagesSource : UICollectionViewSource
    {
        public List<string> imagePaths;

        public ImagesSource(List<string> imagePaths = null)
        {
            this.imagePaths = imagePaths;
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return imagePaths?.Count ?? 3;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (ImageCell)collectionView.DequeueReusableCell(ImageCell.Key, indexPath);
            cell.UpdateData(imagePaths?[indexPath.Row]);
            return cell;
        }
    }
}
