using System;
using System.Threading.Tasks;

namespace Strive.Core.Services.Interfaces
{
    public interface IAdminService
    {
        Task<object> Login(string username, string password);
    }
}
