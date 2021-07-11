using System.Drawing;

namespace Greeter.Common
{
    public class Constants
    {
        // 12 hour clock
        internal const string TIME_FORMAT = "hh:mm tt";
        internal const string DATE_FORMAT = "dd/MM/yyyy";
    }

    public class Urls
    {
        //Dev 
        //internal const string BASE_URL = "https://strivedev.azurewebsites.net";

        //internal const string BASE_URL = "http://40.114.79.101:5004";

        // Mamooth Dev
        internal const string BASE_URL = "https://mammothuatapi-dev.azurewebsites.net";

        //QA
        //internal const string BASE_URL = "https://mammothuatapi-qa.azurewebsites.net";


        internal const string LOGIN = "/Auth/Login";
        internal const string LOCATIONS = "/Admin/Location/GetAll";

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
        internal const string CHECKOUTS = "/Admin/Checkout/GetAllCheckoutDetails";
        internal const string SEND_EMAIL = "/Admin/Common/SendMail";
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
        internal const string SERVICE_CREATED_MSG = "Service created successFully";
        internal const string SERVICE_CREATION_ISSUE = "Couldn't generate Service";
        internal const string TICKET_CERATION_ISSUE = "Couldn't generate ticket";
        internal const string WARNING_FOR_MANDATORY_FILEDS_IN_SERVICE = "Please fill all mandatory fields make, model, color and {0}";
        internal const string WAH_PACKAGE = "wash package";
        internal const string DETAIL_PACKAGE = "detail package";
        internal const string EMAIL_SENT_MSG = "Email sent successFully";
        internal const string EMAIL_WARNING = "Please enter the valid email id";
    }

    public class StatusCodes
    {
        internal const int SUCCESS = 200;
        internal const int BAD_REQUEST = 400;
        internal const int INTERNAL_SERVER_ERROR = 401;
    }
}