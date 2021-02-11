﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Strive.Core.Models;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.Employee.Collisions;
using Strive.Core.Models.Employee.Common;
using Strive.Core.Models.Employee.Documents;
using Strive.Core.Models.Employee.PersonalDetails;
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

        Task<MembershipServiceList> GetMembershipServiceList();

        Task<PostResponseBool> SaveVehicleMembership(ClientVehicleRoot clientVehicle);

        Task<ClientStatus> GetClientDetail(int ClientId);

        Task<VehicleList> GetClientVehicle(int ClientId);

        Task<ClientVehicleRootView> GetVehicleMembership(int VehicleId);

        Task<ServiceList> GetVehicleServices();

        Task<SelectedServiceList> GetSelectedMembershipServices(int MembershipId);

        Task<ClientsSearch> SearchClient(string ClientName);

        Task<CustomerResponse> SaveClientInfo(CustomerInfoModel infoModel);
        Task<CustomerResponse> SaveClientInfo(CustomerUpdateInfo infoModel);

        Task<VehicleCodes> GetVehicleCodes();

        Task<GeneralResponse> AddCustomerVehicle(AddCustomerVehicle addVehicle);

        Task<GeneralResponse> DeleteCustomerVehicle(int VehicleID);

        Task<CustomerCompleteDetails> GetVehicleCompleteDetails(int VehicleID);

        Task<PastClientServices> GetPastClientServices(int ClientID);

        Task<PersonalDetails> GetPersonalDetails(int EmployeeID);

        Task<CommonCodes> GetCommonCodes(string CommonCodes);

        Task<PostResponse> AddCollisions(AddCollisions collisionDetails);

        Task<PostResponse> UpdateCollisions(AddCollisions collisionDetails);

        Task<PostResponse> DeleteCollisions(int liabilityID);

        Task<GetCollisions> GetCollisions(int liabilityID);

        Task<PostResponseBool> SaveDocuments(AddDocuments documents);

        Task<DownloadDocuments> DownloadDocuments(int documentID, string documentPassword);

        Task<CheckOutVehicleDetails> CheckOutVehicleDetails(int EmployeeID);
        
        Task<ScheduleModel> GetSchedulePastService(int clientID);
        
        Task<AvailableScheduleServicesModel> GetScheduleServices();
        
        Task<AvailableScheduleSlots> GetScheduleSlots(ScheduleSlotInfo slotInfo);
    
    }
}
