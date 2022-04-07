using System;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views.Inventory
{
    public class IconsViewSource : MvxCollectionViewSource
    
    {

        private readonly UICollectionView _collectionView;

        private static string CellId = "IconViewCell";

        public IList<string> IconsList { get; set; }

        private IconsViewModel ViewModel;
        public IconsViewSource(UICollectionView collectionView, IconsViewModel ViewModel)
             : base(collectionView, IconViewCell.Key)
        {
            _collectionView = collectionView;
            this.ViewModel = ViewModel;
            _collectionView.RegisterNibForCell(IconViewCell.Nib, CellId);
            ReloadOnAllItemsSourceSets = true;
        }

        public override IEnumerable ItemsSource
        {
            get => base.ItemsSource;
            set
            {
                IconsList = ViewModel.IconList;
                //if (value != null)
                //{
                //    IconsList = (IList<string>)value;
                //}
                //else
                //{
                //    IconsList = new List<string>();
                //}
                //base.ItemsSource = value;
            }
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return IconsList.Count;
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            return IconsList[indexPath.Row];
        }

        protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView,
            NSIndexPath indexPath, object item)
        {
            IconViewCell cell = (IconViewCell)collectionView.DequeueReusableCell(CellId, indexPath);
            cell.SetCell(cell, IconsList[indexPath.Row]);
            return cell;
        }
    }
}
