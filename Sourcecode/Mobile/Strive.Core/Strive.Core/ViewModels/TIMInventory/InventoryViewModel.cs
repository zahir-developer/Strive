using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;
using Strive.Core.Resources;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class InventoryViewModel : BaseViewModel
    {
        public InventoryViewModel()
        {
        }

        public ObservableCollection<ProductDetail> FilteredList { get; set; } = new ObservableCollection<ProductDetail>();

        private ObservableCollection<ProductDetail> _InventoryItemList = new ObservableCollection<ProductDetail>();

        private ObservableCollection<ProductDetail> EditableList = new ObservableCollection<ProductDetail>();

        public async Task GetProductsCommand()
        {
            _userDialog.ShowLoading(Strings.Loading);
            Products products = await AdminService.GetAllProducts();
            foreach(var product in products.Product)
            {
                _InventoryItemList.Add(product);
            }
            _userDialog.HideLoading();
            await RaiseAllPropertiesChanged();
        }

        public void InventorySearchCommand(string SearchText)
        {
            FilteredList = new ObservableCollection<ProductDetail>(_InventoryItemList.
                Where(s => s.ProductName.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())));
            EditableList = FilteredList;
            RaiseAllPropertiesChanged();
        }

        public void IncrementCommand(int index)
        {
            FilteredList[index].Quantity++;
            RaiseAllPropertiesChanged();
        }

        public void DecrementCommand(int index)
        {
            FilteredList[index].Quantity--;
            RaiseAllPropertiesChanged();
        }

        public void ClearCommand()
        {
            FilteredList.Clear();
            _InventoryItemList.Clear();
        }
    }
}
