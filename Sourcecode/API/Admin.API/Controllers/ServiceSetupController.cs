using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO.ServiceSetup;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic;
using Strive.Common;
using System.Collections.Generic;

namespace Admin.API.Controllers
{
    [Authorize]

    [Route("Admin/[Controller]")]
    public class ServiceSetupController : StriveControllerBase<IServiceSetupBpl>
    {
        public ServiceSetupController(IServiceSetupBpl svcBpl) : base(svcBpl) { }

        #region POST
        /// <summary>
        /// Method to Add a Service.
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public Result AddService([FromBody] Service serviceSetup) => _bplManager.AddService(serviceSetup);

        /// <summary>
        /// Method to Update a Service.
        /// </summary>
        [HttpPost]
        [Route("Update")]
        public Result UpdateService([FromBody] Service serviceSetup) => _bplManager.UpdateService(serviceSetup);

        /// <summary>
        /// Retrieves list of all Service Details by search params
        /// </summary>
        [HttpPost]
        [Route("Search")]
        public Result GetSearchResult([FromBody] ServiceSearchDto search) => _bplManager.GetSearchResult(search);
        #endregion

        #region DELETE
        /// <summary>
        /// Deletes a Service based on the given ServiceId.
        /// </summary>
        [HttpDelete]
        [Route("Delete")]
        public Result Delete(int id) => _bplManager.DeleteServiceById(id);
        #endregion

        #region GET
        /// <summary>
        /// Retrieves list of all Services available.
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public Result GetAllServiceSetup() => _bplManager.GetAllServiceSetup();

        /// <summary>
        /// Retrieves list of all Services Types available.
        /// </summary>
        [HttpGet]
        [Route("GetAllServiceType")]
        public Result GetAllServiceType() => _bplManager.GetAllServiceType();

        /// <summary>
        /// Retrieves list of all Service Details with its Upcharges.
        /// </summary>
        [HttpGet]
        [Route("GetServicesDetails")]
        public Result GetServicesDetailsWithUpcharges() => _bplManager.GetServicesDetailsWithUpcharges();

        /// <summary>
        /// Method to retrieve Service details based on given ServiceId.
        /// </summary>
        [HttpGet]
        [Route("GetById/{id}")]
        public Result GetServiceSetupById(int id) => _bplManager.GetServiceSetupById(id);

        /// <summary>
        /// Method to retrieve Service Category details based on given locationId.
        /// </summary>
        [HttpGet]
        [Route("GetServiceCategoryByLocationId/{id}")]
        public Result GetServiceCategoryByLocationId(int id) => _bplManager.GetServiceCategoryByLocationId(id);
        #endregion
    }
}
