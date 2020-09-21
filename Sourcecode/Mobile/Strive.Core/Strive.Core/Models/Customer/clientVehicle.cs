﻿using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class clientVehicle
    {
        public  int vehicleId { get; set; }
        public  int clientId { get; set; }
        public int? locationId { get; set; } = null;
        public  string vehicleNumber { get; set; }
        public int? vehicleMfr { get; set; } = null;
        public  int? vehicleModel { get; set; } = null;
        public  int? vehicleModelNo { get; set; } = null;
        public  string vehicleYear { get; set; }
        public  int? vehicleColor { get; set; } = null;
        public  int upcharge { get; set; }
        public  string barcode { get; set; }
        public  string notes { get; set; }
        public  bool isActive { get; set; }
        public  bool isDeleted { get; set; }
        public  int createdBy { get; set; }
        public  string createdDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public  int updatedBy { get; set; }
        public  string updatedDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public double monthlyCharge { get; set; }
    }
}
