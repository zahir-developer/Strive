﻿using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessLogic.Checkout;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class CheckoutController : StriveControllerBase<ICheckoutBpl>
    {
        public CheckoutController(ICheckoutBpl prdBpl) : base(prdBpl) { }

        /// <summary>
        /// Method to Get CheckedIn Vehicle Details.
        /// </summary>
        [HttpGet]
        [Route("GetCheckedInVehicleDetails")]
        public Result GetCheckedInVehicleDetails() => _bplManager.GetCheckedInVehicleDetails();
    }
}
