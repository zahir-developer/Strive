using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities;
using Strive.BusinessLogic;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class CommonController : ControllerBase
    {
        ICommonBpl _CommonBpl = null;

        public CommonController(ICommonBpl CommonBpl)
        {
            _CommonBpl = CommonBpl;
        }

        [HttpGet]
        [Route("CountryList")]
        public Result GetCountyList()
        {
            return _CommonBpl.GetCodesByCategory(GlobalCodes.COUNTRY);
        }

        [HttpGet]
        [Route("StateList")]
        public Result GetStateList()
        {
            return _CommonBpl.GetCodesByCategory(GlobalCodes.STATE);
        }

        [HttpGet]
        [Route("GetCodesByCategory")]
        public Result GetCodeByCategory(GlobalCodes globalCode)
        {
            return _CommonBpl.GetCodesByCategory(globalCode);
        }
    }
}
