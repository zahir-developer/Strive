using Strive.Core.Models.Customer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Strive.Core.Services.Interfaces
{
    public interface ICarwashLocationService
    {
        Task<Locations> GetAllCarWashLocations();
        Task<washLocations> GetAllLocationStatus(LocationStatusReq request);
    }
}
