using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Customer
{
    public class ServiceHistoryModel
    {
        public detailsGrid DetailsGrid { get; set; }
    }

    public class detailsGrid
    {
        public List<jobViewModel> JobViewModel { get; set; }
    }
    public class jobViewModel
    {
        //public int BayId { get; set; }
        //public string BayName { get; set; }
        public int JobId { get; set; }
        public string TicketNumber { get; set; }
        public string TimeIn { get; set; }
        public string ClientName { get; set; }
        public string PhoneNumber { get; set; }
        public string EstimatedTimeOut { get; set; }
        public string ServiceTypeName { get; set; }
        public string VehicleMake { get; set; }
        public string VehicleModel { get; set; }
        public string VehicleColor { get; set; }
        public float Upcharge { get; set; }
        //public string OutsideService { get; set; }
        public string JobDate { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        //public int JobItemId { get; set; }
        public float Cost { get; set; }
        public string Barcode { get; set; }
        public string VehicleId { get; set; }
        public string PaymentDate { get; set; }
        public string JobPaymentId { get; set; }
        public string TipPaymentId { get; set; }
        public string TipAmount { get; set; }
        public bool? IsOpened = false;
    }
}
