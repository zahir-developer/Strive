using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Strive.BusinessLogic.Logger;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Route("Admin/[Controller]")]
    public class ErrorHandlerController : StriveControllerBase<ILogBpl>
    {

        private readonly ILogger _logger;

        public ErrorHandlerController(ILogBpl logBpl, IConfiguration config, ILogger<ErrorHandlerController> logger) : base(logBpl, config)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("Log")]
        public Result Error()
        {
            //var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();

            _logger.LogInformation("Error log called...!");

            return Helper.BindValidationErrorResult("testing");

        }
    }
}