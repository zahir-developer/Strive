using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.PaymentGateway;
using Strive.BusinessLogic.PaymentGateway;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Route("Payroll/[controller]")]
    [ApiController]
    public class PaymentGatewayController : StriveControllerBase<IPaymentGatewayBpl>
    {

        public PaymentGatewayController(IPaymentGatewayBpl paymentGatewayBpl) : base(paymentGatewayBpl) { }


        [HttpPost]
        [Route("VoidTrasaction")]
        public Result VoidTrasaction([FromBody]CreditCardDto creditCardDto)
        {
            return _bplManager.VoidTrasaction(creditCardDto);
        }
    }
}