using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Resources;

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

        public void InventorySearchCommand(string SearchText)
        {
            FilteredList = new ObservableCollection<InventoryDataModel>(InventoryList.
                Where(s => s.Product.ProductName.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())));
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
            FilteredList[index].Product.Quantity--;
            RaiseAllPropertiesChanged();
        }

        public void ClearCommand()
        {
            FilteredList.Clear();
            InventoryList.Clear();
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
    }
}
