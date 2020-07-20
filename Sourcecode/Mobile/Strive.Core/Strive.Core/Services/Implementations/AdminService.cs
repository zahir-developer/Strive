﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using MvvmCross;
using Strive.Core.Models;
using Strive.Core.Models.Customer;
using Strive.Core.Models.TimInventory;
using Strive.Core.Rest.Interfaces;
using Strive.Core.Services.Interfaces;
using Strive.Core.Utils;

namespace Strive.Core.Services.Implementations
{
    public class AdminService : IAdminService
    {
        public IRestClient _restClient = Mvx.IoCProvider.Resolve<IRestClient>();

        public AdminService()
        {
        }

        public async Task<object> Login(string username, string password)
        {
            return await _restClient.MakeApiCall<object>(ApiUtils.URL_LOGIN_EMPLOYEE, HttpMethod.Post, new User());
        }

        public async Task<EmployeeResultData> EmployeeLogin(EmployeeLoginRequest request)
        {
            return await _restClient.MakeApiCall<EmployeeResultData>(ApiUtils.URL_LOGIN_EMPLOYEE, HttpMethod.Post, request);
        }

        public async Task<EmployeeResultData> CustomerLogin(CustomerLoginRequest loginRequest)
        {
            return await _restClient.MakeApiCall<EmployeeResultData>(ApiUtils.URL_LOGIN_EMPLOYEE, HttpMethod.Post, loginRequest);
        }
    }
}
