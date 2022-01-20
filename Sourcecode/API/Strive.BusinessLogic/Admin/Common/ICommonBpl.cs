using Strive.BusinessEntities;
using Strive.BusinessEntities.City;
using Strive.BusinessEntities.DTO;
using Strive.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Common
{
    public interface ICommonBpl
    {
        Result GetAllCodes();
        Result GetCodesByCategory(GlobalCodes codeCategory);
        Result GetCodesByCategory(int codeCategoryId);
        Task<Result> GetWeather();
        Task<Result> CreateLocationForWeatherPortal();
        Result GetAllEmail();
        Result GetSearchResult<T>(string searchTerm);
        Result GetEmailIdExist(string emailId);
        Result GetCityByStateId(int stateId);
        Result GetTicketNumber(int locationId);
        Result GetModelByMakeId(int makeId);
        Result GetAllMake();
        Result GetUpchargeByType(UpchargeDto upchargeDto);
        //void SendMultipleMail(string email, string body, string subject);
        void SendEmail(HtmlTemplate htmlTemplate, string emailId, Dictionary<string, string> keyValues, string sub);
        string Template(string templateName);
        Result GetVehiclePrint(PrintTicketDto printTicketDto);
        Result GetCustomerPrint(PrintTicketDto printTicketDto);
    }
}