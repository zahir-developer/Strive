﻿using Admin.API.Helpers;
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
    public class TenantController : StriveControllerBase<ITenantBpl>
    {
        public TenantController(ITenantBpl bonBpl, IConfiguration config) : base(bonBpl,config) { }
        #region POST

        /// <summary>
        /// Create Tenant
        /// </summary>
        [HttpPost]
        [Route("CreateTenant")]
        public Result CreateTenant([FromBody] TenantCreateViewModel tenant)
        {
            return _bplManager.CreateTenant(tenant);
        }


        /// <summary>
        /// Update tenant
        /// </summary>
        [HttpPost]
        [Route("UpdateTenant")]
        public Result UpdateTenant([FromBody] TenantCreateViewModel tenant)
        {
            return _bplManager.UpdateTenant(tenant);
        }
        #endregion

        #region GET

        /// <summary>
        /// GetAllTenant
        /// </summary>
        [HttpGet]
        [Route("AllTenant")]
        public Result GetAllTenant()
        {
            return _bplManager.GetAllTenant();
        }

        /// <summary>
        /// GetAllModule
        /// </summary>
        [HttpGet]
        [Route("AllModule")]
        public Result GetAllModule()
        {
            return _bplManager.GetAllModule();
        }

        /// <summary>
        /// GetTanantbyId
        /// </summary>
        [HttpGet]
        [Route("TenantById/{id}")]
        public TenantByIdViewModel GetTenantById(int id)
        {
            return _bplManager.GetTenantById(id);
        }
        #endregion


    }
}
