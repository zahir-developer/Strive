using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{

    public class EnumSP
    {
        public enum Employee
        {
            //Employee
            USPGETEMPLOYEE,
            USPSAVEEMPLOYEE,
            USPGETSCHEMABYGUID,
            USPGETCODES,
            USPGETUSERBYAUTHID,
            USPDELETEEMPLOYEE,
            USPGETEMPLOYEEBYEMPID,
            USPGETEMPLOYEEROLES,
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
        USPGETALLEMAIL,

        //Employee
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
        USPGETEMPLOYEELIST,
        USPGETEMPLOYEEBYID,
        USPGETALLSERVICE,
        USPGETPRODUCTS,
        USPGETCASHREGISTER,
        USPGETCOLLISIONBYEMPID,
        USPGETVEHICLELISTBYCLIENTID,
        //MembershipSetup
        USPGETMEMBERSHIPSETUP,
        USPGETSERVICEWITHPRICE,
        USPDELETEMEMBERSHIP,
        USPGETMEMBERSHIPBYID,
        USPSAVEMEMBERSHIPSETUP,
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
        USPGETVEHICLESTATEMENTBYCLIENTID,
        USPGETVEHICLEHISTORYBYCLIENTID,
        uspGetVehicleCodes,
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
        uspGetGiftCardBalance
    }


}
