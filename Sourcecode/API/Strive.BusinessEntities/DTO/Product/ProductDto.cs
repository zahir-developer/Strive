using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities.ViewModel.Product
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int? ProductType { get; set; }
        public string FileName { get; set; }
        public string Base64 { get; set; }
        public int? LocationId { get; set; }
        public int? VendorId { get; set; }
        public int? Size { get; set; }
        public string SizeDescription { get; set; }
        public short? Quantity { get; set; }
        public string QuantityDescription { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public bool? IsTaxable { get; set; }
        public decimal? TaxAmount { get; set; }
        public int? ThresholdLimit { get; set; }
        public bool? IsActive { get; set; }

    }
}
