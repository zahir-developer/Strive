using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.Client
{
    public class ClientVehicle
    {
        public int VehicleId { get; set; }
        public int ClientId { get; set; }
        public int LocationId { get; set; }
        public string VehicleNumber { get; set; }
        public int VehicleMake { get; set; }
        public int VehicleModel { get; set; }
        public int VehicleModelNo { get; set; }
        public string VehicleYear { get; set; }
        public int VehicleColor { get; set; }
        public int Upcharge { get; set; }
        public string Barcode { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
