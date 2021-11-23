using System;
using System.Threading.Tasks;
using Strive.Core.Models.Customer;

namespace Strive.Core.Services.Interfaces
{
    public interface IGeneralApiService
    {
        //Task<LocationsResponse> GetLocations();
        //Task<MakeResponse> GetAllMake();
        Task<GlobalDataResponse> GetGlobalData(string dataType);
       // Task<LocationWashTimeResponse> GetLocationWashTime(long locationId);
        //Task<LocationWashTimeResponse> GetAllLocationWashTime();
    }

}
