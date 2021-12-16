using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Strive.Core.Models;
using Strive.Core.Models.Customer;
using Strive.Core.Models.Customer.Schedule;
using Strive.Core.Models.Employee.CheckOut;
using Strive.Core.Models.Employee.Collisions;
using Strive.Core.Models.Employee.Common;
using Strive.Core.Models.Employee.Documents;
using Strive.Core.Models.Employee.Messenger.MessengerContacts;
using Strive.Core.Models.Employee.PayRoll;
using Strive.Core.Models.Employee.PersonalDetails;
using Strive.Core.Models.Owner;
using Strive.Core.Models.TimInventory;
using EditProduct = Strive.Core.Models.TimInventory.Product_Id;

namespace Strive.Core.Services.Interfaces
{
    public interface IAdminService
    {
        Task<object> Login(string username, string password);

        Task<EmployeeResultData> EmployeeLogin(EmployeeLoginRequest request);

        Task<CustomerLoginResponse> CustomerLogin(CustomerLoginRequest loginRequest);

        Task<CustomerResponse> CustomerSignUp(CustomerSignUp signUpRequest);

        Task<CustomerResponse> CustomerForgotPassword(string email);

        Task<CustomerResponse> CustomerVerifyOTP(CustomerVerifyOTPRequest otpRequest);

        Task<CustomerResponse> CustomerConfirmPassword(CustomerResetPassword resetPasswordRequest);

        Task<TimeClockRootList> GetClockInStatus(TimeClockRequest request);

        Task<DeleteResponse> SaveClockInTime(TimeClockSave ClockInRequest);

        Task<DealsList> GetAllDeals();

        Task<ClientDeals> GetClientDeal(int clientid, string date, int dealid);

        Task<Products> GetAllProducts(ProductSearches searchQuery);

        Task<Vendors> GetAllVendors();

        Task<LocationProd> GetAllLocationName();

        Task<ProductType> GetProductType();

        Task<ProductType> GetCodes();

        Task<PostResponse> AddProduct(AddProduct product);

        Task<DeleteResponse> DeleteProduct(int Id);

        Task<EditProduct.ProductDetail_Id> GetProductByID(int Id);

        Task<PostResponse> UpdateProduct(AddProduct product);

        Task<PostResponse> ProductRequest(ProductRequest product);

        Task<PostResponse> UpdateProdQuantity(int Id, int quantity);

        Task<ClientResponse> GetAllClient(ClientRequest clientRequest);

        Task<CustomerPersonalInfo> GetClientById(int Id);

        Task<ProductsSearch> SearchProduct(string productName);

        Task<modelUpchargeResponse> GetModelUpcharge(modelUpcharge request);

        Task<MembershipServiceList> GetMembershipServiceList();

        Task<PostResponseBool> SaveVehicleMembership(ClientVehicleRoot clientVehicle);

        Task<PostResponse> DeleteVehicleMembership(deleteMembership deleteMembership);

        Task<ClientStatus> GetClientDetail(int ClientId);

        Task<VehicleList> GetClientVehicle(int ClientId);

        Task<ClientVehicleRootView> GetVehicleMembership(int VehicleId);

        Task<ServiceList> GetVehicleServices();

        Task<SelectedServiceList> GetSelectedMembershipServices(int MembershipId);

        Task<ClientsSearch> SearchClient(string ClientName);

        Task<CustomerResponse> SaveClientInfo(CustomerInfoModel infoModel);
        Task<CustomerResponse> SaveClientInfo(CustomerUpdateInfo infoModel);

        Task<VehicleCodes> GetVehicleCodes();

        Task<MakeList> GetMakeList();
        Task<ModelList> GetModelList(int Id);

        Task<GeneralResponse> AddCustomerVehicle(CustomerVehicles addVehicle);

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

        Task<TermsDocument> TermsDocuments(int doctypeid, string doctype);

        Task<DeleteResponse> DeleteDocuments(int documentID);

        Task<CheckoutDetails> CheckOutVehicleDetails(GetAllEmployeeDetail_Request EmployeeID);

        Task<holdCheckoutResponse> CheckOutHold(holdCheckoutReq holdReq);

        Task<holdCheckoutResponse> CheckOutComplete(completeCheckoutReq completeReq);

        Task<CheckoutResponse> DoCheckout(doCheckoutReq checkoutReq);

        Task<ScheduleModel> GetSchedulePastService(int clientID);

        Task<PostResponse> ScheduleDetail(DetailSchedule detailScheduleRequest);

        Task<AvailableServicesModel> GetScheduleServices(int LocationID);
        
        Task<AvailableScheduleSlots> GetScheduleSlots(ScheduleSlotInfo slotInfo);

        Task<ticketNumber> GetTicketNumber(int locationId);

        Task<PostResponseBool> UpdateEmployeePersonalDetails(UpdatePersonalDetails employeeInfo);

        Task<employeeSchedule> GetScheduleList(ScheduleRequest scheduleRequest);

        Task<StatisticsData> getDashboardServices(StatisticRequest statisticsRequest);

        Task<ScheduleModel> getDashboardSchedule(string jobDate, int locationId);

        Task<VehicleDiscountDetail> GetVehicleDiscountDetail(int ClientId, int VehicleId);

        Task<PayRollRateViewModel> GetPayRollStatus(string StartDate, String EndDate, int EmpId, int LocationId);

        Task<PayRoll> GetPayRoll(string StartDate, string EndDate, int EmpId, int LocationId);

        Task<UploadedDocument> AddDocumentDetails(AddDocument addDocument);

        Task<ProductType> GetCodesByCategory();

        Task<AddClientDealResponse> AddClientDeal(AddClientDeal clientDeal);

    }
}
