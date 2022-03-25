using System;
using System.Threading.Tasks;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.Owner;
using UIKit;

namespace StriveOwner.iOS.Views.Inventory
{
    public partial class InventoryView : MvxViewController<InventoryViewModel>
    {
        public InventoryView() : base("InventoryView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            DoInitialSetup();

            AddProd_Btn.Layer.CornerRadius = 5;
            var inventoryTableSource = new InventoryDataSource(Inventory_TableView, ViewModel);

            var set = this.CreateBindingSet<InventoryView, InventoryViewModel>();
            set.Bind(inventoryTableSource).To(vm => vm.FilteredList);
            set.Bind(AddProd_Btn).To(vm => vm.Commands["AddProduct"]);
            set.Bind(LogoutButton).To(vm => vm.Commands["NavigateBack"]);
            set.Apply();

            Inventory_TableView.Source = inventoryTableSource;
            Inventory_TableView.TableFooterView = new UIView(CGRect.Empty);
            Inventory_TableView.DelaysContentTouches = false;
            Inventory_TableView.ReloadData();

            InventorySearch.Placeholder = "Search";
            InventorySearch.TextChanged += SearchTextChanged;

            InventorySearch.Layer.CornerRadius = 5;
            Inventory_TableView.Layer.CornerRadius = 5;
        }

        public override async void ViewDidAppear(bool animated)
        {
            var vendors = await GetVendors();
            if (vendors)
            {
                ViewModel.ClearCommand();
                await ViewModel.InventorySearchCommand("");
            }
        }               

        private async Task<bool> GetVendors()
        {
            await ViewModel.GetVendorsCommand();    
            return true;
        }

        private void SearchTextChanged(object sender, UISearchBarTextChangedEventArgs e)
        {
            ViewModel.InventorySearchCommand(e.SearchText);
            if (e.SearchText == "")
            {
                InventorySearch.ResignFirstResponder();
            }
        }

        void DoInitialSetup()
        {
            NavigationController.NavigationBarHidden = true;

            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

