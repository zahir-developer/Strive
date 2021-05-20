using System;
using System.Threading.Tasks;
using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using Strive.Core.ViewModels.TIMInventory;
using UIKit;

namespace StriveTimInventory.iOS.Views
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
           
            // Perform any additional setup after loading the view, typically from a nib.

            var InventoryTableSource = new InventoryTableViewDataSource(InventoryListTableView,ViewModel);

            var set = this.CreateBindingSet<InventoryView, InventoryViewModel>();
            set.Bind(InventoryTableSource).To(vm => vm.FilteredList);
            set.Bind(AddProductButton).To(vm => vm.Commands["AddProduct"]);
            set.Bind(LogoutButton).To(vm => vm.Commands["NavigateBack"]);
            //set.Bind(OtherInformationTableSource).For(s => s.SelectionChangedCommand).To(vm => vm.Commands["NavigateToDetail"]);
            //set.Bind(BackButton).To(vm => vm.Commands["NavigationBack"]);
            set.Apply();

            InventoryListTableView.Source = InventoryTableSource;
            InventoryListTableView.TableFooterView = new UIView(CGRect.Empty);
            InventoryListTableView.DelaysContentTouches = false;
            InventoryListTableView.ReloadData();

            InventorySearch.Placeholder = "Search";
            InventorySearch.TextChanged += SearchTextChanged;

            var Tap = new UITapGestureRecognizer(() => View.EndEditing(true));
            Tap.CancelsTouchesInView = false;
            View.AddGestureRecognizer(Tap);
        }

        public override async void ViewDidAppear(bool animated)
        {
            var vendors = await GetVendors();
            if (vendors)
            {
                GetProducts();
            }
        }

        private async Task GetProducts()
        {
            ViewModel.ClearCommand(); 
            //await ViewModel.GetProductsCommand();
            await ViewModel.InventorySearchCommand("");
        }

        private async Task<bool> GetVendors()
        {
            await ViewModel.GetVendorsCommand();
            return true;
        }

        private void SearchTextChanged(object sender, UISearchBarTextChangedEventArgs e)
        {
            ViewModel.InventorySearchCommand(e.SearchText);
            if(e.SearchText == "")
            {
                InventorySearch.ResignFirstResponder();
            }
        }

        void DoInitialSetup()
        {
            NavigationController.NavigationBarHidden = true;   
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

