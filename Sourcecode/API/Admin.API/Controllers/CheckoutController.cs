using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CheckoutEntry;
using Strive.BusinessEntities.DTO.Report;
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

        #region GET
        /// <summary>
        /// Method to Get CheckedIn Vehicle Details.
        /// </summary>
        [HttpPost]
        [Route("GetAllCheckoutDetails")]
        public Result GetAllCheckoutDetails([FromBody]CheckOutDto checkoutDto) => _bplManager.GetAllCheckoutDetails(checkoutDto);
        #endregion

        #region POST
        /// <summary>
        /// Method to Update Checkout Flag and Checkout Time
        /// </summary>
        [HttpPost]
        [Route("UpdateCheckoutDetails")]
        public Result UpdateCheckoutDetails([FromBody]CheckoutEntryDto checkoutEntry) => _bplManager.UpdateCheckoutDetails(checkoutEntry);

        /// <summary>
        /// Method to Update Job Status AS HOLD
        /// </summary>
        [HttpPost]
        [Route("UpdateJobStatusHold")]
        public Result UpdateJobStatusHold([FromBody]JobIdDto jobIdDto) => _bplManager.UpdateJobStatusHold(jobIdDto);

        /// <summary>
        /// Method to Update Job Status AS COMPLETED
        /// </summary>
        [HttpPost]
        [Route("UpdateJobStatusComplete")]
        public Result UpdateJobStatusComplete([FromBody]JobIdDto jobIdDto) => _bplManager.UpdateJobStatusComplete(jobIdDto);
        #endregion

        #region GET
        /// <summary>
        /// Method to Get Customer history
        /// </summary>
        [HttpPost]
        [Route("GetCustomerHistory")]
        public Result GetCustomerHistory([FromBody]CustomerHistorySearchDto salesReportDto) => _bplManager.GetCustomerHistory(salesReportDto);
        #endregion
    }
}
