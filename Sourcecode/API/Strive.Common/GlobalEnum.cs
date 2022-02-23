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

    public static class EmailSubject
    {
        public static string GiftCard = "Gift card details";
        public static string ProductThreshold = "Product threshold limit reached";
        public static string VehicleHold = "Vehicle is ON Hold";
        public static string WelcomeEmail = "Welcome To Strive";
        public static string Manager = "New Employee Info";
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
            ADS,
            MEMBERSHIPAGREEMENT
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
        DetailAssignedToEmployee,
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

    public static class ZebraPrint
    {
        public static string MammothLogo = 
            @"0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            000000000000000000000000000000000003E000000000000000000000000000000000000000
            000000000000000000000000000000007003FC00FF0000000000000000000000000000000000
            00000000000000000000000000000007F003FC00FF0000000000000000000000000000000000
            0000000000000000000000000000000FF003FE01FF0000000000000000000000000000000000
            0000000000000000000000000000400FF803FE01FF0000000000000000000000000000000000
            000000000000000000000000000FE00FF803FF03FF0007FC0000000000000000000000000000
            000000000000000000000000001FE00FF803FF03FF001FFF0000000000000000000000000000
            000000000000000000000000001FF01FF803FF07FF003FFFC000000000000000000000000000
            000000000000000000000000001FF81FF803FF87FF00FFFFF000000000000000000000000000
            000000000000000000000000001FF81FF803F78FFF00FF07F000000000000000000000000000
            000000000000000000000000000FFC1FFC03F7CF7E01FE01F800000000000000000000000000
            000000000000000000000020000FFE3CFC03F7DF7E01FC00FC01000000000000000000000000
            0000000000000000000000F8000FFF3CFC03F3FE7E03F800FC03C00000000000000000000000
            0000000000000000000003F8000FDF3CFC03F3FE7E03F800FC03F00000000000000000000000
            0000000000000000000007FC000FCFFCFC03F1FC7E03F0007E07FC0000000000000000000000
            0000000000000000000007FF000FCFFCFE03F1FC7E03F0007E03FF0000000000000000000000
            0000000000000000000007FF0007E7F8FE03E0F87E03F0007E00FFC000000000000000000000
            0000000000000000000007FF8007E3F87E03E0F87E03F000FE001FF000000000000000000000
            0000000000000000000007FFC007E1F87E03E0007E03F000FE000FFC00000000000000000000
            0000000000000000000007CFE007E1F07E03E0007E03F000FC001FFF00000000000000000000
            0000000000000000000007CFF003E0C07E03E0007E03F001FC001FFFC0000000000000000000
            0000000000000000000007C3F803E0007F03E0007E03F001FC003F9FC0000000000000000000
            00000000000000000C0007C3FC03F0003F03E0007E01F803F8003F8F80000000000000000000
            00000000000000003E0007C1FE03F0003F0000007E01FC07F8007F0380000000000000000000
            00000000000000007F0007C0FF03F000000000000000FF9FF0007F0000000000000000000000
            0000000000000000FF8007C7FF83F0000000000000007FFFE000FE0000C00000000000000000
            0000000000000000FF8007FFFFC1F0000000000000001FFFE000FE0000E00000000000000000
            0000000000000000FFC007FFFFE1800000000000000007FF8001FC0001F00000000000000000
            0000000000000000FFE007FF8FF0000000000000000000FE0001FC0003FC0000000000000000
            0000000000000000FFF007FE07E0000000000000000000000001F80007F80000000000000000
            0000000000000000FFF007F803000000001FFFFF000000000003F80007F00000000000000000
            00000000000000007FF807E0000000003FFFFFFFFF0000000003F0000FF00000000000000000
            00000000000000007BFC07E000000007FFFFFFFFFFF800000007F0001FE00000000000000000
            000000000000078079FC07E0000000FFFFFFFFFFFFFF80000007E0003FC00000000000000000
            0000000000000FE079FE07E0000007FFFFFFFFFFFFFFF800000FE0007F800000000000000000
            0000000000001FF87CFF07E000003FFFFFFFFFFFFFFFFF800007C000FF800400000000000000
            0000000000007FFE3C7F87C00003FFFFFFFFFFFFFFFFFFE00001C000FFC00E00000000000000
            0000000000007FFFBC3FC600000FFFFFFFFFFFFFFFFFFFFC00000001FFE01F00000000000000
            0000000000003FFFFE1FC400003FFFFFFFFFFFFFFFFFFFFF00000003FFF03F80000000000000
            0000000000001FFFFE1FE00001FFFFFFFFFFFFFFFFFFFFFFE0000007F9FC3F80000000000000
            0000000000000FE7FE0FC00007FFFFFFFFFFFFFFFFFFFFFFF800000FF0FEFF00000000000000
            0000000000000FF1FE0700001FFFFFFFFFFFFFFFFFFFFFFFFE00000FE07FFE00000000000000
            00000000000007F0FC0600007FFFFFFFFFFFFFFFFFFFFFFFFF80001FE01FFE00000000000000
            00000000000003F838000001FFFFFFFFFFFFFFFFFFFFFFFFFFE0001FC00FFC00000000000000
            00000000000001FC000000000000007FFFFFFFFFFFFF8000000000048007F800000000000000
            00000000000001FE000000000000003FFFFFFFFFFFFF0000000000001C07F000000000000000
            00000000000000FF000000000000003FFFFFFFFFFFFF0000000000003E0FF000000000000000
            000000000000007F000000000000003FFFFFFFFFFFFF0000000000003E1FE000000000000000
            000000000000003F800000000000003FFFFFFFFFFFFF0000000000003E3FC000000000000000
            000000000000003FC00000000000003FFFFFFFFFFFFF0000000000003E7F8000000000000000
            000000000000001FE00000000000003FFFFFFFFFFFFF0000000000003CFF0000000000000000
            000000000000000FE00000000000003FFFFFFFFFFFFF0000000000003DFE0000000000000000
            0000000000000007C0000000000000FFFFFFFFFFFFFFC00000000001F1FE0000000000000000
            000000000000000780000000000003FFFFFFFFFFFFFFF00000000007E0FC0000000000000000
            000000000000000300000000000003FFFFFFFFFFFFFFF0000000000780780000000000000000
            000000000000000000000000000003FFFFFFFFFFFFFFF0000000000780300000000000000000
            000000000000000000000000000001FFFFFFFFFFFFFFE00000000007C0000000000000000000
            000000000000000000000000000000FFFFFFFFFFFFFFC0000000000780000000000000000000
            000000000000000000000000000000FFFFFFFFFFFFFFC0000000000780000000000000000000
            0000000000000000001000000000007FFFFFFFFFFFFF80000000000780000000000000000000
            0000000000000000003000000000007FFFFFFFFFFFFF80000000000780000000000000000000
            0000000000000000006000000000003FFFFFFFFFFFFF000000000007C0000000000000000000
            000000000000000000F000000000001FFFFFFFFFFFFE000000000007C0000000000000000000
            000000000000000001F000000000001FFFFFFFFFFFFE000000000007E0000000000000000000
            000000000000000003F000000000000FFFFFFFFFFFFC000000000007F0000000000000000000
            000000000000000003F000000000000FFFFFFFFFFFF8000000000007F0000000000000000000
            000000000000000007F0000000000007FFFFFFFFFFF8000000000007F8000000000000000000
            00000000000000000FF0000000000003FFFFFFFFFFF0000000000007FC000000000000000000
            00000000000000001FF0000000000003FFFFFFFFFFF0000000000007FE000000000000000000
            00000000000000001FF0000000000003FFFFFFFFFFE0000000000007FE000000000000000000
            00000000000000003FF0000000000001FFFFFFFFFFC0000000000007FF000000000000000000
            00000000000000003FF0000000000001FFFFFFFFFFC0000000000007FF000000000000000000
            00000000000000007FF0000000000000FFFFFFFFFF80000000000007FF800000000000000000
            00000000000000007FF00000000000007FFFFFFFFF80000000000007FF800000000000000000
            0000000000000000FFF00000000000007FFFFFFFFF00000000000007FFC00000000000000000
            0000000000000000FFF00000000000003FFFFFFFFE00000000000007FFC00000000000000000
            0000000000000001FFF00000000000001FFFFFFFFE00000000000007FFE00000000000000000
            0000000000000001FFF00000000000001FFFFFFFFC00000000000007FFE00000000000000000
            0000000000000003FFF00000000000000FFFFFFFF800000000000007FFF00000000000000000
            0000000000000003FFF00000000000000FFFFFFFF800000000000007FFF00000000000000000
            0000000000000007FFF000000000000007FFFFFFF000000000000007FFF80000000000000000
            0000000000000007FFF000000000000007FFFFFFF000000000000007FFF80000000000000000
            0000000000000007FFF000000000000003FFFFFFE000000000000007FFF80000000000000000
            000000000000000FFFF000000000000001FFFFFFE000000000000007FFFC0000000000000000
            000000000000000FFFF000000000000001FFFFFFC000000000000007FFFC0000000000000000
            000000000000000FFFF000000000000000FFFFFF8000000000000007FFFC0000000000000000
            000000000000001FFFF000000000000000FFFFFF8000000000000007FFFE0000000000000000
            000000000000001FFFF0000000000000007FFFFF0000000000000007FFFE0000000000000000
            000000000000001FFFF0000000000000007FFFFF0000000000000007FFFE0000000000000000
            000000000000001FFFF0000000000000003FFFFE0000000000000007FFFE0000000000000000
            000000000000001FFFF0000000400000003FFFFC0000000C00000007FFFE0000000000000000
            000000000000001FFFF0000000600000001FFFFC0000001C00000007FFFE0000000000000000
            000000000000003FFFF0000000700000000FFFF80000001C00000007FFFF0000000000000000
            000000000000003FFFF0000000700000000FFFF00000003C00000007FFFF0000000000000000
            000000000000003FFFF00000007800000007FFF00000007C00000007FFFF0000000000000000
            000000000000003FFFF00000007800000007FFE00000007C00000007FFFF0000000000000000
            000000000000003FFFF00000007C00000003FFE0000000FC00000007FFFF0000000000000000
            000000000000003FFFF00000007C00000001FFC0000000FC00000007FFFF0000000000000000
            00000000E000003FFFF00000007E00000001FF80000001FC00000007FFFF0000000000000000
            0000001FE000003FFFF00000007F00000000FF80000003FC00000007FFFF0000000000000000
            000001FFE000003FFFF00000007F00000000FF00000003FC00000007FFFF00000003C0000000
            00000FFFE000003FFFF00000007F800000007F00000007FC00000007FFFF00000007FC000000
            00000FFF0000003FFFF00000007F800000003E0000000FFC00000007FFFF0000000FFF800000
            00000FFC0000003FFFF00000007FC00000003C0000000FFC00000007FFFF0000000FFF800000
            00000E1E0000003FFFF00000007FE00000003C0000001FFC00000007FFFF0000000E1F800000
            0000001E0000003FFFF00000007FE0000000180000001FFC00000007FFFF0000000E01000000
            0000000E0000003FFFF00000007FF0000000000000003FFC00000007FFFF0000000E00000000
            0000000E7800003FFFE00000007FF0000000000000003FFC00000007FFFF0000000E00000000
            0000000FF800001FFFF00000007FF8000000000000007FFC00000007FFFE0000001FF0000000
            0000007FF800001FFFF00000007FF800000000000000FFFC00000007FFFE0000001FFE000000
            000003FFF800001FFFF00000007FFC00000000000000FFFC00000007FFFE0000000FFE000000
            000003FF0000001FFFF00000007FFE00000000000001FFFC00000007FFFE000000007E000000
            000003E00000001FFFF00000007FFE00000000000003FFFC00000007FFFE0000000004000000
            000000000000000FFFF00000007FFF00000000000003FFFC00000007FFFE0000000000000000
            000000000000000FFFF00000007FFF00000000000007FFFC00000007FFFC0000003F00000000
            000000007000000FFFF00000007FFF80000000000007FFFC00000007FFFC0000007FC0000000
            0000001C7000000FFFF00000007FFFC000000000000FFFFC00000007FFFC000000FFE0000000
            0000007E38000007FFF00000007FFFC000000000000FFFFC00000007FFF8000000E1F0000000
            0000007F18000007FFF00000007FFFE000000000001FFFFC00000007FFF8000000C0F0000000
            0000007B18000007FFF00000007FFFE000000000003FFFFC00000007FFF8000001C070000000
            000000619C000003FFF00000007FFFF000000000007FFFFC00000007FFF0000001E070000000
            00000071FC000003FFF00000007FFFF000000000007FFFFC00000007FFF0000000F070000000
            00000031FC000001FFF00000007FFFF80000000000FFFFFC00000007FFE0000000FFE0000000
            0000001FF0000001FFF00000007FFFFC0000000000FFFFFC00000007FFE00000E0FFE0000000
            0000001FE0000000FFF00000007FFFFC0000000001FFFFFC0000000FFFC00000F83FC0000000
            0000001F80000000FFF00000007FFFFE0000000001FFFFFC00000007FFC00000FC0F00000000
            0000001C010000007FF00000007FFFFF0000000003FFFFFC00000007FF800000FF0000000000
            00000000078000007FF00000007FFFFF0000000007FFFFFC00000007FF8000003FC000000000
            000000001F8000003FF00000007FFFFF800000000FFFFFFC00000007FF0000000FF000000000
            000000007F8000003FF00000007FFFFF800000000FFFFFFC00000007FF00000003FC00000000
            00000000FEE000001FF00000007FFFFFC00000001FFFFFFC00000007FE00000000FE00000000
            00000003F8F000001FF00000007FFFFFE00000003FFFFFFC00000007FE000000003E00000000
            00000003E07000000FF00000007FFFFFE00000003FFFFFFC00000007FC000000780C00000000
            000000018070000007F00000007FFFFFF00000007FFFFFFC00000007F8000000FE0000000000
            0000000001F0000003F00000007FFFFFF80000007FFFFFFC00000007F0000001FF0000000000
            0000000003F0000003F00000007FFFFFF8000000FFFFFFFC00000007F00000019F8000000000
            000000000FC0000001F00000007FFFFFFC000001FFFFFFFC00000007E00000039FE000000000
            000000003F80000000E00000007FFFFFFC000003FFFFFFFC00000007C00000071BE000000000
            000000003E00000000700000007FFFFFFE000003FFFFFFFC000000078000000731E000000000
            00000000181F800000300000007FFFFFFF000007FFFFFFFC000000078000008231C000000000
            00000000003FC00000100000007FFFFFFF000007FFFFFFFC00000007000003E0718000000000
            00000000007FC70000000000007FFFFFFF80000FFFFFFFFC00000007000007E03B8000000000
            0000000000F8FF8000000000007FFFFFFF80001FFFFFFFFC0000000700000F003F0000000000
            0000000001F07F0000000000007FFFFFFFC0001FFFFFFFFC0000000780001E001F0000000000
            0000000001E07E0000000000007FFFFFFFC0003FFFFFFFFC0000000780001C1F0E0000000000
            0000000001C0F80000000000007FFFFFFFE0007FFFFFFFFC0000000680003C3F800000000000
            0000000001C3F00000000000007FFFFFFFF0007FFFFFFFFC00008007C0003C7FC00000000000
            0000000000E3E00000000000007FFFFFFFF000FFFFFFFFF000000001E0001FFFC00000000000
            00000000007F800000000000000FFFFFFFF801FFFFFFFF800000200030001FF1C00000000000
            00000000001F0000000000000003FFFFFFF813FFFFFFFF00000000003E001FE1C00000000000
            00000000001E0000000000000003FFFFFFFC13FFFFFFFF8000000C003E000781C00000000000
            00000000001C0000000000000003FFFFFFFE13FFFFFFFF20000063803E000003800000000000
            000000000000000FE00000000003FFFFFFFF67FFFFFFFF00000080103E400007800000000000
            000000000000001FF00000000003FFFFFFFF0FFFFFFFFF00000000003FE0001F000000000000
            000000000000003FF90000000003FFFFFFFF8FFFFFFFFF001C0C00003FF0007E000000000000
            000000000000007C7C0000000007FFFFFFFF9FFFFFFFFF8600008000BFF0003C000000000000
            00000000000000F81C0FFFFFFFFFFFFFFFFFDFFFFFFFFFFFFFFFFFFFFC7C0018000000000000
            00000000000000F01C000010FFFFFFFFFFFFFFFFFFFFFFFFFFC000003C3C0000000000000000
            00000000000001E00C0000001FFFFFFFFFFFFFFFFFFFFFFFFF0000003C3E0000000000000000
            00000000000001C00800000007FFFFFFFFFFFFFFFFFFFFFFF8000000181F0000020000000000
            00000000000001C00000000001FFFFFFFFFFFFFFFFFFFFFFE0000000010F8000000000000000
            00000000000001C001000000003FFFFFFFFFFFFFFFFFFFFF000000000787C000000000000000
            00000000000001C003800000000FFFFFFFFFFFFFFFFFFFFC000000000783C000000000000000
            00000000000000E007E000000003FFFFFFFFFFFFFFFFFFF00000000003C1F000000000000000
            00000000000000F801F0000000007FFFFFFFFFFFFFFFFF800000000001E1F000000000000000
            000000000000007F00780000000007FFFFFFFFFFFFFFF80000000001E0F0E000000000000000
            000000000000003F1E3C0000000000FFFFFFFFFFFFFFC00000000007F0F04000000000000000
            000000000000000C7FBC00000000000FFFFFFFFFFFF800000000000FF8780001000000000000
            00000000000000007FFC6000000000003FFFFFFFFF0000000000101E7C3C0000000000000000
            000000000000000079F870000000008000FFFFFEC0000000000078187C1C0000000000000000
            000000000000000070F0F80000000000000000000000000000007998FE180000000000000000
            000000000000000078F0FF0000000000000000000000000000003F81CE000000000000000000
            00000000000000003FE1FF00E0000000000000000000000000003F838F000000000000000000
            00000000000000001FE1FF00F0000000000000000000000000003E078F000100000000000000
            000000000000000003C3C600F0000000000000000000000000003F079F000000000000000000
            00000000000000000387C001E1E000000000000001000000001F6F03F8000C00000000000000
            000000000000000000078001E3F000000001000000000000007F8F03F0000000000000000000
            0000000000000000000F8001E3F08000000000000000001FC0FFC781E0000000000000000000
            0000000000000000000F0001E7F1E00000000000000001FFF0F3E7C80002C000000000000000
            000000000000000000070001E7E3E00000000000000003FFF1E3E3F800030000000000000000
            000000000000000000000001EFE3E00000000000000003FFF9FFC3FC00100000000000000000
            000000000000000000000001FFE7C000000F00001FC001F0F9FF81F040200000000000000000
            000000000000000000000001FFE7CE00000F00007FE001F07CFC30E000C00000000000000000
            000000000000000000000001FEEF8FF0000F0000FFE009F07CF0701001600000000000000000
            000000000000000000000003FDFF1FF8040F0000F9E000F03CFFF80004000000000000000000
            000000000000000000000003F9FF0FFC7F8F000079E000F83CFFF0800E000000000000000000
            000000000000000000000003F9FE007CFFCFFC007DE000F83C7FE00018000000000000000000
            000000000000000000000003F1FC1FFDFFCFFE007FBE007C3C1F8003C0000000000000000000
            000000000000000000000001E1FC3FFDF0CFFE007FBE007C7C04000C80000000000000000000
            00000000000000000000000021F83FFCFC0FFE003FFE007FF800001C00000000000000000000
            00000000000000000000000001F87C7CFF1F1E007FFE007FF800004000000000000000000000
            00000000000000000000000001F07C78FFCF3E007FFE003FF00004E000000000000000000000
            00000000000000000000000000013FF93F8F1E00F9FC223FF8001F8000000000000000000000
            00000000000000000000000000107FF98FCF1E00F8FE103DC0003C0000000000000000000000
            00000000000000000000000000021FFBE7CF1E00FFFF80910001F00000000000000000000000
            00000000000000000000000000000CF3FF8F1E007FFFC3800038800000000000000000000000
            00000000000000000000000020000271FF8F1E003FFFF600007F000000000000000000000000
            00000000000000000000000000000003FF2F1E003FC400000FF0000000000000000000000000
            00000000000000000000000000008000002F5E00008400001FC0000000000000000000000000
            000000000000000000000000004C10000020020000000099FC00000000000000000000000000
            0000000000000000000000000000800000000000000000FF8000000000000000000000000000
            0000000000000000000000000000000000000000000003FE0000000000000000000000000000
            0000000000000000000000000000000730F80000003FFF000000000000000000000000000000
            000000000000000000000000000000000F000000FFC700000000000000000000000000000000
            0000000000000000000000000000000080FFFFFFFC8000000000000000000000000000000000
            0000000000000000000000000000000000000000400000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000
            0000000000000000000000000000000000000000000000000000000000000000000000000000";

    }

}
