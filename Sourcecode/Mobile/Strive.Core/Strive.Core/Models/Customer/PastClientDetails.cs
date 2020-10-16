using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class PastClientDetails
    {   
        public int VehicleId { get; set; }
        public string Barcode { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string DetailVisitDate { get; set; }
        public string ServiceName { get; set; }
        public string DetailOrAdditionalService { get; set; }
        public string Cost { get; set; }
        public string WashOrDetailJobType { get; set; }
    }
    public class PastClientServices
    {
        public List<PastClientDetails> PastClientDetails { get; set; }
    }
}
