using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerVehicleDetails
    {
        public int ClientVehicleId { get; set; }
        public int ClientId { get; set; }
        public int? LocationId { get; set; }
        public string VehicleNumber { get; set; }
        public int VehicleMakeId { get; set; }
        public string VehicleMake { get; set; }
        public int? VehicleModelId { get; set; }
        public int? VehicleModelNo { get; set; }
        public string VehicleYear { get; set; }
        public int? VehicleColor { get; set; }
        public int? Upcharge { get; set; }
        public double? MonthlyCharge { get; set; }
        public string Barcode { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string ModelName { get; set; }
        public string Color { get; set; }
        public int ColorId { get; set; }
    }
    public class CustomerCompleteDetails
    {
        public CustomerVehicleDetails Status { get; set; }
    }
}
