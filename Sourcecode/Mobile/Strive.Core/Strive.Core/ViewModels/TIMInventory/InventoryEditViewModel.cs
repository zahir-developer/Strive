using System;
using System.Threading.Tasks;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Models.TimInventory;
using System.Collections.ObjectModel;
using System.Linq;
using Strive.Core.Utils;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class InventoryEditViewModel : BaseViewModel
    {
        private InventoryDataModel EditInventoryItem;

        public ObservableCollection<VendorDetail> VendorList = new ObservableCollection<VendorDetail>();

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
                PickerSelectionCommand(EditInventoryItem.Vendor);
            }
            if (EditInventoryItem != null)
            {
                SetProductCommand(EditInventoryItem.Product);
            }
            RaiseAllPropertiesChanged();
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
            set { SetProperty(ref _SelectedItemQuantity, value); }
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

        private ObservableCollection<string> _VendorNames = new ObservableCollection<string>();
        public virtual ObservableCollection<string> VendorNames
        {
            get { return _VendorNames; }
            set { SetProperty(ref _VendorNames, value); }
        }

        public async Task NavigateBackCommand()
        {
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

        public void SetProductCommand(ProductDetail SelectedProduct)
        {
            ItemCode = SelectedProduct.ProductCode;
            ItemName = SelectedProduct.ProductName;
            ItemDescription = SelectedProduct.ProductDescription;
            ItemQuantity = SelectedProduct.Quantity.ToString();
        }

        public async Task AddProductCommand()
        {
            var product = PrepareAddProduct();
            var result = await AdminService.AddProduct(product);
            if(result.Status == "true")
            {
                NavigateBackCommand();
            }
        }

        private ProductDetail PrepareAddProduct()
        {
            ProductDetail AddProduct = new ProductDetail()
            {
                ProductCode = _SelectedItemCode,
                ProductName = _SelectedItemName,
                ProductDescription = _SelectedItemDescription,
                ProductType = 1,
                LocationId = 1,
                VendorId = CurrentVendor.VendorId,
                Size = 1,
                SizeDescription = "",
                Quantity = int.Parse(_SelectedItemQuantity),
                QuantityDescription = "",
                Cost = 10,
                IsTaxable = true,
                TaxAmount = 10,
                ThresholdLimit = 5,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = 0,
                CreatedDate = DateUtils.GetTodayDateString(),
                UpdatedBy = 0,
                UpdatedDate = DateUtils.GetTodayDateString(),
                Price = 20
            };
            return AddProduct;
        }

        public async Task NavigateUploadImageCommand()
        {
            ViewAlpha = 0.5f;
            await _navigationService.Navigate<ChooseImageViewModel>();
        }
    }
}
