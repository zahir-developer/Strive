using System.Drawing;

namespace Greeter.Common
{
    public class Constants
    {
        // 12 hour clock
        internal const string TIME_FORMAT = "hh:mm tt";
        internal const string DATE_FORMAT = "dd/MM/yyyy";

        internal const short PAGINATION_LIMIT = 10;
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
        internal const string BASE_URL = "https://mammothuatapi-dev.azurewebsites.net";

        // Mammoth QA
        //internal const string BASE_URL = "https://mammothuatapi-qa.azurewebsites.net";



        internal const string LOGIN = "/Auth/Login";
        internal const string LOCATIONS = "/Admin/Location/GetAll";
        internal const string REFRESH_TOKEN = "/Auth/Refresh";

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
        internal const string SEND_EMAIL = "/Admin/Common/SendMail";
        internal const string DETAILER_EMPLOYEES = "/Admin/TimeClock/GetClockedInDetailer";
        internal const string WASH_TIME = "/Admin/Washes/GetWashTimeDetail/";

        internal const string PAYMENT_AUTH = "/Payroll/PaymentGateway/Auth";
        internal const string PAYMENT_CAPTURE = "/Payroll/PaymentGateway/Capture";
        internal const string ADD_PAYMENT = "/Admin/Sales/AddPayment";

        internal const string CHECKOUTS = "/Admin/Checkout/GetAllCheckoutDetails";
        internal const string HOLD_CHECKOUT = "/Admin/Checkout/UpdateJobStatusHold";
        internal const string COMPLETE_CHECKOUT = "/Admin/Checkout/UpdateJobStatusComplete";
        internal const string DO_CHECKOUT = "/Admin/Checkout/UpdateCheckoutDetails";
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
    }

    public class ColorNames
    {
        internal const string APP_COLOR = "app_color";
        internal const string APP_SECONDARY_COLOR = "app_secondary_color";
    }

    public class Colors
    {
        internal static Color APP_BASE_COLOR = Color.FromArgb(29, 201, 183);
    }

    public class Messages
    {
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

        internal const string HOLD_VERIFICATION_MSG = "Are you sure want to change the status to hold?";
        internal const string SERVICE_HOLD_SUCCESS_MSG = "Service status changed to hold successfully";

        internal const string COMPLETE_VERIFICATION_MSG = "Are you sure want to change the status to complete?";
        internal const string SERVICE_COMPLETED_SUCCESS_MSG = "Service has been completed successfully";

        internal const string CHECKOUT_VERIFICATION_MSG = "Are you sure want to change the status to checkout?";
        internal const string SERVICE_CHECKED_OUT_SUCCESS_MSG = "Vehicle has been checked out successfully";

        internal const string CARD_DETAILS_EMPTY_MISSING_MSG = "Please fill your all card details";

        internal const string HOLD = "Hold";
        internal const string COMPLETE = "Complete";
        internal const string CHECKOUT = "Checkout";
        internal const string LOCATION = "Location";
        internal const string SERVICE = "Service";
        internal const string EMAIL = "Email";
    }

    public class StatusCodes
    {
        internal const int SUCCESS = 200;
        internal const int BAD_REQUEST = 400;
        internal const int INTERNAL_SERVER_ERROR = 500;
        internal const int UN_AUTHORIZED = 401;
    }
}