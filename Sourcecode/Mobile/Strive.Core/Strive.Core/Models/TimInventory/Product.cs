using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    //public class ProductDetail
    //{
    //    public int ProductId { get; set; }
    //    public string ProductName { get; set; }
    //    public string ProductCode { get; set; }
    //    public string ProductDescription { get; set; }
    //    public int ProductType { get; set; }
    //    public int LocationId { get; set; }
    //    public int? VendorId { get; set; }
    //    public string fileName { get; set; }
    //    public string thumbFileName { get; set; }
    //    public string base64 { get; set; }
    //    public int? Size { get; set; }
    //    public string SizeDescription { get; set; }
    //    public int Quantity { get; set; }
    //    public string QuantityDescription { get; set; }
    //    public double Cost { get; set; }
    //    public bool IsTaxable { get; set; }
    //    public double TaxAmount { get; set; }
    //    public int? ThresholdLimit { get; set; }
    //    public bool IsActive { get; set; }
    //    public object IsDeleted { get; set; }
    //    public object CreatedBy { get; set; }
    //    public object CreatedDate { get; set; }
    //    public object UpdatedBy { get; set; }
    //    public object UpdatedDate { get; set; }
    //    public double? Price { get; set; }
    //}

    public class ProductDetail
    {
        public string? productCode { get; set; }
        public string? productDescription { get; set; }
        public int productType { get; set; }
        public int productId { get; set; }
        public int locationId { get; set; }
        public string productName { get; set; }
        public string? fileName { get; set; }
        public string? OriginalFileName { get; set; }
        public string? thumbFileName { get; set; }
        public string? base64 { get; set; }
        public string cost { get; set; }
        public bool isTaxable { get; set; }
        public int taxAmount { get; set; }
        public int size { get; set; }
        public string? sizeDescription { get; set; }
        public int quantity { get; set; }
        public string? quantityDescription { get; set; }
        public bool isActive { get; set; }
        public string? thresholdLimit { get; set; }
        public bool isDeleted { get; set; }
        public string price { get; set; }      
    }
    public class ProductVendor
    {
        public int productVendorId { get; set; }
        public int productId { get; set; }
        public int vendorId { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
    }
    public class Product
    {
        public ProductDetail product { get; set; }
        public List<ProductVendor> productVendor { get; set; }
    }
    public class AddProduct
    {
        public List<Product> Product { get; set; }
    }


    public class Products
    {
        public List<ProductDetails> ProductSearch { get; set; }
    }

    public class ProductsSearch
    {
        public List<ProductDetail> ProductSearch { get; set; }
    }
}
