using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class PastJobsOfClientViewModel
    {
        public int VehicleId { get; set; }
        public string Barcode { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }

        public string Color { get; set; }
        public DateTime DetailVisitDate { get; set; }
        public string ServiceName { get; set; }
        public string DetailOrAdditionalService { get; set; }
        public decimal Cost { get; set; }
        public string WashOrDetailJobType { get; set; }
    }
}
