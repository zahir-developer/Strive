﻿using System;
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

        Task<CustomerResponse> CustomerSignUp(CustomerSignUp signUpRequest);

        Task<CustomerResponse> CustomerForgotPassword(string email);

        Task<CustomerResponse> CustomerVerifyOTP(CustomerVerifyOTPRequest otpRequest);

        Task<CustomerResponse> CustomerConfirmPassword(CustomerResetPassword resetPasswordRequest);

        Task<TimeClockRootList> GetClockInStatus(TimeClockRequest request);

        Task<DeleteResponse> SaveClockInTime(TimeClockRoot ClockInRequest);

        Task<Products> GetAllProducts();

        Task<Vendors> GetAllVendors();

        Task<PostResponse> AddProduct(ProductDetail product);

        Task<DeleteResponse> DeleteProduct(int Id);

        Task<PostResponse> UpdateProduct(ProductDetail product);

        Task<Clients> GetAllClient();

        Task<CustomerPersonalInfo> GetClientById(int Id);

        Task<ProductsSearch> SearchProduct(string productName);
    }
}
