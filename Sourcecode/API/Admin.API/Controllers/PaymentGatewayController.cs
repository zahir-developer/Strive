using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.PaymentGateway;
using Strive.BusinessLogic.PaymentGateway;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Route("Payroll/[controller]")]
    [Authorize]
    public class PaymentGatewayController : StriveControllerBase<IPaymentGatewayBpl>
    {

        public PaymentGatewayController(IPaymentGatewayBpl paymentGatewayBpl) : base(paymentGatewayBpl) { }



        [HttpPost]
        [Route("Auth")]
        public Result Auth([FromBody] CardPaymentDto cardPaymentDto)
        {

            try
            {
                var authResponse = _bplManager.AuthTransaction(cardPaymentDto);

                if (authResponse != null)
                {
                    try
                    {
                        string respText = string.Empty;

                        var respStat = authResponse.GetValue("respstat");
                        if (respStat != null)
                        {
                            string resp = respStat.ToString();

                            var respTextObj = authResponse.GetValue("resptext");

                            string respMessage = string.Empty;

                            if(respTextObj != null)
                            {
                                respMessage = respTextObj.ToString();
                            }
                            
                            if (resp == "A")
                            {
                                var retRefObj = authResponse.GetValue("retref");
                                var authcodeObj = authResponse.GetValue("authcode");
                                //Capture
                                if (retRefObj != null & authcodeObj != null)
                                {
                                    return Helper.BindSuccessResult(authResponse);
                                }
                                else
                                    return SendValidationErrorResult(respMessage, authResponse);
                            }
                            else if (resp == "B" || resp == "C")
                            {
                                return Helper.BindValidationErrorResult(respMessage, authResponse);
                            }
                            else
                                return Helper.BindValidationErrorResult("Transaction Error", authResponse);
                        }
                        else
                        {
                            return Helper.BindValidationErrorResult("Trasaction Failed", authResponse);
                        }
                    }
                    catch (Exception ex)
                    {
                        return Helper.BindFailedResultWithContent(authResponse, ex, System.Net.HttpStatusCode.BadRequest);
                    }
                }
                else
                    return Helper.BindValidationErrorResult("Trasaction Error", authResponse);

            }
            catch (Exception ex)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("Capture")]
        public Result Capture([FromBody] CaptureDetail captureDetail)
        {
            try
            {
                string respMessage = string.Empty;

                captureDetail.Amount = captureDetail.Amount;
                captureDetail.AuthCode = captureDetail.AuthCode;
                captureDetail.InvoiceId = captureDetail.InvoiceId;
                captureDetail.RetRef = captureDetail.RetRef;

                var captureResponse = _bplManager.CaptureTransaction(captureDetail);

                string respText = string.Empty;

                var respTextObj = captureResponse.GetValue("resptext");

                if (respTextObj != null)
                {
                    respText = respTextObj.ToString();
                }
                else
                    respText = "Unknown Error.";

                var respStat = captureResponse.GetValue("respstat");

                if (respStat != null)
                {
                    string resp = respStat.ToString();
                    if (resp == "A")
                    {
                        return Helper.BindSuccessResult(captureResponse);
                    }
                    else
                    {
                        return Helper.BindValidationErrorResult(respText, captureResponse);
                    }
                }
                else
                    return Helper.BindValidationErrorResult(respText, captureResponse);
            }
            catch (Exception ex)
            {
                return Helper.BindFailedResult(ex,System.Net.HttpStatusCode.BadRequest);
            }
            
        }


        Result SendErrorResult(Newtonsoft.Json.Linq.JObject jObject)
        {
            return Helper.BindValidationErrorResult("Communication Error");
        }

        Result SendValidationErrorResult(string message, Newtonsoft.Json.Linq.JObject jObject)
        {
            return Helper.BindValidationErrorResult(message, jObject);
        }
    }
}