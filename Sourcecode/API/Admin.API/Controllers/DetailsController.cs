using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.Model;
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
        [Route("GetDetailsById/{id}")]
        public Result GetDetailsById(int id) => _bplManager.GetDetailsById(id);
        [HttpPost]
        [Route("AddDetails")]
        public Result AddDetails([FromBody] DetailsDto details) => _bplManager.AddDetails(details);
        [HttpPost]
        [Route("UpdateDetails")]
        public Result UpdateDetails([FromBody] DetailsDto details) => _bplManager.UpdateDetails(details);
        [HttpGet]
        [Route("GetAllBayById/{id}")]
        public Result GetAllBayById(int id) => _bplManager.GetAllBayById(id);
        [HttpPost]
        [Route("GetBaySchedulesDetails")]
        public Result GetBaySchedulesDetails([FromBody] DetailsGridDto detailsGrid) => _bplManager.GetBaySchedulesDetails(detailsGrid);
        [HttpGet]
        [Route("GetJobType")]
        public Result GetJobType() => _bplManager.GetJobType();
        [HttpDelete]
        [Route("Delete")]
        public Result DeleteDetails(int id) => _bplManager.DeleteDetails(id);
        [HttpPost]
        [Route("AddEmployeeScheduleToDetails")]
        public Result AddEmployeeScheduleToDetails([FromBody] EmployeeScheduleDetailsDto empSchedule) => _bplManager.AddEmployeeScheduleToDetails(empSchedule);
        [HttpPost]
        [Route("GetAllDetails")]
        public Result GetAllDetails([FromBody] DetailsGridDto detailsGrid) => _bplManager.GetAllDetails(detailsGrid);
    }
}
