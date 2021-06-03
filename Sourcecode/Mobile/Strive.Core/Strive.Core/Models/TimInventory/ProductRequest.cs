using System;
namespace Strive.Core.Models.TimInventory
{
    public class ProductRequest
    {
        public int locationId { get; set; }

        public string locationName { get; set; }

        public int productId { get; set; }

        public string productName { get; set; }

        public double requestQuantity { get; set; }
    }
}
