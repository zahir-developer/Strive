using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class VehicleViewModel
    {
        public int ClientVehicleId { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public string VehicleNumber { get; set; }
        public int? ColorId { get; set; }
        public int? VehicleMakeId { get; set; }
        public int? VehicleModelId { get; set; }
        public string ModelName { get; set; }
        public string Color { get; set; }
        public string VehicleMake { get; set; }
        public string Upcharge { get; set; }
        public string Barcode { get; set; }
        public string MembershipName { get; set; }
        public int? DocumentId { get; set; }

    }
}
