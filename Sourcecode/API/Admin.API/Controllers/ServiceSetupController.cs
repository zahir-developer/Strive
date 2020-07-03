using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessLogic;
using Strive.Common;
using System.Collections.Generic;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class ServiceSetupController : ControllerBase
    {
        readonly IServiceSetupBpl _serviceSetupBpl = null;

        public ServiceSetupController(IServiceSetupBpl serviceSetupBpl)
        {
            _serviceSetupBpl = serviceSetupBpl;
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllServiceSetup()
        {
            return _serviceSetupBpl.GetServiceSetupDetails();
        }

        [HttpGet]
        [Route("GetAllServiceType")]
        public Result GetAllServiceType()
        {
            return _serviceSetupBpl.GetAllServiceType();
        }

        [HttpGet]
        [Route("GetServiceById/{id}")]
        public Result GetServiceSetupById(int id)
        {
            return _serviceSetupBpl.GetServiceSetupById(id);
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveNewService([FromBody] List<Strive.BusinessEntities.ServiceSetup.tblService> lstServiceSetup)
        {
            return _serviceSetupBpl.SaveNewServiceDetails(lstServiceSetup);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public Result DeleteServiceById(int id)
        {
            return _serviceSetupBpl.DeleteServiceById(id);
        }
    }
}
