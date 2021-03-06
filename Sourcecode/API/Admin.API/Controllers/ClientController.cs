using Admin.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Client;
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
        [Route("InsertClientDetails")]
        public Result InsertClientDetails([FromBody] ClientDto client) => _bplManager.SaveClientDetails(client);

        [HttpPost]
        [Route("UpdateClientVehicle")]
        public Result ClientVehicleSave([FromBody] ClientDto client) => _bplManager.UpdateClientVehicle(client);

        [HttpPost]
        [Route("GetAll")]
        public Result GetAllClient([FromBody] SearchDto  searchDto) =>_bplManager.GetAllClient(searchDto);

        
        [HttpDelete]
        [Route("{clientId}")]
        public Result DeleteClient(int clientId)
        {
            return _bplManager.DeleteClient(clientId);
        }

        [HttpPost]
        [Route("UpdateAccountBalance")]
        public Result UpdateAccountBalance([FromBody] ClientAmountUpdateDto clientAmountUpdate) => _bplManager.UpdateAccountBalance(clientAmountUpdate);

        [HttpGet]
        [Route("GetClientById/{clientId}")]
        public Result GetClientById(int? clientId)
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

        [HttpPost]
        [Route("IsClientName")]
        public Result IsClientName([FromBody]ClientNameDto clientNameDto) => _bplManager.IsClientName(clientNameDto);

        [HttpGet]
        [Route("GetAllName/{name}")]
        public Result GetAllClientName(string name)
        {
            return _bplManager.GetAllClientName(name);

        }
        [HttpGet]
        [Route("ClientEmailExist")]
        public Result ClientEmailExist(string email)
        {

            return _bplManager.ClientEmailExist(email);



        }


    }
}
