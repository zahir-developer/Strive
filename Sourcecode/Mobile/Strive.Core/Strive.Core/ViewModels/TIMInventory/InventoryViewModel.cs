using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Strive.Core.ViewModels.TIMInventory
{
    public class InventoryViewModel : BaseViewModel
    {
        public InventoryViewModel()
        {
            PopulateData();
            InventorySearchCommand("");
        }

        public ObservableCollection<string> FilteredList { get; private set; } = new ObservableCollection<string>();

        private ObservableCollection<string> _InventoryItemList = new ObservableCollection<string>();

        public ObservableCollection<string> InventoryItemList
        {
            get => PopulateData();
            protected set { }
        }

        private ObservableCollection<string> PopulateData()
        {
            _InventoryItemList.Add("Men's Western Tan");
            _InventoryItemList.Add("Premium wash");
            _InventoryItemList.Add("Milk - 100 ml");
            _InventoryItemList.Add("Synthetic sealant");
            _InventoryItemList.Add("Water Bottle");
            _InventoryItemList.Add("car shampoo");
            _InventoryItemList.Add("Men's Western Black");
            _InventoryItemList.Add("Cleaning Cap");
            _InventoryItemList.Add("Brush");
            _InventoryItemList.Add("Premium shoes");
            _InventoryItemList.Add("Car wash foam");
            return _InventoryItemList;
        }

        public void InventorySearchCommand(string SearchText)
        {
            FilteredList = new ObservableCollection<string>(_InventoryItemList.
                Where(s => s.ToLowerInvariant().Contains(SearchText.ToLowerInvariant())));
            RaiseAllPropertiesChanged();
        }

    }
}
