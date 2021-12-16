using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Strive.BusinessLogic.SuperAdmin.Tenant;
using Strive.Common;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.DTO;

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
            return _bplManager.CreateTenant(tenant, GetTenantConnection());
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
        [HttpPost]
        [Route("AllTenant")]
        public Result GetAllTenant([FromBody] SearchDto searchDto)
        {
            return _bplManager.GetAllTenant(searchDto);
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
        public TenantModulesViewModel GetTenantById(int id)
        {
            return _bplManager.GetTenantById(id);
        }
        [HttpGet]
        [Route("StateList")]
        public Result GetState()
        {
            return _bplManager.GetState();
        }
        [HttpGet]
        [Route("GetCityByStateId/{stateId}")]
        public Result GetCityByStateId(int stateId)
        {
            return _bplManager.GetCityByStateId(stateId);
        }
        [HttpGet]
        [Route("GetLocationMaxLimit")]
        public Result GetLocationMaxLimit()
        {
            return _bplManager.GetLocationMaxLimit();
        }

        #endregion


    }
}
