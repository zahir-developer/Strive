using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
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
        public Result AddDealSetup([FromBody]DealSetupDto dealSetup) => _bplManager.AddDealSetup(dealSetup);


        #endregion
    }
}
