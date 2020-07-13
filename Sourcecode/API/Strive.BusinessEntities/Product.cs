using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int? ProductType { get; set; }
        public int? LocationId { get; set; }
        public int? VendorId { get; set; }
        public int? Size { get; set; }
        public string SizeDescription { get; set; }
        public double Quantity { get; set; }
        public string QuantityDescription { get; set; }
        public double Cost { get; set; }
        public bool? IsTaxable { get; set; }
        public double TaxAmount { get; set; }
        public bool? IsActive { get; set; }
        public int? ThresholdLimit { get; set; }

    }
}
