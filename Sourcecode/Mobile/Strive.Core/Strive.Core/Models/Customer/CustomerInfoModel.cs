using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public static class client
    {
        public static int clientId { get; set; }
        public static string firstName { get; set; }
        public static string middleName { get; set; }
        public static string lastName { get; set; }
        public static int gender { get; set; }
        public static int maritalStatus { get; set; }
        public static string birthDate { get; set; }
        public static string notes { get; set; }
        public static string recNotes { get; set; }
        public static int score { get; set; }
        public static bool noEmail { get; set; }
        public static int clientType { get; set; }
        public static bool isActive { get; set; }
        public static bool isDeleted { get; set; }
        public static int createdBy { get; set; }
        public static string createdDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public static int updatedBy { get; set; }
        public static string updatedDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
    }
    public static class clientVehicle
    {
        public static int vehicleId { get; set; }
        public static int clientId { get; set; }
        public static int locationId { get; set; }
        public static string vehicleNumber { get; set; }
        public static int vehicleMfr { get; set; }
        public static int vehicleModel { get; set; }
        public static int vehicleModelNo { get; set; }
        public static string vehicleYear { get; set; }
        public static int vehicleColor { get; set; }
        public static int upcharge { get; set; }
        public static string barcode { get; set; }
        public static string notes { get; set; }
        public static bool isActive { get; set; }
        public static bool isDeleted { get; set; }
        public static int createdBy { get; set; }
        public static string createdDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public static int updatedBy { get; set; }
        public static string updatedDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
    }
    public static class clientAddress
    {
        public static int clientAddressId { get; set; }
        public static int clientId { get; set; }
        public static string address1 { get; set; }
        public static string address2 { get; set; }
        public static string phoneNumber { get; set; }
        public static string phoneNumber2 { get; set; }
        public static string email { get; set; }
        public static int city { get; set; }
        public static int state { get; set; }
        public static int country { get; set; }
        public static string zip { get; set; }
        public static bool isActive { get; set; }
        public static bool isDeleted { get; set; }
        public static int createdBy { get; set;}
        public static string createdDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public static int updatedBy { get; set; }
        public static string updatedDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
    }
}
