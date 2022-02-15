using System;
using System.Threading.Tasks;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Models.TimInventory;
using System.Collections.ObjectModel;
using System.Linq;
using Strive.Core.Utils;
using Strive.Core.Resources;
using System.Collections.Generic;
using EditProduct = Strive.Core.Models.TimInventory.Product_Id;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class InventoryEditViewModel : BaseViewModel
    {
        private InventoryDataModel  EditInventoryItem;

        public ObservableCollection<VendorDetail> VendorList = new ObservableCollection<VendorDetail>();
        public ObservableCollection<LocationDetail> LocationList = new ObservableCollection<LocationDetail>();
        public ObservableCollection<Code> ProductTypeList = new ObservableCollection<Code>();
        public EditProduct.ProductDetail_Id editableProduct = new EditProduct.ProductDetail_Id();

        private VendorDetail CurrentVendor = new VendorDetail();

        public InventoryEditViewModel()
        {
            EditInventoryItem = EmployeeData.EditableProduct;
            if(EmployeeData.Vendors != null)
            {
                foreach(var vendor in EmployeeData.Vendors.Vendor)
                {
                    VendorList.Add(vendor);
                    VendorNames.Add(vendor.VendorName);
                }
            }
            if(EditInventoryItem != null)
            {
                productByIdCommand();
                PickerSelectionCommand(EditInventoryItem.Vendor);
            }
            if (EditInventoryItem != null)
            {
                SetProductCommand(EditInventoryItem.Product);
                ItemLocation = EditInventoryItem.Product.LocationName;
                ItemType = EditInventoryItem.Product.ProductTypeName;
                if(EditInventoryItem.Product.Base64 != null)
                {
                    Base64String = EditInventoryItem.Product.Base64;
                }
            }           
            RaiseAllPropertiesChanged();
        }

        public async Task GetAllLocNameCommand()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var locations = await AdminService.GetAllLocationName();

            foreach(var location in locations.Location)
            {
                LocationList.Add(location);
                LocationNames.Add(location.LocationName);
            }
            _userDialog.HideLoading();
            await RaiseAllPropertiesChanged();
        }
        public async Task GetProductTypeCommand()
        {
            _userDialog.ShowLoading(Strings.Loading);
            var Typelist = await AdminService.GetProductType();

            foreach(var item in Typelist.Codes)
            {
                ProductTypeList.Add(item);
            }
            _userDialog.HideLoading();
            await RaiseAllPropertiesChanged();
        }
        public string ItemCode { get
            {
                return _SelectedItemCode;
            }
            set { SetProperty(ref _SelectedItemCode, value); }
        }
        public string ItemName { get
            {
                return _SelectedItemName;
            }
            set { SetProperty(ref _SelectedItemName, value); }
        }
        public string ItemDescription { get
            {
                return _SelectedItemDescription;
            }
            set { SetProperty(ref _SelectedItemDescription, value); }
        }
        public string ItemQuantity { get
            {
                return _SelectedItemQuantity;
            }
            set {
                int number = 0;
                bool isNumeric = int.TryParse(value, out number);
                if(((isNumeric) && number > 0) || value == "")
                {
                    SetProperty(ref _SelectedItemQuantity, value);
                }
                RaiseAllPropertiesChanged();
            }
        }
        public string ItemType { get
            {
                return _SelectedItemType;
            }
            set { SetProperty(ref _SelectedItemType, value); }
        }
        public int ItemTypeId {
            get
            {
                return _SelectedItemTypeId;
            }
            set { SetProperty(ref _SelectedItemTypeId, value); }
        }
        public string ItemLocation { get
            {
                return _SelectedItemLocation;
            }
            set { SetProperty(ref _SelectedItemLocation, value); }
        }
         public int ItemLocationId { get
            {
                return _SelectedItemLocationId;
            }
            set { SetProperty(ref _SelectedItemLocationId, value); }
        }
        public string ItemCost { get
            {
                return _SelectedItemCost;
            }
            set {
                double number = 0;
                bool isNumeric = double.TryParse(value, out number);
                if (((isNumeric) && number > 0) || value == "")
                {
                    SetProperty(ref _SelectedItemCost, value);
                }
                RaiseAllPropertiesChanged();
            }                   
        }
        public string ItemPrice { get
            {
                return _SelectedItemPrice;
            }
            set {
                double number = 0;
                bool isNumeric = double.TryParse(value, out number);
                if (((isNumeric) && number > 0) || value == "")
                {
                    SetProperty(ref _SelectedItemPrice, value);
                }
                RaiseAllPropertiesChanged();
            }
        }
        public string SupplierName { get
            {
                return _SelectedSupplierName;
            }
            set { SetProperty(ref _SelectedSupplierName, value); } }

        public string SupplierContact { get
            {
                return _SelectedSupplierContact;
            } set { SetProperty(ref _SelectedSupplierContact, value); }
        }

        public string SupplierFax { get
            {
                return _SelectedSupplierFax;
            } set { SetProperty(ref _SelectedSupplierFax, value); }
        }

        public string SupplierAddress { get
            {
                return _SelectedSupplierAddress;
            }
            set { SetProperty(ref _SelectedSupplierAddress, value); }
        }

        public string SupplierEmail { get
            {
                return _SelectedSupplierEmail;
            } set { SetProperty(ref _SelectedSupplierEmail, value); }
        }

        private float _ViewAlpha = 1f;

        public float ViewAlpha
        {
            get
            {
                return _ViewAlpha;
            }
            set
            {
                SetProperty(ref _ViewAlpha, value);
            }
        }

        private string _SelectedSupplierName;
        private string _SelectedSupplierContact;
        private string _SelectedSupplierEmail;
        private string _SelectedSupplierFax;
        private string _SelectedSupplierAddress;

        private string _SelectedItemName;
        private string _SelectedItemCode;
        private string _SelectedItemDescription;
        private string _SelectedItemQuantity;
        private string _SelectedItemType;
        private int _SelectedItemTypeId;
        private string _SelectedItemLocation;
        private int _SelectedItemLocationId;
        private string _SelectedItemCost;
        private string _SelectedItemPrice;

        public string Base64String { get; set; }
        public string Filename { get; set; }

        private ObservableCollection<string> _VendorNames = new ObservableCollection<string>();
        public virtual ObservableCollection<string> VendorNames
        {
            get { return _VendorNames; }
            set { SetProperty(ref _VendorNames, value); }
        }

        private ObservableCollection<string> _LocationNames = new ObservableCollection<string>();
        public virtual ObservableCollection<string> LocationNames
        {
            get { return _LocationNames; }
            set { SetProperty(ref _LocationNames, value); }
        }

        public async Task NavigateBackCommand()
        {
            EmployeeData.EditableProduct = null;
            await _navigationService.Close(this);
        }

        public void PickerSelectionCommand(VendorDetail SelectedVendor)
        {
            SupplierName = SelectedVendor.VendorName;
            SupplierContact = SelectedVendor.PhoneNumber;
            SupplierEmail = SelectedVendor.Email;
            SupplierFax = SelectedVendor.Fax;
            SupplierAddress = SelectedVendor.Address1;
            CurrentVendor = SelectedVendor;
        }

        public void SetProductCommand(ProductSearch SelectedProduct)
        {
            ItemCode = SelectedProduct.ProductCode;
            ItemName = SelectedProduct.ProductName;
            ItemDescription = SelectedProduct.ProductDescription;
            ItemQuantity = SelectedProduct.Quantity.ToString();
            ItemCost = SelectedProduct.Cost.ToString();
            ItemPrice = SelectedProduct.Price.ToString();
        }

        public void setLocationCommand(LocationDetail selectedLocation)
        {
            ItemLocation = selectedLocation.LocationName;
            ItemLocationId = selectedLocation.LocationId;
        }

        public void setProdTypeCommand(Code selectedType)
        {
            ItemType = selectedType.CodeValue;
            ItemTypeId = selectedType.CodeId;
        }

        public async Task AddProductCommand()
        {
            if (ValidateCommand() == false)
            {
                return;
            }
            else if (ItemTypeId == 0)
            {
                var productType = ProductTypeList.Where(l => l.CodeValue == EditInventoryItem.Product.ProductTypeName).FirstOrDefault();
                ItemTypeId = productType.CodeId;
                //return false;
            }
            else if(ItemLocationId == 0)
            {
                var location = LocationList.Where(l => l.LocationName == EditInventoryItem.Product.LocationName).FirstOrDefault();
                ItemLocationId = location.LocationId;
                //return false;
            }
            var product = PrepareAddProduct(); 
            _userDialog.ShowLoading(Strings.Loading);
            var result = await AdminService.AddProduct(product);
            if(result.Status == "true")
            {
                NavigateBackCommand();
            }
            _userDialog.HideLoading();
        }

        bool ValidateCommand()
        {
            if(string.IsNullOrEmpty(ItemName))
            {
                _userDialog.AlertAsync("Please enter Item name");
                return false;
            }
            else if (string.IsNullOrEmpty(ItemQuantity))
            {
                _userDialog.AlertAsync("Please enter Item quantity");
                return false;
            }
            else if (int.Parse(ItemQuantity) < 0)
            {
                _userDialog.AlertAsync("Quantity cannot be a negative value");
                return false;
            }
            else if (string.IsNullOrEmpty(ItemType))
            {
                _userDialog.AlertAsync("Please enter product Type");
                return false;
            }
            //else if(ItemTypeId == 0)
            //{
            //    var productType = ProductTypeList.Where(l => l.CodeValue == EditInventoryItem.Product.ProductTypeName).FirstOrDefault();
            //    ItemTypeId = productType.CodeId;
            //    return false;
            //}
            else if (string.IsNullOrEmpty(ItemLocation) || (ItemLocation == ""))
            {
                _userDialog.AlertAsync("Please enter location");
                return false;
            }
            //else if(ItemLocationId == 0)
            //{
            //    var location = LocationList.Where(l => l.LocationName == EditInventoryItem.Product.LocationName).FirstOrDefault();
            //    ItemLocationId = location.LocationId;
            //    return false;
            //}
            else if (string.IsNullOrEmpty(ItemCost))
            {
                _userDialog.AlertAsync("Please enter Item cost");
                return false;
            }
            else if (string.IsNullOrEmpty(ItemPrice))
            {
                _userDialog.AlertAsync("Please enter Item Price");
                return false;
            }
            else if (string.IsNullOrEmpty(SupplierName))
            {
                _userDialog.AlertAsync("Please enter Supplier information");
                return false;
            }
            return true;
        }

        private AddProduct PrepareAddProduct()
        {           
            ProductDetail newProd = new ProductDetail()
            {
                productCode = _SelectedItemCode,
                productDescription = _SelectedItemDescription,
                productType = _SelectedItemTypeId,
                productId = 0,
                locationId = _SelectedItemLocationId,
                productName = _SelectedItemName,
                base64 = Base64String,
                fileName = Filename,
                OriginalFileName = Filename,
                thumbFileName = "png",
                cost = _SelectedItemCost,
                isTaxable = false,
                taxAmount = 0,
                size = 23,
                quantity = int.Parse(_SelectedItemQuantity),
                isActive = true,
                thresholdLimit = "",
                isDeleted = false,
                price = _SelectedItemPrice
            };
            ProductVendor newVendor = new ProductVendor()
            {
                productVendorId = 0,
                productId = 0,
                vendorId = CurrentVendor.VendorId,
                isActive = true,
                isDeleted = false
            };
            List<ProductVendor> vendorList = new List<ProductVendor>();
            vendorList.Add(newVendor);
            Product productSet = new Product()
            {
                product = newProd,
                productVendor = vendorList
            };

            List<Product> productList = new List<Product>();
            productList.Add(productSet);

            AddProduct newProduct = new AddProduct()
            {
                Product = productList
            };
            return newProduct;
        }

        public void AddorUpdateCommand()
        {
            if (EmployeeData.EditableProduct != null)
            {
                UpdateProductCommand();
                return;
            }
            AddProductCommand();
        }

        public async void UpdateProductCommand()
        {
            if (ValidateCommand() == false)
            {
                return;
            }
            else if (ItemTypeId == 0)
            {
                var productType = ProductTypeList.Where(l => l.CodeValue == EditInventoryItem.Product.ProductTypeName).FirstOrDefault();
                ItemTypeId = productType.CodeId;
                //return false;
            }
            if (ItemLocationId == 0)
            {
                var location = LocationList.Where(l => l.LocationName == EditInventoryItem.Product.LocationName).FirstOrDefault();
                ItemLocationId = location.LocationId;
                //return false;
            }
            if (Filename == null)
            {
                System.Random random = new System.Random();
                Filename = "Image" + random.Next().ToString() + ".png";
            }
            var updateProduct = PrepareUpdateProduct();
            var response = await AdminService.UpdateProduct(updateProduct);
            if(response.Status != "true")
            {
               await _userDialog.AlertAsync("Something unusal has happened.");
                return;
            }
            NavigateBackCommand();
        }

        public async void productByIdCommand()
        {
            editableProduct = await AdminService.GetProductByID(EditInventoryItem.Product.ProductId);
        }
        private AddProduct PrepareUpdateProduct()
        {         
            ProductDetail newProd = new ProductDetail()
            {
                productCode = _SelectedItemCode,
                productDescription = _SelectedItemDescription,
                productType = _SelectedItemTypeId,
                productId = editableProduct.Product.ProductDetail.ProductId,
                locationId = _SelectedItemLocationId,
                productName = _SelectedItemName,
                base64 = Base64String,
                fileName = Filename,
                OriginalFileName = Filename,
                thumbFileName = "png",
                cost = _SelectedItemCost,
                isTaxable = false,
                taxAmount = 0,
                size = 23,
                quantity = int.Parse(_SelectedItemQuantity),
                isActive = true,
                thresholdLimit = "",
                isDeleted = false,
                price = _SelectedItemPrice
            };
            ProductVendor newVendor = new ProductVendor()
            {
                productVendorId = editableProduct.Product.ProductVendor.FirstOrDefault().ProductVendorId,
                productId = editableProduct.Product.ProductDetail.ProductId,
                vendorId = CurrentVendor.VendorId,
                isActive = true,
                isDeleted = false
            };
            List<ProductVendor> vendorList = new List<ProductVendor>();
            vendorList.Add(newVendor);
            Product productSet = new Product()
            {
                product = newProd,
                productVendor = vendorList
            };

            List<Product> productList = new List<Product>();
            productList.Add(productSet);

            AddProduct newProduct = new AddProduct()
            {
                Product = productList
            };
            return newProduct;
        }

        public async Task NavigateUploadImageCommand()
        {
            ViewAlpha = 0.5f;
            await _navigationService.Navigate<ChooseImageViewModel>();
        }

        public async Task LogoutCommand()
        {
            await _navigationService.Close(this);
            _mvxMessenger.Publish<ValuesChangedMessage>(new ValuesChangedMessage(this, 1, "boo!"));
        }
    }
}
