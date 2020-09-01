using System;
using Foundation;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
{
    public class EmployeeRolesViewDelegate : UICollectionViewDelegate
    {
        #region Computed Properties
        public UICollectionView CollectionView { get; set; }

        public ClockInViewModel viewModel { get; set; }
        #endregion

        #region Constructors
        public EmployeeRolesViewDelegate(UICollectionView collectionView, ClockInViewModel viewModel)
        {

            // Initialize
            CollectionView = collectionView;
            this.viewModel = viewModel;
        }
        #endregion

        #region Overrides Methods
        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {

            viewModel.RoleDecisionCommand(indexPath.Row);
        }

       
        #endregion
    }
}
