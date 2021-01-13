﻿using Microsoft.Extensions.Caching.Distributed;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CheckoutEntry;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Checkout
{
    public class CheckoutBpl : Strivebase, ICheckoutBpl
    {
        public CheckoutBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache) { }
        public Result GetAllCheckoutDetails(int locationId)
        {
            return ResultWrap(new CheckoutRal(_tenant).GetAllCheckoutDetails,locationId, "GetCheckedInVehicleDetails");
        }
        public Result UpdateCheckoutDetails(CheckoutEntryDto checkoutEntry)
        {
            return ResultWrap(new CheckoutRal(_tenant).UpdateCheckoutDetails,checkoutEntry, "SaveCheckoutTime");
        }
        public Result UpdateJobStatusHold(JobIdDto jobIdDto)
        {
            return ResultWrap(new CheckoutRal(_tenant).UpdateJobStatusHold, jobIdDto, "UpdateJobStatus");
        }
        public Result UpdateJobStatusComplete(JobIdDto jobIdDto)
        {
            return ResultWrap(new CheckoutRal(_tenant).UpdateJobStatusComplete, jobIdDto, "UpdateJobStatus");
        }
    }
}
