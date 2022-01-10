using System;

namespace Strive.BusinessEntities.ViewModel
{
    public class ClientCardDetailViewModel
    {
        public int Id { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int ClientId { get; set; }
        public int VehicleId { get; set; }
        public int MembershipId { get; set; }
    }
}
