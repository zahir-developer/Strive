namespace Strive.BusinessEntities.DTO.PaymentGateway
{
    public class PaymentDetail
    {
        public string Account { get; set; }
        public string Expiry { get; set; }
        public string Amount { get; set; }
        public string OrderId { get; set; }
        public string Batchid { get; set; }
        public string Currency { get; set; }
        public string Receipt { get; set; }
        public string CCV { get; set; }
    }
}