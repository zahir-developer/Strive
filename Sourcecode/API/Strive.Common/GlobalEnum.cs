﻿using System;
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

    

    public static class GlobalUpload
    {
        public static string InvalidMessage = "Invalid format. Valid formats: ";
        public enum DocumentType
        {
            EMPLOYEEDOCUMENT,
            PRODUCTIMAGE,
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
        Employee =1,
        Client=2,
        Admin = 3,
        SuperAdmin = 100
    }

    public enum HtmlContent
    {
        EmployeeSignUp,
        ClientSignUp,
        HoldNotification
    }
}
