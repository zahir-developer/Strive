using System;
using System.Threading.Tasks;
using Strive.Core.Utils.TimInventory;
using Strive.Core.Models.TimInventory;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class InventoryEditViewModel : BaseViewModel
    {
        private ProductDetail EditProduct;
        public InventoryEditViewModel()
        {
            EditProduct = EmployeeData.EditableProduct;
        }

        public string ItemCode { get
            {
                return EditProduct != null ? EditProduct.ProductCode : string.Empty; 
            } set { } }
        public string ItemName { get
            {
                return EditProduct != null ? EditProduct.ProductName : string.Empty;
            }
            set { } }
        public string ItemDescription { get
            {
                return EditProduct != null ? EditProduct.ProductDescription : string.Empty;
            }
            set { } }
        public string ItemQuantity { get
            {
                return EditProduct != null ? EditProduct.Quantity.ToString() : string.Empty;
            }
            set { } }
        public string SupplierName { get; set; }
        public string SupplierContact { get; set; }
        public string SupplierFax { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierEmail { get; set; }

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

        public async Task NavigateBackCommand()
        {
            await _navigationService.Close(this);
        }

        public async Task NavigateUploadImageCommand()
        {
            ViewAlpha = 0.5f;
            await _navigationService.Navigate<ChooseImageViewModel>();
        }
    }
}
