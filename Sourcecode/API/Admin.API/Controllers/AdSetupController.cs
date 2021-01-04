using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.AdSetup;
using Strive.BusinessLogic.AdSetup;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]

    [Route("Admin/[Controller]")]
    public class AdSetupController : StriveControllerBase<IAdSetupBpl>
    {
        public AdSetupController(IAdSetupBpl aBpl) : base(aBpl) { }
        #region POST
        [HttpPost]
        [Route("Add")]
        public Result AddAdSetup([FromBody] AdSetupDto adSetup) => _bplManager.AddAdSetup(adSetup);

        [HttpPost]
        [Route("Update")]
        public Result UpdateAdSetup([FromBody] AdSetupDto adSetup) => _bplManager.UpdateAdSetup(adSetup);
        #endregion

        #region DELETE
        [HttpDelete]
        [Route("Delete")]
        public Result DeleteAdSetup(int id) => _bplManager.DeleteAdSetup(id);
        #endregion


        #region GET
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllAdSetup() => _bplManager.GetAllAdSetup();

        [HttpGet]
        [Route("GetById")]
        public Result GetAdSetupById(int id) => _bplManager.GetAdSetupById(id);
        #endregion
    }
}
