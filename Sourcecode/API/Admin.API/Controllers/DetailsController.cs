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

        #region POST
        [HttpPost]
        [Route("AddDetails")]
        public Result AddDetails([FromBody] DetailsDto details) => _bplManager.AddDetails(details);
        #endregion

        #region PUT
        [HttpPost]
        [Route("UpdateDetails")]
        public Result UpdateDetails([FromBody] DetailsDto details) => _bplManager.UpdateDetails(details);
        #endregion

        #region GET
        [HttpGet]
        [Route("GetDetailsById/{id}")]
        public Result GetDetailsById(int id) => _bplManager.GetDetailsById(id);

        [HttpGet]
        [Route("GetAllBayById/{id}")]
        public Result GetAllBayById(int id) => _bplManager.GetAllBayById(id);

        [HttpGet]
        [Route("GetWashTimeById/{id}")]
        public Result GetWashTimeById(int id) => _bplManager.GetWashTimeById(id);

        [HttpGet]
        [Route("GetPastClientNotesById/{id}")]
        public Result GetPastClientNotesById(int id) => _bplManager.GetPastClientNotesById(id);
        
        [HttpGet]
        [Route("GetScheduleDetailsByDate/{date}")]
        public Result GetScheduleDetailsByDate(DateTime date) => _bplManager.GetScheduleDetailsByDate(date);

        [HttpGet]
        [Route("GetJobType")]
        public Result GetJobType() => _bplManager.GetJobType();

        [HttpGet]
        [Route("GetAllDetails")]
        public Result GetAllDetails(DetailsGridDto detailsGrid) => _bplManager.GetAllDetails(detailsGrid);
        #endregion

        #region
        [HttpDelete]
        [Route("Delete")]
        public Result DeleteDetails(int id) => _bplManager.DeleteDetails(id);
        #endregion
    }
}
