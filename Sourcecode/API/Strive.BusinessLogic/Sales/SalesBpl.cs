﻿using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO.Sales;
using Strive.BusinessLogic.Common;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Sales
{
    public class SalesBpl : Strivebase, ISalesBpl
    {
        public SalesBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }
        //public Result UpdateSalesItem(SalesDto sales)
        //{
        //    return ResultWrap(new SalesRal(_tenant).UpdateSalesItem, sales, "Status");
        //}
        public Result UpdateItem(SalesItemUpdateDto salesItemUpdateDto)
        {
            try
            {
                return ResultWrap(new SalesRal(_tenant).UpdateItem, salesItemUpdateDto, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result SaveProductItem(SalesProductItemDto salesProductItemDto)
        {
            try
            {
                return ResultWrap(new SalesRal(_tenant).SaveProductItem, salesProductItemDto, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }


        public Result DeleteItemById(DeleteItemDto itemDto)
        {
            try
            {
                return ResultWrap(new SalesRal(_tenant).DeleteItemById, itemDto, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public Result GetItemList(SalesListItemDto salesListItemDto)
        {
            return ResultWrap(new SalesRal(_tenant).GetItemList, salesListItemDto, "SalesList");
        }
        public Result GetAccountDetails(SalesAccountDto salesAccountDto)
        {
            return ResultWrap(new SalesRal(_tenant).GetAccountDetails, salesAccountDto, "Account");
        }

        public Result GetScheduleByTicketNumber(SalesDto salesDto)
        {
            return ResultWrap(new SalesRal(_tenant).GetScheduleByTicketNumber, salesDto, "Status");
        }
        public Result AddPayment(SalesPaymentDetailDto salesPayment)
        {
            try
            {
                var jobPaymnetId = new SalesRal(_tenant).AddPayment(salesPayment.SalesPaymentDto);

                if (jobPaymnetId > 0)
                {
                    var result = new SalesRal(_tenant).UpdateJobPayment(salesPayment.SalesPaymentDto.JobPayment.JobId, jobPaymnetId);

                    if (salesPayment.SalesProductItemDto != null)
                    {
                        foreach (var prod in salesPayment.SalesProductItemDto.JobProductItem)
                        {
                            var productUpdate = new SalesRal(_tenant).UpdateProductQuantity(prod.Quantity, prod.ProductId);

                            var product = new ProductRal(_tenant).GetProductById(prod.ProductId);

                            string roles = Roles.Manager.ToString() + ',' + Roles.Operator;

                            //var emailId = new CommonRal(_tenant).GetEmailIdByRole(salesPayment.LocationId, roles);

                            var emailId = new CommonRal(_tenant).GetEmailIdByRole(salesPayment.LocationId.ToString());

                            if (product != null && product.Quantity != null)
                            {
                                if ((product.Quantity - prod.Quantity) < product.ThresholdLimit)
                                {
                                    foreach (var item in emailId)
                                    {
                                        var subject = "Product threshold limit";
                                        Dictionary<string, string> keyValues = new Dictionary<string, string>();
                                        keyValues.Add("{{managerName}}", item.FirstName);
                                        keyValues.Add("{{productName}}", product.ProductName);
                                        keyValues.Add("{{locationName}}", product.LocationName);
                                        new CommonBpl(_cache, _tenant).SendEmail(HtmlTemplate.ProductThreshold, item.Email, keyValues, subject);
                                    }

                                }
                            }
                        }
                    }
                }

                return ResultWrap(jobPaymnetId > 0, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result AddListItem(SalesAddListItemDto salesAddListItem)
        {
            try
            {

                return ResultWrap(new SalesRal(_tenant).AddListItem, salesAddListItem, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result DeleteJob(SalesItemDeleteDto salesItemDeleteDto)
        {
            try
            {
                return ResultWrap(new SalesRal(_tenant).DeleteJob, salesItemDeleteDto, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result RollBackPayment(SalesItemDeleteDto salesItemDeleteDto)
        {
            try
            {
                return ResultWrap(new SalesRal(_tenant).RollBackPayment, salesItemDeleteDto, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result UpdateListItem(SalesUpdateItemDto salesUpdateItemDto)
        {
            try
            {
                return ResultWrap(new SalesRal(_tenant).UpdateListItem, salesUpdateItemDto, "Status");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result GetServicesWithPrice()
        {
            return ResultWrap(new SalesRal(_tenant).GetServicesWithPrice, "ServicesWithPrice");
        }

        public Result GetServicesAndProduct(int id,string query)
        {
            return ResultWrap(new SalesRal(_tenant).GetServicesAndProduct,id,query, "ServiceAndProductList");
        }
    }
}
