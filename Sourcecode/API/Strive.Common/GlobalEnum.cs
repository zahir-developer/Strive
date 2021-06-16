using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Common
{
    public enum GlobalEnum
    {
        Success,
        Fail
    }

    public enum UserRole
    {
        Auditor,
        Accountant,
        Controller,
        NoAccess,
        Admin,
    }

    public static class EmailSubject
    {
        public static string GiftCard = "Gift card details";
        public static string ProductThreshold = "Product threshold limit";
        public static string VehicleHold = "Vehicle is oh Hold!!";
        public static string WelcomeEmail = "Welcom To Strive";
        public static string Manager = "New Employee Info!!";
    }


    public static class GlobalUpload
    {
        public static string InvalidMessage = "Invalid format. Valid formats: ";
        public enum DocumentType
        {
            EMPLOYEEDOCUMENT,
            PRODUCTIMAGE,
            VEHICLEIMAGE,
            LOGO,
            EMPLOYEEHANDBOOK,
            TERMSANDCONDITION,
            ADS

        }

        public enum ArchiveFolder
        {
            ARCHIVED
        }
    }

    public enum UserType
    {
        Employee = 1,
        Client = 2,
        Admin = 3,
        SuperAdmin = 100
    }

    public enum HtmlTemplate
    {
        EmployeeSignUp,
        ClientSignUp,
        VehicleHold,
        ProductThreshold,
        EmployeeThreshold,
        GiftCardDetails,
        SuperAdmin,
        NewEmployeeInfo,
        ProductRequest,
        GeneralMail
    }

    public enum Roles
    {
        Admin,
        Operator,
        Manager,
        Cashier,
        Detailer,
        Washer,
        Runner
    }
}
