using System;

namespace Strive.Core.Models.TimInventory
{
    public class InventoryDataModel
    {
        public ProductSearch Product { get; set; }

        public VendorDetail Vendor { get; set; }

        public bool DisplayRequestView { get; set; }
    }
}
