using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessLogic;
using Strive.BusinessLogic.ServiceSetup;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class ServiceSetupController : ControllerBase
    {
        IServiceSetupBpl _ServiceSetupBpl = null;

        public ServiceSetupController(IServiceSetupBpl ServiceSetupBpl)
        {
            _ServiceSetupBpl = ServiceSetupBpl;
        }

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllServiceSetup()
        {
            return _ServiceSetupBpl.GetServiceSetupDetails();
        }

        [HttpGet]
        [Route("GetAllServiceType")]
        public Result GetAllServiceType()
        {
            return _ServiceSetupBpl.GetAllServiceType();
        }

        [HttpGet]
        [Route("GetServiceById/{id}")]
        public Result GetServiceSetupById(int id)
        {
            return _ServiceSetupBpl.GetServiceSetupById(id);
        }

        [HttpPost]
        [Route("Save")]
        public Result SaveNewService([FromBody] List<Strive.BusinessEntities.ServiceSetup.tblService> lstServiceSetup)
        {
            return _ServiceSetupBpl.SaveNewServiceDetails(lstServiceSetup);
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public Result DeleteServiceById(int id)
        {
            return _ServiceSetupBpl.DeleteServiceById(id);
        }
    }
}
