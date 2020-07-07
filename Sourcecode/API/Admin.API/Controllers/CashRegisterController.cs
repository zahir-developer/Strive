using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.CashRegister;
using Strive.BusinessLogic.CashRegister;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class CashRegisterController : ControllerBase
    {
        ICashRegisterBpl _CashRegisterBpl = null;

        public CashRegisterController(ICashRegisterBpl CashRegisterBpl)
        {
            _CashRegisterBpl = CashRegisterBpl;
        }

        [HttpGet]
        [Route("GetCashRegisterByDate/{dateTime}")]
        public Result GetCashRegisterByDate(DateTime dateTime)
        {
            return _CashRegisterBpl.GetCashRegisterByDate(dateTime);
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveTodayCashRegister([FromBody] List<Strive.BusinessEntities.CashRegister.CashRegister> lstCashRegisterConsolidate)
        {
            return _CashRegisterBpl.SaveTodayCashRegister(lstCashRegisterConsolidate);
        }

        [HttpPost]
        [Route("SaveNewApproach")]
        public Result SaveCashRegisterNewApproach([FromBody] List<Strive.BusinessEntities.CashRegister.CashRegister> lstCashRegisterConsolidate)
        {
            return _CashRegisterBpl.SaveCashRegisterNewApproach(lstCashRegisterConsolidate);
        }
    }
}
