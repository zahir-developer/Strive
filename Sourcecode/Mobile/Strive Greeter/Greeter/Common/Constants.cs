using System.Drawing;

namespace Greeter.Common
{
    public class Constants
    {
        // 12 hour clock
        internal const string TIME_FORMAT = "hh:mm tt";
        internal const string DATE_FORMAT = "dd/MM/yyyy";
        internal const string DATE_FORMAT_FOR_API = "yyyy-MM-dd";
        internal const string DATE_TIME_FORMAT_FOR_API = "yyyy-MM-dd HH:mm:ss";

        internal const short PAGINATION_LIMIT = 10;

        internal const string APP_CENTER_SECTRET_KEY = "4e8a243b-8318-4735-980d-439d2ba8ecd4";
        internal const string INFEA_DEVELOPER_KEY = "lkAfY0qI6092/y9YWsMZCeHshSjZK6QMo2z8Hqx6HU+SeLVCMILsSKK9JX+33hqkm6HKBVosqMTGi4YRkXPODg==";
    }

    class BundleIds
    {
        internal const string STRIVE_GREETER = "com.strive.greeter";
        internal const string MOBILE_UNIT = "com.strive.mobileunit";
    }

    class AppNames
    {
        internal const string STRIVE_GREETER = "Greeter";
        internal const string MOBILE_UNIT = "Mobile Unit";
    }

    public class Urls
    {
        ////Strive Dev By Zahir
        //internal const string STRIVE_DEV_BASE_URL = "https://strivedev.azurewebsites.net";

        //// Mamooth Dev
        //internal const string  MAMMOTH_DEV_BASE_URL = "https://mammothuatapi-dev.azurewebsites.net";

        ////QA
        //internal const string MAMMOTH_DEV_QA_BASE_URL = "https://mammothuatapi-qa.azurewebsites.net";

        ////Live
        //internal const string LIVE_BASE_URL = "https://strivedev.azurewebsites.net";

        //Strive Dev By Zahir
        //internal const string BASE_URL = "https://strivedev.azurewebsites.net";

        // Mamooth Dev
        //internal const string BASE_URL = "https://mammothuatapi-dev.azurewebsites.net";

        // Mammoth QA
        internal const string BASE_URL = "https://mammothuatapi-qa.azurewebsites.net";

        // Client Url by Zahir - not working - don't use this one as of now
        //internal const string BASE_URL = "https://mammothuat.azurewebsites.net";

        // Client
        //internal const string BASE_URL = "https://mammothuatapi.azurewebsites.net";

        // User
        internal const string LOGIN = "/Auth/Login";
        internal const string LOCATIONS = "/Admin/Location/GetAll";
        internal const string REFRESH_TOKEN = "/Auth/Refresh";

        // Service
        // Url Format Example : /Admin/Washes/GetByBarCode/{barcode}
        internal const string BARCODE = "/Admin/Washes/GetByBarCode/";
        // Url Format Example : /Admin/Common/GetCodesByCategory/{globalCode}
        internal const string GLOBAL_DATA = "/Admin/Common/GetCodesByCategory/";
        internal const string ALL_MAKE = "/Admin/Common/GetAllMake";
        // Url Format Example :/Admin/Common/GetModelById/{makeId}
        internal const string MODELS_BY_MAKE = "/Admin/Common/GetModelById/";
        internal const string ALL_SERVICE_DETAILS = "/Admin/ServiceSetup/GetAllServiceDetail";
        //  Url Format Example : /Admin/Common/GetTicketNumber/{locationId}
        internal const string TICKET_NUMBER = "/Admin/Common/GetTicketNumber/";
        internal const string CREATE_SERVICE = "/Admin/Washes/UpdateWashTime";
        //internal const string CREATE_DETAIL_SERVICE = "/Admin/Details/UpdateDetails";
        
        internal const string SEND_EMAIL = "/Admin/Common/SendMail";
        internal const string GET_AVAILABLILITY_SCHEDULE_TIME = "/Admin/Dashboard/GetAvailablilityScheduleTime";
        internal const string WASH_TIME = "/Admin/Washes/GetWashTimeDetail/";

        // Employees
        internal const string DETAILER_EMPLOYEES = "/Admin/TimeClock/GetClockedInDetailer";
        internal const string CONTACTS_LIST = "/Admin/Employee/GetAllEmployeeDetail";

        // Payment
        internal const string PAYMENT_AUTH = "/Payroll/PaymentGateway/Auth";
        internal const string PAYMENT_CAPTURE = "/Payroll/PaymentGateway/Capture";
        internal const string ADD_PAYMENT = "/Admin/Sales/AddPayment";

        // Checkout
        internal const string CHECKOUTS = "/Admin/Checkout/GetAllCheckoutDetails";
        internal const string HOLD_CHECKOUT = "/Admin/Checkout/UpdateJobStatusHold";
        internal const string COMPLETE_CHECKOUT = "/Admin/Checkout/UpdateJobStatusComplete";
        internal const string DO_CHECKOUT = "/Admin/Checkout/UpdateCheckoutDetails";

        //internal const string URL_MESSENGER_PERSONAL_CHATS = "/Admin/Messenger/GetChatMessage";
        internal const string RECENT_CHAT_LIST = "/Admin/Messenger/GetChatEmployeeList/";
        internal const string CREATE_GROUP = "/Admin/Messenger/CreateChatGroup";
        // Url Format Example : /Admin/Messenger/GetChatGroupEmployeelist/{chatGroupId}
        internal const string GROUP_USERS = "/Admin/Messenger/GetChatGroupEmployeelist/";
        // Url Format Example : Admin/Messenger/AddEmployeeToGroup/{employeeId}/{communicationId}
        internal const string ADD_USER_TO_GROUP = "/Admin/Messenger/AddEmployeeToGroup/";
        internal const string REMOVE_USER_FROM_GROUP = "/Admin/Messenger/DeleteChatGroupUser/";
        internal const string CHAT_MESSAGES = "/Admin/Messenger/GetChatMessage";
        internal const string SEND_MESSAGE = "/Admin/Messenger/SendChatMessage";
        //internal const string GET_DETAIL_SERVICES = "/Admin/Details/GetAllDetails";
        internal const string GET_LOCATION_WASH_TIME = "/Admin/Washes/GetAllLocationWashTime/";
        internal const string LAST_VISIT_SERVICE = "/Admin/Washes/GetLastServiceVisit";
        internal const string GET_UPCHARGE = "/Admin/Common/GetUpchargeType";
        internal const string ASSIGN_EMPLOYEE_FOR_DETAIL_SERVICE = "/Admin/Details/AddServiceEmployee";
        internal const string CREATE_DETAIL_SERVICE = "/Admin/Details/AddDetails";
        internal const string GET_VEHICLE_MEMBERSHIP_DETAILS = "/Admin/Vehicle/GetVehicleMembershipDetailsByVehicleId";

        internal const string GET_DETAIL_SERVICE = "/Admin/Details/GetDetailsById/";
    }

    public class UIConstants
    {
        internal const float TEXT_FIELD_HORIZONTAL_PADDING = 15;
        internal const float TEXT_FIELD_RIGHT_BUTTON_PADDING = 60;
    }

    public static class StoryBoardNames
    {
        internal const string USER = "UserStoryboard";
        internal const string HOME = "HomeStoryboard";
    }

    public class ImageNames
    {
        internal const string SPLASH_BG = "splash_bg";
        internal const string CHECKOUT = "checkout";
        internal const string DOLLAR = "dollar";
        internal const string HOME = "home";
        internal const string CAR = "car";
        internal const string SEND = "send";
        internal const string TICK = "tick";
        internal const string SEARCH = "search";
        internal const string CLOSE_SOLID = "close_red";
        internal const string ADD_CIRCLE = "add_circle";
        internal const string More = "more";
        internal const string MESSAGE = "messaging";
        internal const string PAID = "paid";
        internal const string CHECKOUT_ACTION = "checkout_action";
        internal const string COMPLETE = "complete";
        internal const string ADD_GROUP = "add_group";
        internal const string REFRESH = "refresh";
        internal const string PARTICIPANTS = "participants";
        internal const string MEMBER = "badge_star";
    }

    public class ColorNames
    {
        internal const string APP_COLOR = "app_color";
        internal const string APP_SECONDARY_COLOR = "app_secondary_color";
    }

    public class Colors
    {
        internal static Color APP_BASE_COLOR = Color.FromArgb(29, 201, 183);
        internal static Color PRINT_COLOR = Color.FromArgb(36, 72, 154);
    }

    public class Messages
    {
        internal const string SERVICE_RECEIPT_SUBJECT = "Wash Receipt";
        internal const string DETAIL_RECEIPT_SUBJECT = "Details Receipt";
        internal const string DRIVE_UP = "Drive up";
        internal const string BARCODE_EMPTY = "Please enter the barcode";
        internal const string USER_ID_AND_PSWD_EMPTY = "Please enter your user id and password";
        internal const string USER_ID_EMPTY = "Please enter your user id";
        internal const string PSWD_EMPTY = "Please enter your password";
        internal const string LOCATION_EMPTY = "Please select the location";
        internal const string BARCODE_WRONG = "Invalid barcode";
        internal const string SERVICE_CREATED_MSG = "Service created successfully";
        internal const string SERVICE_CREATION_ISSUE = "Couldn't generate Service";
        internal const string TICKET_CERATION_ISSUE = "Couldn't generate ticket";
        internal const string WARNING_FOR_MANDATORY_FILEDS_IN_SERVICE = "Please fill all mandatory fields make, model, color and {0}";
        internal const string WAH_PACKAGE = "wash package";
        internal const string DETAIL_PACKAGE = "detail package";
        internal const string EMAIL_SENT_MSG = "Email sent successfully";
        internal const string EMAIL_WARNING = "Please enter the valid email id";
        internal const string EMAIL_MISSING = "Please enter the email id";
        internal const string EMPLOYEE_MISSING = "Please select the employee";
        internal const string EMPLOYEE_ASSIGNED_SUCCESS_MSG = "Employee assigned successfully";

        // Network
        internal const string NO_INTERNET_MSG = "Please check your internet connection and try again";
        internal const string INTERNAL_SERVER_ERROR = "Internal Server Error";
        internal const string BAD_REQUEST = "Bad request";
        internal const string SESSION_TIMED_OUT = "Your session was timed out.Please try login again";

        internal const string HOLD_VERIFICATION_MSG = "Are you sure want to change the status to hold?";
        internal const string SERVICE_HOLD_SUCCESS_MSG = "Service status changed to hold successfully";

        internal const string COMPLETE_VERIFICATION_MSG = "Are you sure want to change the status to complete?";
        internal const string SERVICE_COMPLETED_SUCCESS_MSG = "Service has been completed successfully";

        internal const string CHECKOUT_VERIFICATION_MSG = "Are you sure want to change the status to checkout?";
        internal const string SERVICE_CHECKED_OUT_SUCCESS_MSG = "Vehicle has been checked out successfully";

        internal const string CARD_DETAILS_EMPTY_MISSING_MSG = "Please fill your all card details";

        internal const string CARD_NUMBER_16_DIGIT_WARNING_MSG = "Card number should be in 16 digits";

        internal const string GROUP_NAME_EMPTY = "Plesae enter the group name";
        internal const string NO_GROUP_PARTICIPANTS = "Plesae add atleast one participant for a group";
        internal const string GROUP_CREATED_MSG = "Group created successfully";
        internal const string REMOVE_USER_FROM_GROUP_CONFIRMATION_MSG = "Are you sure you want to remove this user from this group?";
        internal const string USER_REMOVED_SUCCESS_MSG = "User removed successfully";
        internal const string USER_ADDED_SUCCESS_MSG = "User added successfully";
        internal const string NOT_PAID_ALERT_MSG = "Only Paid ticket can be checkedout!";
        internal const string NOT_COMPLETED_ALERT_MSG = "Only completed ticket can be checkedout!";
        internal const string NO_SLOTS = "No slots avaibale";
        internal const string ADD_EMAIL_ACCOUNT = "Please add your account in mail application";
        internal const string MEMBERSHIP_MESSAGE = " This customer has membership for this vechicle. Please ask him to pay at the cashier inside.";

        internal const string HOLD = "Hold";
        internal const string COMPLETE = "Complete";
        internal const string CHECKOUT = "Checkout";
        internal const string LOCATION = "Location";
        internal const string SERVICE = "Service";
        internal const string EMAIL = "Email";
        internal const string ASSIGN = "Assign";
        internal const string REMOVE_USER_FROM_GROUP_TITLE = "Remove User";
        internal const string ADD_USER_FROM_TO_GROUP_TITLE = "Add User";
        internal const string GROUP_TITLE = " Group";
        internal const string NOT_PAID = " Not Paid";
        internal const string IN_PROGRESS = " In Progress";
    }

    //public class StatusCodes
    //{
    //    internal const int SUCCESS = 200;
    //    internal const int BAD_REQUEST = 400;
    //    internal const int INTERNAL_SERVER_ERROR = 500;
    //    internal const int UN_AUTHORIZED = 401;
    //}
}