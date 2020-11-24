using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.BonusSetup;
using Strive.BusinessLogic.BonusSetup;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class BonusSetupController : StriveControllerBase<IBonusSetupBpl>
    {
        public BonusSetupController(IBonusSetupBpl bonBpl) : base(bonBpl) { }
        #region POST
        /// <summary>
        /// Method to Add BonusSetup
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public Result AddBonusSetup([FromBody] BonusSetupDto bonus) => _bplManager.AddBonusSetup(bonus);

        /// <summary>
        /// Method to Update BonusSetup
        /// </summary>
        [HttpPost]
        [Route("Update")]
        public Result UpdateBonusSetup([FromBody] BonusSetupDto bonus) => _bplManager.UpdateBonusSetup(bonus);
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("Delete/{id}")]
        public Result DeleteBonusSetup(int id) => _bplManager.DeleteBonusSetup(id);
        #endregion
    }
}
