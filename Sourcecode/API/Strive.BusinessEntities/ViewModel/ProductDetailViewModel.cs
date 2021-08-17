using Cocoon.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductDescription { get; set; }
        public int ProductType { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string ThumbFileName { get; set; }
        public int LocationId { get; set; }
        public int? Size { get; set; }
        public decimal? Quantity { get; set; }
        public string QuantityDescription { get; set; }
        public decimal? Cost { get; set; }
        public decimal Price { get; set; }
        public bool? IsTaxable { get; set; }
        public decimal? TaxAmount { get; set; }
        public bool IsActive { get; set; }
        public decimal? ThresholdLimit { get; set; }
        public string LocationName { get; set; }
        public string VendorName { get; set; }
        public string ProductTypeName { get; set; }
        public string SizeName { get; set; }
        public string Base64 { get; set; }
    }
}
