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
        #endregion

        #region POST
        /// <summary>
        /// Method to Add Checklist
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public Result AddChecklist([FromBody]ChecklistAddDto checklistAdd) => _bplManager.AddChecklist(checklistAdd);


        #endregion

        [HttpDelete]
        [Route("Delete")]
        public Result DeleteChecklist(int id) => _bplManager.DeleteChecklist(id);
    }
}
