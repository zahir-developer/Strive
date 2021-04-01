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

        Result authTransactionWithUserFields(PaymentDto paymentDto);

    }
}
