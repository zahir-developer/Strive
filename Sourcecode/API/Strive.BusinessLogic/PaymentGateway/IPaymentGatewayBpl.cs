using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.DTO.PaymentGateway;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.PaymentGateway
{
    public interface IPaymentGatewayBpl
    {
        Result VoidTrasaction(CreditCardDto cardDto);

        JObject AuthTransaction(CardPaymentDto paymentDto);

        JObject CaptureTransaction(CaptureDetail captureDetail);
        Result DeletePaymentGateway(int id);
        JObject CreateUpdateProfile(CardPaymentDto cardPaymentDto);// string UserName, string Password, string url, string MID);
        //List<MerchantDetails> GetMerchantDetails(int LocationId);
    }
}
