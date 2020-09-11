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
        /// <summary>
        /// Method to Add Details.
        /// </summary>
        [HttpPost]
        [Route("AddDetails")]
        public Result AddDetails([FromBody] DetailsDto details) => _bplManager.AddDetails(details);
        #endregion

        #region PUT
        /// <summary>
        /// Method to Update Details.
        /// </summary>
        [HttpPost]
        [Route("UpdateDetails")]
        public Result UpdateDetails([FromBody] DetailsDto details) => _bplManager.UpdateDetails(details);
        #endregion

        #region GET
        /// <summary>
        /// Method to retrieve Details based on given JobId.
        /// </summary>
        [HttpGet]
        [Route("GetDetailsById/{id}")]
        public Result GetDetailsById(int id) => _bplManager.GetDetailsById(id);

        /// <summary>
        /// Method to retrieve Bays based on given LocationId.
        /// </summary>
        [HttpGet]
        [Route("GetAllBayById/{id}")]
        public Result GetAllBayById(int id) => _bplManager.GetAllBayById(id);

        /// <summary>
        /// Method to retrieve WashTime based on given LocationId.
        /// </summary>
        [HttpGet]
        [Route("GetWashTimeById/{id}")]
        public Result GetWashTimeById(int id) => _bplManager.GetWashTimeById(id);

        /// <summary>
        /// Method to retrieve Past Client Notes based on given ClientId.
        /// </summary>
        [HttpGet]
        [Route("GetPastClientNotesById/{id}")]
        public Result GetPastClientNotesById(int id) => _bplManager.GetPastClientNotesById(id);

        /// <summary>
        /// Method to retrieve Schedule Details based on given Date.
        /// </summary>
        [HttpGet]
        [Route("GetScheduleDetailsByDate/{date}")]
        public Result GetScheduleDetailsByDate(DateTime date) => _bplManager.GetScheduleDetailsByDate(date);

        /// <summary>
        /// Method to retrieve All Job Type.
        /// </summary>
        [HttpGet]
        [Route("GetJobType")]
        public Result GetJobType() => _bplManager.GetJobType();

        /// <summary>
        /// Method to retrieve All Details based on given JobId.
        /// </summary>
        [HttpGet]
        [Route("GetAllDetails")]
        public Result GetAllDetails(DetailsGridDto detailsGrid) => _bplManager.GetAllDetails(detailsGrid);
        #endregion

        #region DELETE
        /// <summary>
        /// Delete a Detail based on Date and LocationId.
        /// </summary>
        [HttpDelete]
        [Route("Delete")]
        public Result DeleteDetails(int id) => _bplManager.DeleteDetails(id);
        #endregion
    }
}
