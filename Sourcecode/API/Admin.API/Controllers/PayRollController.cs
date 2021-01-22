using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.PayRoll;
using Strive.BusinessLogic.PayRoll;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class PayRollController : StriveControllerBase<IPayRollBpl>
    {
        public PayRollController(IPayRollBpl colBpl) : base(colBpl) { }

        [HttpGet]
        [Route("GetPayroll")]
        public Result GetPayroll(PayRollDto payRollDto) => _bplManager.GetPayRoll(payRollDto);

        [HttpPost]
        [Route("AddPayRoll")]
        public Result AddPayRoll([FromBody] PayRollAddDto payRollAddDto) => _bplManager.AddPayRoll(payRollAddDto);
        [HttpGet]
        [Route("UpdatePayRoll")]
        public Result UpdatePayRoll(PayRollUpdateDto payRollUpdateDto) => _bplManager.UpdatePayRoll(payRollUpdateDto);
        
        /// <summary>
        /// To update Adjustment against EmployeeId
        /// </summary>
        [HttpPost]
        [Route("UpdateEmployeeAdjustment")]
        public Result UpdateEmployeeAdjustment([FromBody]List<EmployeeAdjustmentDto> employeeAdjustment) => _bplManager.UpdateEmployeeAdjustment(employeeAdjustment);



        [HttpPost]
        [Route("AddPayRollProcess")]
        public Result AddPayRollProcess([FromBody] PayrollProcessAddDto payRollProcessAddDto) => _bplManager.AddPayRollProcess(payRollProcessAddDto);

    }
}