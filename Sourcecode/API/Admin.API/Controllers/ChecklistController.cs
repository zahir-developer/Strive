using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessLogic.Checklist;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class ChecklistController : StriveControllerBase<IChecklistBpl>
    {
        public ChecklistController(IChecklistBpl prdBpl) : base(prdBpl) { }

        #region GET
        /// <summary>
        /// Method to Get All Checklist.
        /// </summary>
        [HttpGet]
        [Route("GetChecklist")]
        public Result GetChecklist() => _bplManager.GetChecklist();

        [HttpGet]
        [Route("GetChecklistById")]
        public Result GetChecklistById(int id) => _bplManager.GetChecklistById(id);

        #endregion

        #region POST
        /// <summary>
        /// Method to Add Checklist
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public Result AddChecklist([FromBody]ChecklistDto checklistAdd) => _bplManager.AddChecklist(checklistAdd);

        [HttpPost]
        [Route("ChecklistNotification")]
        public Result GetChecklistNotification([FromBody]ChecklistNotificationDto checklist) => _bplManager.GetChecklistNotification(checklist);

        [HttpPost]
        [Route("UpdateChecklistNotification")]
        public Result UpdateChecklistNotification([FromBody]ChecklistNotificationUpdateDto checklistUpdate) => _bplManager.UpdateChecklistNotification(checklistUpdate);

        /// <summary>
        /// Method to Update Checklist
        /// </summary>
        [HttpPost]
        [Route("Update")]
        public Result UpdateChecklist([FromBody]ChecklistDto checklistUpdate) => _bplManager.UpdateChecklist(checklistUpdate);

        #endregion

        [HttpDelete]
        [Route("Delete")]
        public Result DeleteChecklist(int id) => _bplManager.DeleteChecklist(id);


    }
}
