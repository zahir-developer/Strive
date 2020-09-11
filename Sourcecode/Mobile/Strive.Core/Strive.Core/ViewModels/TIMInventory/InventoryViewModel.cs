using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Resources;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class InventoryViewModel : BaseViewModel
    {
        public InventoryViewModel()
        {
        }

        public ObservableCollection<InventoryDataModel> FilteredList { get; set; } = new ObservableCollection<InventoryDataModel>();

        private ObservableCollection<ProductDetail> ProductList = new ObservableCollection<ProductDetail>();

        private ObservableCollection<VendorDetail> VendorList = new ObservableCollection<VendorDetail>();

        private ObservableCollection<InventoryDataModel> InventoryList = new ObservableCollection<InventoryDataModel>();

        private ObservableCollection<InventoryDataModel> EditableList = new ObservableCollection<InventoryDataModel>();

        public async Task GetProductsCommand()
        {
            _userDialog.ShowLoading(Strings.Loading);
            Products products = await AdminService.GetAllProducts();
            foreach(var product in products.Product)
            {
                ProductList.Add(product);
                var vendor = VendorList.Where(s => s.VendorId == product.VendorId).FirstOrDefault();
                if(vendor != null)
                {
                    InventoryList.Add(new InventoryDataModel() { Product = product, Vendor = vendor });
                }               
            }
            _userDialog.HideLoading();
            await RaiseAllPropertiesChanged();
        }

        public async Task GetVendorsCommand()
        {
            _userDialog.ShowLoading(Strings.Loading);
            Vendors vendors = await AdminService.GetAllVendors();
            foreach (var vendor in vendors.Vendor)
            {
                VendorList.Add(vendor);
            }
            EmployeeData.Vendors = vendors;
            _userDialog.HideLoading();
            await RaiseAllPropertiesChanged();
        }

        public async Task InventorySearchCommand(string SearchText)
        {
            var searchList = await AdminService.SearchProduct(SearchText);
            ClearCommand();
            foreach (var product in searchList.ProductSearch)
            {
                ProductList.Add(product);
                var vendor = VendorList.Where(s => s.VendorId == product.VendorId).FirstOrDefault();
                if (vendor != null)
                {
                    InventoryList.Add(new InventoryDataModel() { Product = product, Vendor = vendor });
                }
            }
            //FilteredList = new ObservableCollection<InventoryDataModel>(InventoryList.
            //    Where(s => s.Product.ProductName.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())));
            FilteredList = InventoryList;
            EditableList = FilteredList;
            RaiseAllPropertiesChanged();
        }

        public void IncrementCommand(int index)
        {
            FilteredList[index].Product.Quantity++;
            RaiseAllPropertiesChanged();
        }

        public void DecrementCommand(int index)
        {
            if (!(FilteredList[index].Product.Quantity > 0))
                return;
            FilteredList[index].Product.Quantity--;
            RaiseAllPropertiesChanged();
        }

        public void ClearCommand()
        {
            FilteredList.Clear();
            InventoryList.Clear();
            ProductList.Clear();
        }

        public async void EditCommand(int index)
        {
            EmployeeData.EditableProduct = FilteredList[index];
            await _navigationService.Navigate<InventoryEditViewModel>();
        }

        public async void AddProductCommand()
        {
            await _navigationService.Navigate<InventoryEditViewModel>();
        }

        public async Task<bool> DeleteProductCommand(int index)
        {
            _userDialog.ShowLoading(Strings.Loading);
            var response = await AdminService.DeleteProduct(InventoryList[index].Product.ProductId);
            return (response.Result);
        }

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }
    }
}
