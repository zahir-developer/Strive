using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strive.BusinessEntities.Client;
using Strive.BusinessEntities.DTO.Client;
using Strive.BusinessEntities.DTO.Vehicle;
using Strive.BusinessEntities.DTO.User;
using Strive.Common;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.ViewModel;

namespace Strive.BusinessLogic.Client
{
    public interface IClientBpl
    {
        //Result SaveClientDetails(ClientView lstClient);
        Result SaveClientDetails(ClientDto client);
        Result GetAllClient(SearchDto searchDto);
        Result DeleteClient(int clientId);
        Result GetClientById(int? clientId);
        Result GetClientVehicleById(int clientId);
        Result UpdateClientVehicle(ClientDto vehicle);
        Result UpdateAccountBalance(ClientAmountUpdateDto clientAmountUpdate);
        Result GetClientSearch(ClientSearchDto search);
        Result GetClientCodes();
        Result GetStatementByClientId(int id);
        Result GetHistoryByClientId(int id);

        Result IsClientName(ClientNameDto clientNameDto);

        Result GetAllClientName(string name);
        Result ClientEmailExist(string email);
        List<ClientEmailBlastViewModel> ClientExport(EmailBlastDto emailBlast);

        Result ClientCSVExport(EmailBlastDto emailBlast);
        Result SaveCreditDetails(CreditDTO client);
        Result GetCreditAccountBalanceHistory(string clientId);
        Result AddCreditAccountHistory(CreditHistoryDTO addGiftCardHistory);
        Result UpdateCreditAccountHistory(CreditHistoryDTO updateGiftCardHistory);
        bool SendClientEmail();
    }
}
