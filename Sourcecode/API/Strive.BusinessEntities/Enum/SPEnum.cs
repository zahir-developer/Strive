﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{

    public class EnumSP
    {
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

        public enum Membership
        {
            USPGETALLMEMBERSHIP
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
        //Document
        USPSAVEDOCUMENT,
        USPGETEMPLOYEEDOCUMENTBYID,
        USPUPDATEDOCUMENTPASSWORD,
        USPGETEMPLOYEEDOCUMENTBYEMPID,
        USPDELETEEMPLOYEEDOCUMENTBYID,
        //Client
        USPGETALLCLIENT,
        USPGETCLIENT,
        USPDELETECLIENT,
        USPGETCLIENTBYID,
        //GiftCard
        USPGETALLGIFTCARD,
        USPGETGIFTCARDHISTORY,
        USPGETGIFTCARDBYID,
        USPGIFTCARDCHANGESTATUS,
        USPSAVEGIFTCARD,
        //Location
        USPSAVELOCATION,
        USPDELETELOCATION,
        USPGETPRODUCT,
        USPDELETEPRODUCT,
        USPGETAllPRODUCT,
        USPGETAllPRODUCTNEW,
        USPGETALLLOCATION,
        USPGETLOCATIONBYID,
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
        USPSAVEVEHICLE,
        uspGetVehicleByClientId,
        uspGetVihicleMembership,
        uspUpdateVechicleMembership,
        uspGetVehicleById,
        USPGETVEHICLEDETAILBYCLIENTID,
        USPGETVEHICLESTATEMENTBYCLIENTID,
        USPGETVEHICLEHISTORYBYCLIENTID,
        uspGetVehicleCodes,
        USPGETVEHICLEMEMBERSHIPBYVEHICLEID,
        USPGETMEMBERSHIPBYVEHICLEID,
        USPCLOCKTIMEDETAILS,
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
        USPGETSCHEDULEDETAILSBYDATE,
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
        uspGetMembershipListByVehicleId
    }


}
