﻿using System;
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
            USPDELETEUSER,
            USPGETCLIENTUSERBYAUTHID
        }

        public enum ClockTime
        {
            USPGETTIMECLOCK,
            USPGETTIMECLOCKEMPLOYEEDETAILS,
            USPGETTIMECLOCKWEEKDETAILS,
            USPDELETETIMECLOCKEMPLOYEE,
            USPGETTIMECLOCKEMPLOYEEHOURDETAIL,
            USPGETEMPLOYEEWEEKLYTIMECLOCKHOUR,
            USPGETCLOCKEDINDETAILER
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
            USPGETHOURLYWASHSALESREPORT
        }

        public enum WhiteLabelling
        {
            USPGETWHITELABEL
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
            USPGETEMPLOYEEBYID,
            USPGETMODELBYMAKE,
            USPGETUPCHARGEBYMODEL,
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
            USPGETVEHICLEMEMBERSHIPAVAILABILITY,
            USPGETVEHICLEMEMBERSHIPBYMEMBERSHIPID,
            USPGETMEMBERSHIPLISTSETUPBYMEMBERSHIPID,
            USPDELETEMEMBERSHIP,
            USPGETMEMBERSHIPBYID,
            USPSAVEMEMBERSHIPSETUP,
            USPGETMEMBERSHIPSERVICEBYVEHICLEID,
            USPGETMEMBERSHIPSERVICEBYMEMBERSHIPID,
            uspGetMembershipListByVehicleId,
            USPGETALLMEMBERSHIPNAME,
        }

        public enum Checklist
        {
            USPGETCHECKLIST,
            USPDELETECHECKLIST
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
            USPISGIFTCARDEXIST,
            USPGETGIFTCARDBALANCEHISTORY,
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
            USPADDBAYSLOT,
            USPGETALLLOCATIONNAME
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
            USPGETPRODUCTBYID,
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
            USPGETCASHREGISTER,
            USPGETPASTWEATHERINFO,
            USPGETFORCASTEDRAINPERCENTAGE,
            USPGETTIPDETAIL,
        }
        public enum Collision
        {            //Collison
            USPDELETECOLLISION,
            USPGETCOLLISIONBYEMPID,
            USPGETCOLLISIONBYID,
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

            USPGETDETAILSCHEDULESTATUS,
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
            USPUPDATEPRODUCTQUANTITY,
            USPGETEMAILID,
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
            USPUPDATECHECKOUTDETAIL,
            USPUPDATEJOBSTATUSHOLD,
            USPUPDATEJOBSTATUSCOMPLETE,
            USPGETCUSTOMERHISTORY
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
        USPGETALLDEALS,
        USPUPDATETOGGLEDEALSTATUS,

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
        USPGETALLSERVICE,
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
        USPGETALLSERVICEDETAIL,
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
        USPGETALLVEHICLEIMAGEBYID,
        USPGETVEHICLEIMAGEBYID,
        USPDELETECLIENTVEHICLEIMAGE,
        //Vendor
        USPGETALLVENDOR,
        USPGETVENDORBYID,
        USPDELETEVENDOR,
        USPGETALLVENDORNAME,
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
        USPGETWASHTIMEBYLOCATIONID,
        uspGetAllLocationWashTime,


        USPGETTICKETNUMBER,
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
