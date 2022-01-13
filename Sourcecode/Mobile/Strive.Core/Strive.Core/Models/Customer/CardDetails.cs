using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Customer
{

    public class AddCardRequest
    {
        public client client { get; set; }
        public List<clientVehicle> clientVehicle { get; set; }
        public List<clientAddress> clientAddress { get; set; }
        public CreditAccountHistory CreditAccountHistory { get; set; }
        public string token { get; set; }
        public string password { get; set; }
        public List<CardDetails> ClientCardDetails { get; set; }
    }
    public class CardDetails
    {
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public int Id { get; set; }
        public string CardType { get; set; }
        public int ClientId { get; set; }
        public string VehicleId { get; set; }
        public string MembershipId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

    }

    public class CardLists
    {
        public List<CardDetails> Cards { get; set; }
    }

    public class CreditAccountHistory
    {
        public string creditAccountHistory { get; set; }
    }
    
}
