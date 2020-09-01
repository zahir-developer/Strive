using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessLogic.Details;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]

    [Route("Admin/[Controller]")]
    public class DetailsController : StriveControllerBase<IDetailsBpl>
    {
        public DetailsController(IDetailsBpl dBpl) : base(dBpl) { }
        [HttpGet]
        [Route("GetAllDetailsTime")]
        public Result GetAllDetails() => _bplManager.GetAllDetails();
        [HttpGet]
        [Route("GetDetailsById/{id}")]
        public Result GetDetailsById(int id) => _bplManager.GetDetailsById(id);
        [HttpPost]
        [Route("AddDetails")]
        public Result AddDetails([FromBody] DetailsDto details) => _bplManager.AddDetails(details);
        [HttpPost]
        [Route("UpdateDetails")]
        public Result UpdateDetails([FromBody] DetailsDto details) => _bplManager.UpdateDetails(details);
    }
}
