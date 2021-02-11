using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{

    public class EnumSP
    {
        public enum Authentication
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
            USPGETSCHEMABYGUID,
            USPGETPASSWORDHASH,
            USPSAVEOTP,
            USPRESETPASSWORD,
            USPVERIFYOTP,
        }
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

       
        public enum Employee
        {
            //Employee
            USPGETEMPLOYEELIST,
            USPGETALLEMPLOYEE,
            USPGETEMPLOYEE,
            USPSAVEEMPLOYEE,
            USPGETCODES,
            USPGETUSERBYAUTHID,
            USPDELETEEMPLOYEE,
            USPGETEMPLOYEEBYEMPID,
            USPGETEMPLOYEEROLES,
            USPGETALLEMPLOYEEDETAIL,
            USPEMAILEXIST,
            USPGETEMPLOYEEROLEBYID,
            USPGETCITYBYSTATE,
            USPGETEMPLOYEEBYID
        }
        public enum Client
        {
            //Client
            USPCLIENTEMAILEXIST,
            USPGETALLCLIENT,
            USPUPDATEACCOUNTDETAILS,
            USPGETCLIENT,
            USPDELETECLIENT,
            USPGETCLIENTBYID,
            USPGETVEHICLELISTBYCLIENTID,
            USPGETCLIENTNAME,
            USPGETALLCLIENTNAME,
            uspGetClientCodes,
            uspGetClientAndVehicle,
            USPISCLIENTAVAILABLE
        }
       public enum GiftCard
        {
            //GiftCard
            USPGETALLGIFTCARD,
            USPGETGIFTCARDHISTORY,
            USPGETGIFTCARDBYID,
            USPGIFTCARDCHANGESTATUS,
            USPSAVEGIFTCARD,
            USPGETALLGIFTCARDS,
            USPDELETEGIFTCARD,
            uspGetGiftCardHistoryByNumber,
            uspGetGiftCardByLocation,
            uspGetGiftCardBalance,
        }

        public enum Location
        {
            //Location
            USPSAVELOCATION,
            USPDELETELOCATION,
            USPGETAllPRODUCTNEW,
            USPGETALLLOCATION,
            USPGETLOCATIONBYID,
            USPGETALLLOCATIONOFFSET,
            USPDELETELOCATIONOFFSET,
            USPADDBAYSLOT
        }
        public enum ServiceSetup
        {
            //Service Setup
            USPDELETESERVICEBYID,
            USPGETSERVICES,
            USPGETSERVICECATEGORYBYLOCATIONID,
            USPGETALLSERVICE,
            USPGETSERVICELIST,

        }

        public enum Product
        {
            USPGETPRODUCTS,
            USPGETPRODUCT,
            USPDELETEPRODUCT,
            USPGETAllPRODUCT,
        }
        public enum CashRegister
        {
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
            USPGETCASHREGISTER
        }
        public enum Collision
        {            //Collison
            USPDELETECOLLISION,
            USPGETCOLLISIONBYEMPID,
            USPGETCOLLISIONBYID,
        }
       
       

        public enum Membership
        {
            USPGETALLMEMBERSHIP,
            USPGETVEHICLEMEMBERSHIPAVAILABILITY,
            USPGETVEHICLEMEMBERSHIPBYMEMBERSHIPID,
            USPGETMEMBERSHIPLISTSETUPBYMEMBERSHIPID,
            
            USPDELETEMEMBERSHIP,
            USPGETMEMBERSHIPBYID,
            USPSAVEMEMBERSHIPSETUP,
            USPGETMEMBERSHIPSERVICEBYVEHICLEID,
            USPGETMEMBERSHIPSERVICEBYMEMBERSHIPID,

            uspGetMembershipListByVehicleId,
        }
        public enum Vehicle
        {
            //Vehicle

            USPGETVEHICLE,
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
        }
        public enum Vendor
        {
            //Vendor
            USPGETALLVENDOR,
            USPDELETEVENDOR,
        }
        public enum Washes
        {
            //Washes
            USPGETJOBBYID,
            USPGETALLJOB,
            USPGETWASHDASHBOARD,
            USPGETCLIENTANDVEHICLEDETAIL,
            USPDELETEWASHES
        }
        public enum Details
        {
            //Details
            USPGETALLDETAILJOB,
            USPGETDETAILJOBBYID,
            USPGETALLBAYLISTBYID,
            USPGETBAYSCHEDULESDETAILS,
            USPGETPASTCLIENTNOTESBYCLIENTID,
            USPGETJOBTYPE,
            USPDELETEDETAILSCHEDULE,
            USPGETALLDETAILS,
        }
        public enum Schedule
        {
            //Schedule
            USPSAVESCHEDULE,
            USPGETSCHEDULEBYSCHEDULEID,
            uspDeleteSchedule,
            USPGETSCHEDULE,
        }
        public enum Sales
        {
            uspGetItemList,
            USPGETACCOUNTDETAILS,
            uspGetItemListByTicketNumber,
            uspUpdateSalesItem,
            uspDeleteSalesItemById,
            USPUPDATEJOBPAYMENT,
            USPGETALLSERVICEANDPRODUCTLIST,
            uspGetServiceByItemList,
            USPDELETEJOBITEMS,
            USPROLLBACKPAYMENT,
        }
        public enum Payroll
        {

            USPGETPAYROLLLIST,
            USPGETPAYROLLPROCESS,
            USPUPDATEADJUSMENT,
            USPUPDATEEMPLOYEEADJUSTMENT,
        }
        public enum Checkout
        {

            //CHECKOUT
            USPGETAllCHECKOUTDETAILS,
            USPUPDATECHECKOUTDETAILFORJOBID,
            USPUPDATEJOBSTATUSHOLDBYJOBID,
            USPUPDATEJOBSTATUSCOMPLETEBYJOBID,
            USPGETCUSTOMERHISTORY
        }
           
    }
    
    public enum SPEnum
    {
      
        USPCREATETENANT,
    }
}
