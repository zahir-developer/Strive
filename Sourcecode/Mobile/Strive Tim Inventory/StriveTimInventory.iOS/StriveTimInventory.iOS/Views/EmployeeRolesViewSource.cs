using System;
using System.Collections;
using System.Collections.Generic;
using Foundation;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public class EmployeeRolesViewSource : MvxCollectionViewSource
    {
        private readonly UICollectionView _collectionView;

        private static string CellId = "EmployeeRolesCell";

        public IList<EmployeeRole> RolesList { get; set; }

        public EmployeeRolesViewSource(UICollectionView collectionView)
            : base(collectionView, EmployeeRolesCell.Key)
        {
            _collectionView = collectionView;
            _collectionView.RegisterNibForCell(EmployeeRolesCell.Nib, CellId);
            ReloadOnAllItemsSourceSets = true;
        }

        public override IEnumerable ItemsSource
        {
            get => base.ItemsSource;
            set
            {
                if (value != null)
                {
                    RolesList = (IList<EmployeeRole>)value;
                    if(RolesList.Count < 3)
                    {
                        RolesList.Add(new EmployeeRole("","",0,UIImage.FromBundle("")));
                        RolesList.Add(new EmployeeRole("", "", 0, UIImage.FromBundle("")));
                    }
                }
                else
                {
                    RolesList = new List<EmployeeRole>();
                }
                base.ItemsSource = value;
            }
        }

        public override nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return RolesList.Count;
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            return RolesList[indexPath.Row];
        }

        protected override UICollectionViewCell GetOrCreateCellFor(UICollectionView collectionView,
            NSIndexPath indexPath, object item)
        {
            return (UICollectionViewCell)collectionView.DequeueReusableCell(CellId, indexPath);
        }
    }
}
