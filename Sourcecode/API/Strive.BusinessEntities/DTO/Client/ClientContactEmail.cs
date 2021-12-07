namespace Strive.BusinessEntities.DTO.Client
{
    public class ClientContactEmail
    {
        public int ClientId { get; set; }
        public int ClientAddressId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ClientType { get; set; }
        public bool IsActive { get; set; }
        public bool IsNotified { get; set; }
    }
}
