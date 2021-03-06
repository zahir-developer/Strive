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
        internal const string BASE_URL = "https://strivedev.azurewebsites.net";
        internal const string LOGIN = "/Auth/Login";
        internal const string LOCATIONS = "/Admin/Location/GetAll";
        internal const string BARCODE = "/Admin/Washes/GetByBarCode/";
        internal const string CHECKOUTS = "/Admin/Checkout/GetAllCheckoutDetails";
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
        internal const string PAY = "pay";
    }

    public class ColorNames
    {
        internal const string APP_COLOR = "app_color";
        internal const string APP_SECONDARY_COLOR = "app_secondary_color";
    }

    public class Messages
    {
        internal const string BARCODE_EMPTY = "Please enter the Barcode";
        internal const string USER_ID_AND_PSWD_EMPTY = "Please enter the User ID and Password";
        internal const string USER_ID_EMPTY = "Please enter the User ID";
        internal const string PSWD_EMPTY = "Please enter the Password";
    }

    public class StatusCodes
    {
        internal const int SUCCESS = 200;
        internal const int BAD_REQUEST = 400;
        internal const int INTERNAL_SERVER_ERROR = 401;
    }
}