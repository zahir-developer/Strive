﻿using Microsoft.AspNetCore.Authorization;
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
        [Route("GetCashRegisterDetails/{cashRegisterType}&{locationId}&{dateTime}")]
        public Result GetCashRegisterDetails(CashRegisterType cashRegisterType, int locationId, DateTime dateTime)
        {
            return _CashRegisterBpl.GetCashRegisterDetails(cashRegisterType, locationId, dateTime);
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveTodayCashRegister([FromBody] List<Strive.BusinessEntities.CashRegister.CashRegister> lstCashRegister)
        {
            return _CashRegisterBpl.SaveTodayCashRegister(lstCashRegister);
        }
    }
}
