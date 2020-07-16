using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Admin.API.Controllers
{
    public class HealthCheckController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("/Healthcheck/Ping")]
        public bool Login()
        {
            return true;
        }
    }
}
