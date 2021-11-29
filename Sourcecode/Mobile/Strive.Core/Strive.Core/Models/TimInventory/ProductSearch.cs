using System;
namespace Strive.Core.Models.TimInventory
{
    public class ProductSearch
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductTypeName { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductDescription { get; set; }
        public int LocationId { get; set; }
        public string? FileName { get; set; }
        public string? OriginalFileName { get; set; }
        public string? ThumbFileName { get; set; }
        public string? SizeName { get; set; }
        public double? Quantity { get; set; }
        public double? Cost { get; set; }
        public double? Price { get; set; }
        public bool IsActive { get; set; }
        public string LocationName { get; set; }
        public string? VendorName { get; set; }
        //public string VendorId { get; set; }
        //public string VendorPhone { get; set; }
        public string? Base64 { get; set; }
    }
}
