using System;
using Foundation;
using Greeter.Cells;
using UIKit;

namespace Greeter.Sources
{
    public class ImagesSource : UICollectionViewSource
    {
        public ImagesSource()
        {
        }

        public override nint NumberOfSections(UICollectionView collectionView)
        {
            return 1;
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return 3;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (ImageCell)collectionView.DequeueReusableCell(ImageCell.Key, indexPath);
            //var animal = animals[indexPath.Row];
            return cell;
        }
    }
}
