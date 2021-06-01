using System;
using System.Threading.Tasks;
using Strive.Core.Models.TimInventory;

namespace Strive.Core.Services.Interfaces
{
    public interface ILocationService
    {
        Task<Locations> GetAllLocationAddress();
    }
}
