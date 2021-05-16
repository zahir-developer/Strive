using System;

namespace Strive.Core.Models.TimInventory
{
    public class InventoryDataModel
    {
        public ProductDetails Product { get; set; }

        public VendorDetail Vendor { get; set; }

        public bool DisplayRequestView { get; set; }
    }
}
