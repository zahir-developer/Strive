using System;
using System.Collections.Generic;

namespace Strive.Core.Models.Customer
{
    public class CardDetailsResponse
    {
        public List<status> Status { get; set; }
    }

    public class status
    {
        
        public string CardNumber { get; set; }
        public string ExpiryDate { get; set; }
        public int Id { get; set; }
        public string CardType { get; set; }
        public int ClientId { get; set; }
        public string VehicleId { get; set; }
        public string MembershipId { get; set; }
    }

}
