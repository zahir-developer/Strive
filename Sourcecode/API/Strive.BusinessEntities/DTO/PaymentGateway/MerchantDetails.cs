namespace Strive.BusinessEntities.DTO.PaymentGateway
{
    public class MerchantDetails
    {
        public string UserName { get; set; }
        public string MID { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
		public int MerchantDetailId { get; set; }
		public int LocationId { get; set; }
    }
}