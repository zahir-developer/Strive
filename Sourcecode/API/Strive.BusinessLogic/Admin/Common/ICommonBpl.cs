﻿using Strive.Common;
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

        string GetTicketNumber(int locationId);
        Result GetModelByMakeId(int makeId);
    }
}