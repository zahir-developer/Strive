using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Details;
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
        /// <summary>
        /// Method to Update Details.
        /// </summary>
        [HttpPost]
        [Route("UpdateDetails")]
        public Result UpdateDetails([FromBody] DetailsDto details) => _bplManager.UpdateDetails(details);

        /// <summary>
        /// Method to Assign Employee With Service.
        /// </summary>
        [HttpPost]
        [Route("AddServiceEmployee")]
        public Result AddServiceEmployee([FromBody] JobServiceEmployeeDto jobServiceEmployee) => _bplManager.AddServiceEmployee(jobServiceEmployee);

        /// <summary>
        /// Method to Get Schedule Details in Grid.
        /// </summary>
        [HttpPost]
        [Route("GetBaySchedulesDetails")]
        public Result GetBaySchedulesDetails([FromBody] DetailsGridDto detailsGrid) => _bplManager.GetBaySchedulesDetails(detailsGrid);
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
        /// Method to retrieve All Past ClientNotes based on given ClientId.
        /// </summary>
        [HttpGet]
        [Route("GetPastClientNotesById/{id}")]
        public Result GetPastClientNotesById(int id) => _bplManager.GetPastClientNotesById(id);
        /// <summary>
        /// Method to retrieve All JobType.
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

        /// <summary>
        /// Method to retrieve Detail job based on search input.
        /// </summary>
        [HttpPost]
        [Route("GetAllDetailSearch")]
        public Result GetDetailSearch([FromBody]SearchDto SearchDto) => _bplManager.GetAllDetailSearch(SearchDto);



        [HttpGet]
        [Route("GetDetailScheduleStatus")]
        public Result GetDetailScheduleStatus(DetailScheduleDto scheduleDto) => _bplManager.GetDetailScheduleStatus(scheduleDto);
        #endregion

        /// <summary>
        /// Delete a Detail based on Date and LocationId.
        /// </summary>
        [HttpDelete]
        [Route("Delete")]
        public Result DeleteDetails(int id) => _bplManager.DeleteDetails(id);

        /// <summary>
        /// Update job status
        /// </summary>
        [HttpPost]
        [Route("UpdateJobStatus")]
        public Result UpdateJobStatus([FromBody]JobStatusDto jobStatus) => _bplManager.UpdateJobStatus(jobStatus);
    }
}
