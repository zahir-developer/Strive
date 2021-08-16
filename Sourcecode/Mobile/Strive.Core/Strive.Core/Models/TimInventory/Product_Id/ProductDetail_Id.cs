using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory.Product_Id
{            
        public class ProductDetail
    {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public object ProductCode { get; set; }
            public object ProductDescription { get; set; }
            public int ProductType { get; set; }
            public object FileName { get; set; }
            public object OriginalFileName { get; set; }
            public object ThumbFileName { get; set; }
            public int LocationId { get; set; }
            public object Size { get; set; }
            public double Quantity { get; set; }
            public object QuantityDescription { get; set; }
            public double? Cost { get; set; }
            public double Price { get; set; }
            public bool IsTaxable { get; set; }
            public double? TaxAmount { get; set; }
            public bool IsActive { get; set; }
            public object ThresholdLimit { get; set; }
            public string LocationName { get; set; }
            public object VendorName { get; set; }
            public string ProductTypeName { get; set; }
            public object SizeName { get; set; }
            public string Base64 { get; set; }
        }

        public class ProductVendor
        {
            public int ProductVendorId { get; set; }
            public int ProductId { get; set; }
            public int VendorId { get; set; }
            public string VendorName { get; set; }
        }

        public class Product
        {
            public ProductDetail ProductDetail { get; set; }
            public List<ProductVendor> ProductVendor { get; set; }
        }

        public class ProductDetail_Id
        {
            public Product Product { get; set; }
        }   
}
