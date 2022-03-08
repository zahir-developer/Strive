using System;

namespace Strive.BusinessEntities.DTO.PaymentGateway
{
    public class RecurringPaymentDetails
    {
        public string ExpiryDate { get; set; }
        public string ProfileId { get; set; }
        public string AccountId { get; set; }
        public int ClientMembershipId { get; set; }
        public string Address1 { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string Username { get; set; }
        public decimal Amount { get; set; }
        public string Password { get; set; }
        public DateTime? LastPaymentDate { get; set; }
    }
}