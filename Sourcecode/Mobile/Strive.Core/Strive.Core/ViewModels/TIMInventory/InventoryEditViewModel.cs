using System;
using System.Threading.Tasks;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Models.TimInventory;
using System.Collections.ObjectModel;
using System.Linq;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class InventoryEditViewModel : BaseViewModel
    {
        private InventoryDataModel EditInventoryItem;

        public ObservableCollection<VendorDetail> VendorList = new ObservableCollection<VendorDetail>();

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
            RaiseAllPropertiesChanged();
        }

        public string ItemCode { get
            {
                return EditInventoryItem != null ? EditInventoryItem.Product.ProductCode : string.Empty; 
            } set { } }
        public string ItemName { get
            {
                return EditInventoryItem != null ? EditInventoryItem.Product.ProductName : string.Empty;
            }
            set { } }
        public string ItemDescription { get
            {
                return EditInventoryItem != null ? EditInventoryItem.Product.ProductDescription : string.Empty;
            }
            set { } }
        public string ItemQuantity { get
            {
                return EditInventoryItem != null ? EditInventoryItem.Product.Quantity.ToString() : string.Empty;
            }
            set { } }
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
            RaiseAllPropertiesChanged();
        }

        public async Task NavigateUploadImageCommand()
        {
            ViewAlpha = 0.5f;
            await _navigationService.Navigate<ChooseImageViewModel>();
        }
    }
}
