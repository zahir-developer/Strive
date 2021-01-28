﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{

    public class EnumSP
    {
        public enum ClockTime
        {
            USPGETTIMECLOCK,
            USPGETTIMECLOCKEMPLOYEEDETAILS,
            USPGETTIMECLOCKWEEKDETAILS,
            USPDELETETIMECLOCKEMPLOYEE,
            USPGETTIMECLOCKEMPLOYEEHOURDETAIL
        }
        public enum SalesReport
        {
            USPGETMONTHLYSALESREPORT,
            USPGETCUSTOMERSUMMARYREPORT,
            USPMONTHLYCUSTOMERDETAIL,
            uspGetMonthlyTipDetail,
            uspGetDailyTipDetail,
            uspGetDailyStatusReport,
            uspGetDailyStatusInfo,
            uspGetDailyClockDetail,
            USPGETMONTHLYMONEYOWNEDREPORT,
            USPGETEODSALESREPORT,
            USPGETDAILYSALESREPORT,
            USPGETHOURLYWASHREPORT,
            USPGETHOURLYWASHSALESREPORT,
            USPGetPastWeatherInfo
        }

        public enum WhiteLabelling
        {
            USPGETWHITELABEL
        }

        public enum Employee
        {
            //Employee
            USPGETALLEMPLOYEE,
            USPGETEMPLOYEE,
            USPSAVEEMPLOYEE,
            USPGETSCHEMABYGUID,
            USPGETCODES,
            USPGETUSERBYAUTHID,
            USPDELETEEMPLOYEE,
            USPGETEMPLOYEEBYEMPID,
            USPGETEMPLOYEEROLES,
            USPGETALLEMPLOYEEDETAIL,
        }
        public enum DashboardStatistics
        {
            USPGETDASHBOARDSTATISTICS,
            USPGETAVAILABLETIMESLOT
        }
        public enum Messenger
        {
            USPGETChatEMPLOYEELIST,
            UPDATECHATCOMMUNICATIONDETAIL,
            USPGETCHATMESSAGE,
            USPGETEMPLOYEERECENTCHATHISTORY,
            USPGETCHATMESSAGECOUNT,
            USPGETCHATGROUPEMPLOYEELIST,
            USPGETCHATEMPLOYEEGROUPLIST,
            USPDELETECHATUSERGROUP,
            USPUPDATECHATUNREADMESSAGESTATE,
            USPGETCHATEMPLOYEEANDGROUPHISTORY
        }

        public enum Membership
        {
            USPGETALLMEMBERSHIP,
            USPGETVEHICLEMEMBERSHIPAVAILABILITY
        }
        public enum Checklist
        {
            USPGETCHECKLIST,
            USPDELETECHECKLIST
        }

        public enum SystemSetup
        {
            USPDELETEBONUSSETUP,
            USPGETBONUSSETUP
        }

        public enum Document
        {
            USPGETDOCUMENT,
            USPSAVEDOCUMENT,
            USPGETEMPLOYEEDOCUMENTBYID,
            USPUPDATEDOCUMENTPASSWORD,
            USPGETEMPLOYEEDOCUMENTBYEMPID,
            USPDELETEEMPLOYEEDOCUMENTBYID,
            USPDELETEDOCUMENT,      
            USPDELETEDOCUMENTBYID,
            USPGETDOCUMENTBYID,
            USPGETALLDOCUMENT,
        }
        public enum AdSetup
        {
            USPGETALLADSETUP,
            USPGETADSETUPBYID,
            USPDELETEADSETUP
        }
    }
    
    public enum SPEnum
    {
        USPLOGIN,
        USPGETUSERBYLOGIN,
        USPGETALLUSERS,
        USPGETALLSERVICETYPE,
        USPGETUSERS,
        USPSAVEUSER,
        USPSAVELOGIN,
        USPSAVETENANTUSERMAP,
        USPGETALLEMAIL,

        //Employee
        USPGETEMPLOYEELIST,
        USPGETALLEMPLOYEE,
        USPGETEMPLOYEE,
        USPSAVEEMPLOYEE,
        USPGETSCHEMABYGUID,
        USPGETCODES,
        USPGETUSERBYAUTHID,
        USPDELETEEMPLOYEE,
        USPGETEMPLOYEEBYEMPID,
        USPGETEMPLOYEEROLES,
        USPEMAILEXIST,
        USPGETEMPLOYEEROLEBYID,
        USPGETCITYBYSTATE,

        //Client
        USPGETALLCLIENT,
        USPUPDATEACCOUNTDETAILS,
        USPGETCLIENT,
        USPDELETECLIENT,
        USPGETCLIENTBYID,
        USPISCLIENTAVAILABLE,
        USPGETALLCLIENTNAME,
        //GiftCard
        USPGETALLGIFTCARD,
        USPGETGIFTCARDHISTORY,
        USPGETGIFTCARDBYID,
        USPGIFTCARDCHANGESTATUS,
        USPSAVEGIFTCARD,
        USPGETALLGIFTCARDS,
        USPDELETEGIFTCARD,
        USPGETCLIENTNAME,
        //Location
        USPSAVELOCATION,
        USPDELETELOCATION,
        USPGETPRODUCT,
        USPDELETEPRODUCT,
        USPGETAllPRODUCT,
        USPGETAllPRODUCTNEW,
        USPGETALLLOCATION,
        USPGETLOCATIONBYID,
        USPGETALLLOCATIONOFFSET,
        USPDELETELOCATIONOFFSET,
        USPADDBAYSLOT,
        //Service Setup
        USPDELETESERVICEBYID,
        USPGETSERVICES,
        USPGETSERVICECATEGORYBYLOCATIONID,
        //Cash Register
        USPSAVETODAYCASHREGISTER,
        USPGETCASHREGISTERDETAILS,
        USPSAVECASHREGISTERBILLS,
        USPSAVECASHREGISTERCOINS,
        USPSAVECASHREGISTERMAIN,
        USPSAVECASHREGISTEROTHERS,
        USPSAVECASHREGISTERROLES,
        USPSAVECASHREGISTERROLLS,
        USPSAVECASHREGISTER,
        //Collison
        USPDELETECOLLISION,
        USPGETPASSWORDHASH,
        USPSAVEOTP,
        USPRESETPASSWORD,
        USPVERIFYOTP,

        USPGETEMPLOYEEBYID,
        USPGETALLSERVICE,
        USPGETPRODUCTS,
        USPGETCASHREGISTER,
        USPGETCOLLISIONBYEMPID,
        USPGETCOLLISIONBYID,
        USPGETVEHICLELISTBYCLIENTID,
        //MembershipSetup
        USPGETMEMBERSHIPLISTSETUPBYMEMBERSHIPID,
        USPGETSERVICELIST,
        USPDELETEMEMBERSHIP,
        USPGETMEMBERSHIPBYID,
        USPSAVEMEMBERSHIPSETUP,
        USPGETMEMBERSHIPSERVICEBYVEHICLEID,
        USPGETMEMBERSHIPSERVICEBYMEMBERSHIPID,
        //Vehicle
        USPGETALLVEHICLE,
        USPUPDATEVEHICLE,
        USPDELETECLIENTVEHICLE,
        USPGETVEHICLEBYID,
        USPGETPASTDETAILSBYCLIENTID,
        USPSAVEVEHICLE,
        uspGetVehicleByClientId,
        USPGETVEHICLEMEMBERSHIP,
        uspUpdateVechicleMembership,
        USPGETVEHICLEDETAILBYCLIENTID,
        USPGETVEHICLESTATEMENTBYCLIENTID,
        USPGETVEHICLEHISTORYBYCLIENTID,
        uspGetVehicleCodes,
        USPGETVEHICLEMEMBERSHIPBYVEHICLEID,
        USPGETMEMBERSHIPBYVEHICLEID,
        //Vendor
        USPGETALLVENDOR,
        USPDELETEVENDOR,
        USPGETVEHICLE,
        //Washes
        USPGETJOBBYID,
        USPGETALLJOB,
        USPGETWASHDASHBOARD,
        USPGETCLIENTANDVEHICLEDETAIL,
        //Details
        USPGETALLDETAILJOB,
        USPGETDETAILJOBBYID,
        USPGETALLBAYLISTBYID,
        USPGETBAYSCHEDULESDETAILS,
        USPGETPASTCLIENTNOTESBYCLIENTID,
        USPGETJOBTYPE,
        USPDELETEDETAILSCHEDULE,
        USPGETALLDETAILS,
        //Schedule
        USPSAVESCHEDULE,
        USPGETSCHEDULEBYSCHEDULEID,
        uspDeleteSchedule,
        USPGETSCHEDULE,
        uspGetGiftCardHistoryByNumber,
        uspGetGiftCardByLocation,
        USPDELETEWASHES,
        uspGetClientName,
        uspGetClientCodes,
        uspGetGiftCardBalance,
        uspGetClientAndVehicle,
        uspGetMembershipListByVehicleId,
        uspUpdateSalesItem,
        uspDeleteSalesItemById,
        uspGetItemList,
        USPGETACCOUNTDETAILS,
        uspGetItemListByTicketNumber,
        USPCREATETENANT,
        uspGetServiceByItemList,
        USPDELETEJOBITEMS,
        USPROLLBACKPAYMENT,
        USPGETALLSERVICEANDPRODUCTLIST,
        USPGETPAYROLLLIST,
        USPUPDATEADJUSMENT,
        USPUPDATEEMPLOYEEADJUSTMENT,
        USPUPDATEJOBPAYMENT,
        //CHECKOUT
        USPGETAllCHECKOUTDETAILS,
        USPUPDATECHECKOUTDETAILFORJOBID,
        USPUPDATEJOBSTATUSHOLDBYJOBID,
        USPUPDATEJOBSTATUSCOMPLETEBYJOBID,
        USPGETCUSTOMERHISTORY
    }
}
