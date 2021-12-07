using Admin.API.Helpers;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessLogic.Client;
using Strive.Common;
using System.IO;
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
        [Route("InsertCreditDetails")]
        public Result InsertCreditDetails([FromBody] CreditDTO credit) => _bplManager.SaveCreditDetails(credit);

        [HttpPost]
        [Route("AddCreditAccountHistory")]
        public Result AddCreditAccountHistory([FromBody] CreditHistoryDTO addCreditAccountHistory) => _bplManager.AddCreditAccountHistory(addCreditAccountHistory);

        [HttpPost]
        [Route("UpdateCreditAccountHistory")]
        public Result UpdateCreditAccountHistory([FromBody] CreditHistoryDTO updateCreditAccountHistory) => _bplManager.UpdateCreditAccountHistory(updateCreditAccountHistory);

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

        [HttpPost]
        [Route("EmailBlast")]
        public IActionResult GetClientList([FromBody] EmailBlastDto emailBlast)
        {
            var result = _bplManager.ClientExport(emailBlast);
            if (result.Count > 0)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Client List");
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "First Name";
                    worksheet.Cell(currentRow, 2).Value = "Last Name";
                    worksheet.Cell(currentRow, 3).Value = "Email";
                    worksheet.Cell(currentRow, 4).Value = "Client Type";
                    worksheet.Cell(currentRow, 5).Value = "VehicleId";
                    worksheet.Cell(currentRow, 6).Value = "Vehicle Barcode";
                    worksheet.Cell(currentRow, 7).Value = "IsMembership";
                    if (result != null)
                    {
                        foreach (var client in result)
                        {
                            currentRow++;
                            worksheet.Cell(currentRow, 1).Value = client.FirstName;
                            worksheet.Cell(currentRow, 2).Value = client.LastName;
                            worksheet.Cell(currentRow, 3).Value = client.Email;
                            worksheet.Cell(currentRow, 4).Value = client.Type;
                            worksheet.Cell(currentRow, 5).Value = client.VehicleId;
                            worksheet.Cell(currentRow, 6).Value = client.barcode;
                            worksheet.Cell(currentRow, 7).Value = client.IsMembership;
                        }

                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "ClientLsit.xlsx");
                    }
                }
            }
            else
            {
                var content = new byte[0];
                return File(
                           content,
                           "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                           "ClientLsit.xlsx");
            }
        }

        [HttpPost]
        [Route("EmailBlastCSV")]

        public Result GetClientListCSV([FromBody] EmailBlastDto emailBlast) => _bplManager.ClientCSVExport(emailBlast);

        [HttpGet]
        [Route("GetCreditAccountBalanceHistory/{clientId}")]
        public Result GetCreditAccountBalanceHistory(string clientId) => _bplManager.GetCreditAccountBalanceHistory(clientId);

        [HttpPost]
        [Route("SendClientEmail")]
        public Result SendClientEmail() => _bplManager.SendClientEmail();
    }
}
