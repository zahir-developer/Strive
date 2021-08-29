﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public interface IWashApiService
    {
        Task<ServiceResponse> GetAllSericeDetails(int locationId);
        Task<TicketResponse> GetWashTime(int locationId);
        Task<TicketResponse> GetTicketNumber(int locationId);
        Task<BaseResponse> CreateService(CreateServiceRequest req);
        Task<EmployeeListResponse> GetDetailEmployees(GetDetailEmployeeReq req);
        Task<BaseResponse> SendEmail(string email, string subject, string body);
        Task<AvailableScheduleResponse> GetAvailablilityScheduleTime(GetAvailableScheduleReq req);
        Task<GetDetailsSercviesResponse> GetDetailServices(long ClientId);
    }

    public class WashApiService : IWashApiService
    {
        static readonly IApiService apiService = SingleTon.ApiService;

        public Task<ServiceResponse> GetAllSericeDetails(int locationId)
        {
            var parameters = new Dictionary<string, string>() { { nameof(locationId), locationId.ToString() } };
            return apiService.DoApiCall<ServiceResponse>(Urls.ALL_SERVICE_DETAILS, HttpMethod.Get, parameters);
        }

        public Task<TicketResponse> GetWashTime(int locationId)
        {
            var url = Urls.WASH_TIME + locationId;
            return apiService.DoApiCall<TicketResponse>(url);
        }

        public Task<TicketResponse> GetTicketNumber(int locationId)
        {
            var url = Urls.TICKET_NUMBER + locationId;
            return apiService.DoApiCall<TicketResponse>(url);
        }

        public Task<BaseResponse> CreateService(CreateServiceRequest req)
        {
            return apiService.DoApiCall<BaseResponse>(Urls.CREATE_SERVICE, HttpMethod.Post, null, req);
        }

        // Return current logged-in employees
        public Task<EmployeeListResponse> GetDetailEmployees(GetDetailEmployeeReq req)
        {
            return apiService.DoApiCall<EmployeeListResponse>(Urls.DETAILER_EMPLOYEES, HttpMethod.Post, null, req);
        }

        public Task<BaseResponse> SendEmail(string email, string subject, string body)
        {
            var parameters = new Dictionary<string, string>() { { nameof(email), email }, { nameof(subject), subject }, { nameof(body), body } };
            return apiService.DoApiCall<BaseResponse>(Urls.SEND_EMAIL, HttpMethod.Post, parameters);
        }

        public Task<AvailableScheduleResponse> GetAvailablilityScheduleTime(GetAvailableScheduleReq req)
        {
            return apiService.DoApiCall<AvailableScheduleResponse>(Urls.GET_AVAILABLILITY_SCHEDULE_TIME, HttpMethod.Post, null, req);
        }

        public Task<GetDetailsSercviesResponse> GetDetailServices(long ClientId)
        {
            var parameters = new Dictionary<string, string>() { { nameof(ClientId), ClientId.ToString() } };
            return apiService.DoApiCall<GetDetailsSercviesResponse>(Urls.GET_DETAIL_SERVICES, HttpMethod.Get, parameters);
        }
    }
}
