using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.CashRegister.DTO;
using Strive.BusinessEntities.DTO;
using Strive.BusinessLogic.CashRegister;
using Strive.Common;
using System;

namespace Admin.API.Controllers
{
    [Authorize]
    //[AutoValidateAntiforgeryToken]

    [Route("Admin/[Controller]")]
    public class CashRegisterController : StriveControllerBase<ICashRegisterBpl>
    {
        public CashRegisterController(ICashRegisterBpl cashRegBpl) : base(cashRegBpl) { }
        
        [HttpPost]
        [Route("Save")]
        public Result SaveCashRegister([FromBody] CashRegisterDto cashRegister)
        {
            return _bplManager.SaveCashRegister(cashRegister);
        }

        [HttpGet]
        [Route("Get")]
        public Result GetCashRegisterDetails(CashRegisterType cashRegisterType, int locationId, DateTime dateTime)
        {
            return _bplManager.GetCashRegisterDetails(cashRegisterType, locationId, dateTime);
        }

        [HttpGet]
        [Route("GetTips")]
        public Result GetTipDetail([FromBody]TipdetailDto tipdetailDto)
        {
            return _bplManager.GetTipDetail(tipdetailDto);
        }
    }
}
