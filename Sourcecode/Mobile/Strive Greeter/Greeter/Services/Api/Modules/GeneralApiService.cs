using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Greeter.Common;
using Greeter.DTOs;
using Greeter.Services.Network;

namespace Greeter.Services.Api
{
    public interface IGeneralApiService
    {
        //Task<LocationsResponse> GetLocations();
        Task<MakeResponse> GetAllMake();
        Task<GlobalDataResponse> GetGlobalData(string dataType);
        Task<LocationWashTimeResponse> GetLocationWashTime(long locationId);
        Task<PrinterIp> GetPrinterIp(int locationid);
        Task<TicketModel> GetPrintDetails(long jobid);
        Task<GeneratedTicket> GetVehiclePrint(PrintContentRequest ticket);
        Task<GeneratedTicket> GetCustomerPrint(PrintContentRequest ticket);
        //Task<LocationWashTimeResponse> GetAllLocationWashTime();
    }

    public class GeneralApiService : IGeneralApiService
    {
        readonly IApiService apiService = SingleTon.ApiService;

        //public Task<LocationsResponse> GetLocations()
        //{
        //    return apiService.DoApiCall<LocationsResponse>(Urls.LOCATIONS);
        //}

        public Task<MakeResponse> GetAllMake()
        {
            return apiService.DoApiCall<MakeResponse>(Urls.ALL_MAKE);
        }

        public Task<GlobalDataResponse> GetGlobalData(string dataType)
        {
            var url = Urls.GLOBAL_DATA + dataType;
            return apiService.DoApiCall<GlobalDataResponse>(url);
        }

        public Task<LocationWashTimeResponse> GetLocationWashTime(long locationId = 0)
        {
            //string formattedCurrentDate = DateTime.Now.Date.ToString(Constants.DATE_FORMAT_FOR_API);
            string formattedCurrentDate = DateTime.Now.ToString(Constants.DATE_TIME_FORMAT_FOR_API);
            var parameters = new Dictionary<string, string>() { { nameof(locationId), locationId.ToString()} ,{ "date" , formattedCurrentDate} };
            return apiService.DoApiCall<LocationWashTimeResponse>(Urls.GET_LOCATION_WASH_TIME, HttpMethod.Get, parameters);
        }

        public Task<PrinterIp> GetPrinterIp(int locationid)
        {
            return apiService.DoApiCall<PrinterIp>(Urls.GET_PRINTER_IP+locationid, HttpMethod.Get);
        }

        public Task<TicketModel> GetPrintDetails(long jobid)
        {
            string url = Urls.GET_PRINT_DETAILS + "?jobId=" + jobid;
            return apiService.DoApiCall<TicketModel>(url, HttpMethod.Get);
        }
        public Task<GeneratedTicket> GetVehiclePrint(PrintContentRequest ticket)
        {
            return apiService.DoApiCall<GeneratedTicket>(Urls.FETCH_VEHICLE_TICKET , HttpMethod.Post, null ,ticket);
        }
        public Task<GeneratedTicket> GetCustomerPrint(PrintContentRequest ticket)
        {
            return apiService.DoApiCall<GeneratedTicket>(Urls.FETCH_CUSTOMER_TICKET, HttpMethod.Post, null, ticket);
        }

        //public Task<LocationWashTimeResponse> GetAllLocationWashTime()
        //{
        //    //string formattedCurrentDate = DateTime.Now.Date.ToString(Constants.DATE_FORMAT_FOR_API);
        //    string formattedCurrentDate = DateTime.Now.ToString();
        //    var parameters = new Dictionary<string, string>() { { "locationId", "0" }, { "date", formattedCurrentDate } };
        //    return apiService.DoApiCall<LocationWashTimeResponse>(Urls.GET_LOCATION_WASH_TIME, HttpMethod.Get, parameters);
        //}
    }
}
