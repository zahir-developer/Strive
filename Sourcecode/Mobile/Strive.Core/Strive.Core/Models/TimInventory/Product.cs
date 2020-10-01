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
        public string FileName { get; set; }
        public string ThumbFileName { get; set; }
        public int LocationId { get; set; }
        public int VendorId { get; set; }
        public int Size { get; set; }
        public int Quantity { get; set; }
        public string QuantityDescription { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }
        public bool IsTaxable { get; set; }
        public double TaxAmount { get; set; }
        public bool IsActive { get; set; }
        public int ThresholdLimit { get; set; }
        public string LocationName { get; set; }
        public string VendorName { get; set; }
        public string ProductTypeName { get; set; }
        public string SizeName { get; set; }
        public string Base64 { get; set; }
    }

    public class Products
    {
        public List<ProductDetail> Product { get; set; }
    }

    public class ProductsSearch
    {
        public List<ProductDetail> ProductSearch { get; set; }
    }
}
