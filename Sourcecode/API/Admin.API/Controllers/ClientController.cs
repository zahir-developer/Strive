using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.DTO.User;
using Strive.BusinessLogic.Client;
using Strive.Common;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Admin.API.Controllers
{
    [Authorize]
    [Route("Admin/[Controller]")]
    public class ClientController : StriveControllerBase<IClientBpl>
    {
        public ClientController(IClientBpl clientBpl) : base(clientBpl) { }

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

        [HttpGet]
        [Route("GetAll")]
        public Result GetAllClient() => _bplManager.GetAllClient();

        [HttpGet]
        [Route("GetClientById")]
        public Result GetClientById(int id) => _bplManager.GetClientById(id);

    }
}
