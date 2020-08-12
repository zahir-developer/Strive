using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int ProductType { get; set; }
        public int LocationId { get; set; }
        public int? VendorId { get; set; }
        public int? Size { get; set; }
        public string SizeDescription { get; set; }
        public int Quantity { get; set; }
        public string QuantityDescription { get; set; }
        public double Cost { get; set; }
        public bool IsTaxable { get; set; }
        public double TaxAmount { get; set; }
        public int? ThresholdLimit { get; set; }
        public bool IsActive { get; set; }
        public object IsDeleted { get; set; }
        public object CreatedBy { get; set; }
        public object CreatedDate { get; set; }
        public object UpdatedBy { get; set; }
        public object UpdatedDate { get; set; }
        public double? Price { get; set; }
        public bool DisplayRequestView { get; set; }
    }

    public class Products
    {
        public List<ProductDetail> Product { get; set; }
    }
}
