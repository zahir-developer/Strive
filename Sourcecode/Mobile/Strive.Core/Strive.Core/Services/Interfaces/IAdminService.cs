using System;
using System.Threading.Tasks;
using Strive.Core.Models;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;

namespace Strive.Core.Services.Interfaces
{
    public interface IAdminService
    {
        Task<object> Login(string username, string password);

        Task<EmployeeResultData> EmployeeLogin(EmployeeLoginRequest request);

        Task<EmployeeResultData> CustomerLogin(CustomerLoginRequest loginRequest);

        Task<BaseResponse> CustomerSignUp(CustomerSignUp signUpRequest);

        Task<string> CustomerForgotPassword(string email);
    }
}
