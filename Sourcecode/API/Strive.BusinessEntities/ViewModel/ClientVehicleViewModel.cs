using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientVehicleViewModel
    {
        public int ClientId { get; set; }
        public int? LocationId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public int VehicleId { get; set; }
        public string VehicleNumber { get; set; }
        public int VehicleMfr { get; set; }
        public int VehicleModelId { get; set; }
        public string VehicleModel { get; set; }
        public int VehicleColor { get; set; }
        public int VehicleModelNo { get; set; }
        public string VehicleYear { get; set; }
        public int? Upcharge { get; set; }
        public string Barcode { get; set; }
        public string Notes { get; set; }
        public bool? IsActive { get; set; }


    }
}
