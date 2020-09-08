using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.DTO.User;
using Strive.BusinessLogic.Client;
using Strive.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;
using Microsoft.AspNetCore.Authorization;
using Strive.BusinessLogic.Client;
using Strive.BusinessEntities.Auth;
using Admin.Api.Controllers;
using Strive.BusinessLogic.Auth;
using Microsoft.Extensions.Configuration;
using Strive.BusinessEntities.Client;
using Admin.API.Helpers;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.DTO.Vehicle;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class ClientController : StriveControllerBase<IClientBpl>
    {
        public ClientController(IClientBpl clientBpl) : base(clientBpl) { }

        [HttpPost]
        [Route("InsertClientDetails")]
        public Result InsertClientDetails([FromBody] ClientDto client) => _bplManager.SaveClientDetails(client);
        [HttpPost]
        [Route("Save")]
        public Result SaveClientDetails([FromBody] ClientView client) => _bplManager.SaveClientDetails(client);

        [AllowAnonymous]
        [HttpPost]
        [Route("Signup")]
        public Result Signup([FromBody] UserSignupDto clientSignup) => _bplManager.Signup(clientSignup);

        [HttpDelete]
        [Route("Delete")]
        public Result DeleteClient(int clientId) => _bplManager.DeleteClient(clientId);
        [Route("UpdateClientVehicle")]
        public Result ClientVehicleSave([FromBody] ClientDto client) => _bplManager.UpdateClientVehicle(client);

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllClient() => _bplManager.GetAllClient();
        [Route("GetAllClient")]
        public Result GetAllClient()
        {
            return _bplManager.GetAllClient();

        }
        [HttpDelete]
        [Route("{clientId}")]
        public Result DeleteClient(int clientId)
        {
            return _bplManager.DeleteClient(clientId);
        }
        [HttpGet]
        [Route("GetClientById/{clientId}")]
        public Result GetClientById(int clientId)
        {
            return _bplManager.GetClientById(clientId);
        }
        [HttpGet]
        [Route("GetClientVehicleById/{clientId}")]
        public Result GetClientVehicleById(int clientId)
        {
            return _bplManager.GetClientVehicleById(clientId);
        }
        [HttpPost]
        [Route("GetClientSearch")]
        public Result GetServiceSearch([FromBody] ClientSearchDto search) => _bplManager.GetClientSearch(search);

        [HttpGet]
        [Route("GetClientCodes")]
        public Result GetClientCodes()
        {
            return _bplManager.GetClientCodes();
        }
        #region
        [HttpGet]
        [Route("GetStatementByClientId/{id}")]
        public Result GetStatementByClientId(int id) => _bplManager.GetStatementByClientId(id);
        #endregion
        #region
        [HttpGet]
        [Route("GetHistoryByClientId/{id}")]
        public Result GetHistoryByClientId(int id) => _bplManager.GetHistoryByClientId(id);
        #endregion

    }
}
