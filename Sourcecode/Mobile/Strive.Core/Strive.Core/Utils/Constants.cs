using System;
namespace Strive.Core.Utils
{
    public class ApiUtils
    {
        // public const string BASE_URL = "http://10.0.2.2:60001";

        //public const string BASE_URL = "http://14.141.185.75:5004";
        public const string AZURE_URL = "http://40.114.79.101:5004";

        //Mammoth DEV/QA
        public const string AZURE_URL_TEST = "https://mammothuatapi-dev.azurewebsites.net"; 

        //Mammoth UAT
        //public const string AZURE_URL_TEST = "https://mammothuatapi.azurewebsites.net";


        public static string Token { get; set; }
          
        public const string URL_LOGIN_EMPLOYEE = "/Auth/Login";

        public const string URL_CUST_SIGN_UP = "/Auth/CreateLogin";

        public const string URL_CUST_FORGOT_PASSWORD = "/Auth/SendOTP";

        public const string URL_CUST_CONFIRM_PASSWORD = "/Auth/ResetPassword";

        public const string URL_CUST_VERIFY_OTP = "/Auth/VerfiyOTP";

        public const string URL_GET_ALL_LOCATION_ADDRESS = "/Admin/Location/GetAll";

        public const string URL_ALL_LOCATION_STATUS = "/Admin/Washes/GetAllLocationWashTime";

        public const string URL_GET_CLOCKIN_STATUS = "/Admin/TimeClock/TimeClockDetails";

        public const string URL_SAVE_CLOCKIN_TIME = "/Admin/TimeClock/Save";

        public const string URL_GET_ALLDEALS = "/Admin/DealSetup/GetAllDeals";

        public const string URL_GET_CLIENT_DEALS = "/Admin/DealSetup/GetClientDeal";

        public const string URL_GET_ALL_PRODUCTS = "/Admin/Product/GetAll";

        public const string URL_GET_ALL_VENDORS = "/Admin/Vendor/GetAll";

        public const string URL_GET_ALL_LOCATION_NAME = "/Admin/Location/GetAllLocationName";

        public const string URL_GET_PRODUCTTYPE = "/Admin/Common/GetCodesByCategory/PRODUCTTYPE";

        public const string URL_GET_ALLCODES = "/Admin/Common/GetCodesByCategory/ALL";

        public const string URL_GET_PRODUCTDETAIL_BYID = "/Admin/Product/GetProductDetailById";

        public const string URL_ADD_PRODUCT = "/Admin/Product/Add";

        public const string URL_DELETE_PRODUCT = "/Admin/Product/Delete";
        
        public const string URL_UPDATE_PRODUCT = "/Admin/Product/Update";

        public const string URL_UPDATE_PRODUCT_QUANTITY = "/Admin/Product/UpdateProductQuantity";

        public const string URL_SEARCH_PRODUCT = "/Admin/Product/GetProductSearch";

        public const string URL_PRODUCT_REQUEST = "/Admin/Product/ProductRequest";

        public const string URL_GET_PAST_SCHEDULE = "/Admin/Schedule/GetSchedule";

        public const string URL_GET_ALL_CLIENT = "/Admin/Client/GetAll";

        public const string URL_SEARCH_CLIENT = "/Admin/Client/GetClientSearch";

        public const string URL_MODEL_UPCHARGE = "/Admin/Common/GetUpchargeType";

        public const string URL_GET_ALL_SERVICE = "/Admin/MembershipSetup/GetAll";

        public const string URL_SAVE_VEHICLE_MEMBERSHIP = "/Admin/Vehicle/SaveClientVehicleMembership";

        public const string URL_DELETE_VEHICLE_MEMBERSHIP = "/Admin/Vehicle/DeleteVehicleMembership";

        public const string URL_GET_CLIENT_DETAIL = "/Admin/Client/GetClientById/";

        public const string URL_GET_CLIENT_VEHICLE = "/Admin/Vehicle/GetVehicleByClientId";

        public const string URL_GET_CLIENT_VEHICLE_MEMBERSHIP = "/Admin/Vehicle/GetVehicleMembershipDetailsByVehicleId";

        public const string URL_GET_CLIENT_VEHICLE_SERVICES = "/Admin/ServiceSetup/GetService";

        public const string URL_GET_CLIENT_VEHICLE_SERVICES_LIST = "/Admin/ServiceSetup/GetAllServiceDetail";

        public const string URL_GET_SELECTED_MEMBERSHIP_SERVICES = "/Admin/MembershipSetup/GetAllMembershipById/";

        public const string URL_GET_CLIENT_BY_ID = "/Admin/Client/GetClientById/{0}";

        public const string URL_SAVE_CLIENT_INFO = "/Admin/Client/UpdateClientVehicle";

        public const string URL_GET_VEHICLE_CODES = "/Admin/Vehicle/GetVehicleCodes";

        public const string URL_GET_MAKE_LIST = "/Admin/Common/GetAllMake";

        public const string URL_GET_MODEL_LIST = "/Admin/Common/GetModelById/{0}";

        public const string URL_GET_TICKET_NUMBER = "/Admin/Common/GetTicketNumber/{0}";

        public const string URL_UPDATE_VEHICLE_INFO = "/Admin/Vehicle/Update";

        public const string URL_ADD_VEHICLE_INFO = "/Admin/Vehicle/AddVehicle";

        public const string URL_GET_VEHICLE_COMPLETE_DETAILS = "/Admin/Vehicle/GetVehicleId";

        public const string URL_DELETE_VEHICLE_INFO = "/Admin/Vehicle/Delete";

        public const string URL_PAST_SERVICES_INFO = "/Admin/Vehicle/GetPastDetails/{0}";

        public const string URL_MESSENGER_CHAT_COMMUNICATION = "/Admin/Messenger/ChatCommunication";

        public const string URL_MESSENGER_RECENT_CONTACTS = "/Admin/Messenger/GetChatEmployeeList/{0}";

        public const string URL_MESSENGER_CONTACTS = "/Admin/Employee/GetAllEmployeeDetail";

        public const string URL_MESSENGER_PERSONAL_CHATS = "/Admin/Messenger/GetChatMessage";

        public const string URL_MESSENGER_SEND_CHAT_MESSAGE = "/Admin/Messenger/SendChatMessage";

        public const string URL_MESSENGER_CREATE_GROUP_CHAT = "/Admin/Messenger/CreateChatGroup";

        public const string URL_MESSENGER_PERSONAL_INFO = "/Admin/Employee/GetEmployeeById";

        public const string URL_COMMON_TYPES = "/Admin/Common/GetCodesByCategory/";

        public const string URL_GET_GROUP_PARTICIPANTS = "/Admin/Messenger/GetChatGroupEmployeelist/{0}";

        public const string URL_GROUP_PARTICIPANT_DELETE = "/Admin/Messenger/DeleteChatGroupUser/{0}";

        public const string URL_ADD_COLLISIONS = "/Admin/Collision/Add";

        public const string URL_UPDATE_COLLISIONS = "/Admin/Collision/Update";

        public const string URL_DELETE_COLLISIONS = "/Admin/Collision/Delete";

        public const string URL_GET_COLLISIONS = "/Admin/Collision/GetCollisionById/";

        public const string URL_SAVE_DOCUMENTS = "/Admin/Document/SaveDocument";

        public const string URL_CHECKOUT_DETAILS = "/Admin/Checkout/GetAllCheckoutDetails/";

        public const string URL_CHECKOUT_HOLD = "/Admin/Checkout/UpdateJobStatusHold/";

        public const string URL_CHECKOUT_COMPLETE = "/Admin/Checkout/UpdateJobStatusComplete/";

        public const string URL_CHECKOUT_UPDATE = "/Admin/Checkout/UpdateCheckoutDetails/";

        public const string URL_SCHEDULE_PAST_SERVICE = "/Admin/Details/GetAllJobByClientId";

        public const string URL_SCHEDULE_DETAILBAY = "/Admin/Details/AddDetails";

        public const string URL_SCHEDULE_SERVICES_AVAILABLE = "/Admin/Sales/GetAllServiceDetail";

        public const string URL_SCHEDULE_TIME_SLOTS = "/Admin/Dashboard/GetAvailablilityScheduleTime";

        public const string URL_EMPLOYEE_DOCUMENTS_ADD = "/Admin/Document/SaveEmployeeDocument";

        public const string URL_EMPLOYEE_DOCUMENTS_DOWNLOAD = "/Admin/Document/GetEmployeeDocumentById/";

        public const string URL_EMPLOYEE_DOCUMENTS_DELETE = "/Admin/Document/DeleteEmployeeDocument/";

        public const string URL_UPDATE_EMPLOYEE_PERSONAL_DETAILS = "/Admin/Employee/Update";

        public const string URL_GET_EMPLOYEE_SCHEDULE = "/Admin/Schedule/GetSchedule";

        public const string URL_GET_DASHBOARD_STATISTICS = "/Admin/Dashboard/GetDashboardStatistics";

        public const string URL_GET_CLIENT_VEHICLE_SERVICES_DISCOUNT = "/Admin/Vehicle/GetMembershipDiscountStatus";

        public const string URL_GET_TERMS_AND_CONDITIONS = "/Admin/Document/GetDocumentById/";

        public const string URL_GET_PAYROLL_STATUS = "/Admin/PayRoll/GetPayrollProcessStatus";

        public const string URL_GET_PAYROLL =   "/Admin/PayRoll/GetPayroll";

        public const string URL_GET_CLIENTCARDDETAIL = "/Admin/Client/GetClientCardDetailById";

        public const string URL_UPDATE_CLIENTCARD = "/Admin/Client/UpdateClientVehicle";

        public const string URL_ADD_CLIENTCARD = "/Admin/Client/InsertClientDetails";

        public const string URL_GET_DASHBORD = "/Admin/Details/GetAllDetails";

        public const string URL_CUSTOMER_SIGNUP = "https://mammothuat-qa.azurewebsites.net/#/signup?token=0A7E0CAA-DA62-4BF8-B83A-3F6625CDD6DE";

        //Payment
        internal const string PAYMENT_AUTH = "/Payroll/PaymentGateway/Auth";
        internal const string PAYMENT_AUTH_PROFILE = "/Payroll/PaymentGateway/AuthProfile";
        internal const string PAYMENT_CAPTURE = "/Payroll/PaymentGateway/Capture";
        internal const string PAYMENT_AUTHTIP = "/Payroll/PaymentGateway/AuthTips";
        internal const string ADD_PAYMENT = "/Admin/Sales/AddTipPayment";
        internal const string GLOBAL_DATA = "/Admin/Common/GetCodesByCategory/";

        //Document
        public const string URL_ADD_DOCUMENT = "/Admin/Document/AddDocument";
        public const string URL_GET_CODES_BY_CATEGORY = "/Admin/Common/GetCodesByCategory/DOCUMENTTYPE";

        //Deals
        public const string URL_ADD_CLIENT_DEAL = "/Admin/DealSetup/AddClientDeal";

        //Detailer
        public const string URL_GET_DETAILER_STATUS = "/Admin/Details/GetEmployeeAssignedDetail/";

        //CheckList

        public const string URL_GET_CHECKLIST = "/Admin/Checklist/ChecklistNotification";
        public const string URL_FINISH_CHECKLIST = "/Admin/Checklist/UpdateChecklistNotification";

    }
}
