using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.ServiceSetup;
using Strive.BusinessEntities.Model;
using Strive.BusinessLogic;
using Strive.Common;
using System.Collections.Generic;

namespace Admin.API.Controllers
{
    [Authorize]
    //[AutoValidateAntiforgeryToken]

    [Route("Admin/[Controller]")]
    public class ServiceSetupController : StriveControllerBase<IServiceSetupBpl>
    {
        public ServiceSetupController(IServiceSetupBpl svcBpl) : base(svcBpl) { }

        [HttpPost]
        [Route("Add")]
        public Result AddService([FromBody] ServiceDto serviceSetup) => _bplManager.AddService(serviceSetup);

        [HttpPost]
        [Route("Update")]
        public Result UpdateService([FromBody] Service serviceSetup) => _bplManager.UpdateService(serviceSetup);

        [HttpDelete]
        [Route("Delete")]
        public Result Delete(int id) => _bplManager.DeleteServiceById(id);

        [HttpPost]
        [Route("GetAll")]
        public Result GetAllServiceSetup([FromBody]SearchDto searchDto) => _bplManager.GetAllServiceSetup(searchDto);

        [HttpGet]
        [Route("GetServiceById")]
        public Result GetServiceSetupById(int id) => _bplManager.GetServiceSetupById(id);

        [HttpGet]
        [Route("GetAllServiceType")]
        public Result GetAllServiceType() => _bplManager.GetAllServiceType();
      
        [HttpGet]
        [Route("GetAllServiceDetail")]
        public Result GetAllServiceDetail(int locationId) => _bplManager.GetAllServiceDetail(locationId);

        [HttpGet]
        [Route("GetService")]
        public Result GetServiceWithPrice() => _bplManager.GetServicesWithPrice();
    }
}
