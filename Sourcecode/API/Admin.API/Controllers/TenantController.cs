using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities.DTO.BonusSetup;
using Strive.BusinessLogic.BonusSetup;
using Strive.BusinessLogic.SuperAdmin.Tenant;
using Strive.Common;
using Strive.BusinessEntities.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class TenantController : StriveControllerBase<ISuperAdminBpl>
    {
        public TenantController(ISuperAdminBpl bonBpl, IConfiguration config) : base(bonBpl,config) { }
        #region POST

        /// <summary>
        /// Login for Employee and Client.
        /// </summary>
        [HttpPost]
        [Route("CreateTenant")]
        public Result CreateTenant([FromBody] TenantViewModel tenant)
        {
            return _bplManager.CreateTenant(tenant);
        }
        #endregion

    }
}
