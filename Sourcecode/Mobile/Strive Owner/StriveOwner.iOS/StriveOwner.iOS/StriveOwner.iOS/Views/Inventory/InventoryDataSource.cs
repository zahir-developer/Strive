using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Linq;
using Acr.UserDialogs;
using Foundation;
using MvvmCross;
using MvvmCross.Base;
using MvvmCross.Platforms.Ios.Binding.Views;
using Strive.Core.Models.TimInventory;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views.Inventory
{
    public class InventoryDataSource : MvxTableViewSource
    {
        private static string CellId = "InventoryProd_Cell";
        private InventoryViewModel ViewModel;
        private ObservableCollection<InventoryDataModel> ItemList;
        public static IUserDialogs _userDialog = Mvx.IoCProvider.Resolve<IUserDialogs>();

        public InventoryDataSource(UITableView tableView, InventoryViewModel ViewModel) : base(tableView)
        {
            tableView.RegisterNibForCellReuse(InventoryProd_Cell.Nib, CellId);
            this.ViewModel = ViewModel;
        }

        public override IEnumerable ItemsSource
        {
            get => base.ItemsSource;
            set
            {
                if (value != null)
                {
                    ItemList = (ObservableCollection<InventoryDataModel>)value;
                }
                else
                {
                    ItemList = new ObservableCollection<InventoryDataModel>();
                }

                base.ItemsSource = value;
            }
        }

        protected override object GetItemAt(NSIndexPath indexPath)
        {
            var itm = ItemList[indexPath.Row];
            return itm;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return ItemList[indexPath.Row].DisplayRequestView
                ? InventoryProd_Cell.ExpandedHeight
                : InventoryProd_Cell.NormalHeight;
        }

        public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        public override UISwipeActionsConfiguration GetTrailingSwipeActionsConfiguration(UITableView tableView, NSIndexPath indexPath)
        {
            var actions = new UIContextualAction[1];
            actions[0] = UIContextualAction.FromContextualActionStyle
                    (UIContextualActionStyle.Destructive,
                        "Delete",
                        async (FlagAction, view, success) => {
                            if ((ItemList[indexPath.Row].Product.Quantity > 0))
                            {
                                success(false);
                                return;
                            }
                            var affirmative = _userDialog.ConfirmAsync("Are you sure want to delete this item?", "Delete", "Yes", "No");
                            if (await affirmative)
                            {
                                var response = await ViewModel.DeleteProductCommand(indexPath.Row);
                                if (response)
                                {
                                    success(true);
                                    await ViewModel.InventorySearchCommand("");
                                }
                                else
                                {
                                    success(false);
                                }
                            }
                            else
                            {
                                success(false);
                            }
                        });

            actions[0].Image = UIImage.FromBundle("icon-trash");
            actions[0].BackgroundColor = UIColor.Red;

            return UISwipeActionsConfiguration.FromActions(actions);
        }

        public override string TitleForDeleteConfirmation(UITableView tableView, Foundation.NSIndexPath indexPath)
        {
            return "Delete";
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return ItemList.Count();
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var item = ItemList[indexPath.Row];
            var cell = GetOrCreateCellFor(tableView, indexPath, item);
            cell.SelectionStyle = UITableViewCellSelectionStyle.None;
            IMvxDataConsumer bindable = cell as IMvxDataConsumer;
            if (bindable != null)
            {
                bindable.DataContext = item;
            }
            return cell;
        }

        protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, NSIndexPath indexPath, object item)
        {
            InventoryProd_Cell cell = (InventoryProd_Cell)tableView.DequeueReusableCell(CellId, indexPath);
            cell.SetCell(cell, ViewModel, indexPath.Row);
            cell.SetupCell(ItemList[indexPath.Row], () =>
               tableView.ReloadRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.None));
            return cell;
        }
    }
}
