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

        public ObservableCollection<ProductSearch> ProductList = new ObservableCollection<ProductSearch>();

        private ObservableCollection<VendorDetail> VendorList = new ObservableCollection<VendorDetail>();

        private ObservableCollection<InventoryDataModel> InventoryList = new ObservableCollection<InventoryDataModel>();

        private ObservableCollection<InventoryDataModel> EditableList = new ObservableCollection<InventoryDataModel>();


        public async Task GetProductsCommand()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var SearchText = getSearchText(" ");
            Products products = await AdminService.GetAllProducts(SearchText);
            foreach(var product in products.ProductSearch)
            {
                ProductList.Add(product);
               
                var vendor = VendorList.Where(s => s.VendorName == product.VendorName).FirstOrDefault();
                if (vendor != null)
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

        public async Task InventorySearchCommand(string searchedText)
        {
            _userDialog.ShowLoading(Strings.Loading);
            var SearchText = getSearchText(searchedText);   
            var searchList = await AdminService.GetAllProducts(SearchText);
            ClearCommand();
            foreach (var product in searchList.ProductSearch)
            {
                ProductList.Add(product);
                
                var vendor = VendorList.Where(s => s.VendorName == product.VendorName).FirstOrDefault();
                if (vendor != null)
                {
                    InventoryList.Add(new InventoryDataModel() { Product = product, Vendor = vendor });
                }
            }
            //FilteredList = new ObservableCollection<InventoryDataModel>(InventoryList.
            //    Where(s => s.Product.ProductName.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())));
            FilteredList = InventoryList;
            EditableList = FilteredList;
            await RaiseAllPropertiesChanged();
        }
               
        private ProductSearches getSearchText(string searchedText)
        {
            ProductSearches searchText = new ProductSearches()
            {
                productSearch = searchedText,
                status = true,
                loadThumbnailImage = true
            };
            return searchText;
        }

        public void IncrementCommand(int index)
        {
            FilteredList[index].Product.Quantity++;
            UpdateProdQuantityCommand(index);
            RaiseAllPropertiesChanged();
        }

        public void DecrementCommand(int index)
        {
            if (!(FilteredList[index].Product.Quantity > 0))
                return;
            FilteredList[index].Product.Quantity--;
            UpdateProdQuantityCommand(index);
            RaiseAllPropertiesChanged();
        }
                
        public async void UpdateProdQuantityCommand(int index)
        {
            _userDialog.ShowLoading(Strings.Loading);
            await AdminService.UpdateProdQuantity(FilteredList[index].Product.ProductId, int.Parse(FilteredList[index].Product.Quantity.ToString()));
            
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

        public async void ProductRequestCommand(int quantity,int index)
        {
            _userDialog.ShowLoading(Strings.Loading);
            var request = FilteredList[index];

            ProductRequest requestedProduct = new ProductRequest()
            {
                locationId = request.Product.LocationId,
                locationName = request.Product.LocationName,
                productId = request.Product.ProductId,
                productName = request.Product.ProductName,
                requestQuantity = quantity
            };
            await AdminService.ProductRequest(requestedProduct);
            _userDialog.HideLoading();
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
