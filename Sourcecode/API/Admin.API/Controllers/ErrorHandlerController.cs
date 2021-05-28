using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Route("Admin/[Controller]")]
    public class ErrorHandlerController : ControllerBase
    {

        //private readonly ILogger<ErrorHandlerController> _logger;

        [HttpGet]
        [Route("Log")]
        public Result Error([FromServices] IHostingEnvironment environment)
        {

            if (environment.EnvironmentName == "Development")
            {
                throw new InvalidOperationException(
                "This shouldn't be invoked in non-development environments.");
            }

            var ex = HttpContext.Features.Get<IExceptionHandlerFeature>();

            //_logger.LogInformation(ex.Error.Message + ex.Error.StackTrace);

            return Helper.BindFailedResult(ex.Error, System.Net.HttpStatusCode.InternalServerError);

        }
    }
}