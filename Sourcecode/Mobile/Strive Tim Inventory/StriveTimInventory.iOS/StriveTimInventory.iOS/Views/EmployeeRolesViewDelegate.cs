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

        public ClockInView View { get; set; }
        #endregion

        #region Constructors
        public EmployeeRolesViewDelegate(UICollectionView collectionView, ClockInViewModel viewModel, ClockInView view)
        {

            // Initialize
            CollectionView = collectionView;
            this.viewModel = viewModel;
            View = view;            
        }
        #endregion

        #region Overrides Methods
        public override void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            viewModel.RoleDecisionCommand(indexPath.Row);
            View.ClockInBtnView();
        }
        #endregion                
    }
}
