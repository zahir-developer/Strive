using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessLogic.Printer;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class PrinterController : StriveControllerBase<IPrinterBpl>
    {
        public PrinterController(IPrinterBpl colBpl) : base(colBpl) { }

        [HttpGet]
        [Route("GetPrinterByLocation/{locationId}")]
        public Result GetPrinterByLocation(int locationId) => _bplManager.GetPrinterByLocation(locationId);
    }
}