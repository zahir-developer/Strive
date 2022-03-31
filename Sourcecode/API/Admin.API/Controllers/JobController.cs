using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessLogic.Job;
using Strive.Common;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class JobController : StriveControllerBase<IJobBpl>
    {
        public JobController(IJobBpl dBpl) : base(dBpl) { }

        [HttpGet]
        [Route("GetPrintJobDetail")]
        public Result GetPrintJobDetail(int jobId) => _bplManager.GetPrintJobDetail(jobId);

    }
}