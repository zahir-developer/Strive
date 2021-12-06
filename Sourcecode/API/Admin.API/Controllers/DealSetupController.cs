using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.AdSetup;
using Strive.BusinessLogic.DealSetup;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]

    public class DealSetupController : StriveControllerBase<IdealSetupBpl>
    {
        public DealSetupController(IdealSetupBpl prdBpl) : base(prdBpl) { }


        #region POST
        /// <summary>
        /// Method to Add DealSetup
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public Result AddDealSetup([FromBody]Deals dealSetup) => _bplManager.AddDealSetup(dealSetup);


        #endregion

        #region Get

        [HttpGet]
        [Route("GetAllDeals")]
        public Result GetAllDeals() => _bplManager.GetAllDeals();

        #endregion
        [HttpGet]
        [Route("Update")]
        public Result UpdateToggledeal(bool status) => _bplManager.UpdateToggledeal(status);

        [HttpPost]
        [Route("AddClientDeal")]
        public Result AddClientDetail([FromBody] ClientDealDto addClientDeal) => _bplManager.AddClientDeal(addClientDeal);


        [HttpGet]
        [Route("GetClientDeal")]
        public Result GetClientDeal(ClientDealDto clientDeal) => _bplManager.GetClientDeal(clientDeal);
    }
}
