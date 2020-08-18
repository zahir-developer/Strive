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
        uspGetVehicleCodes,
        USPCLOCKTIMEDETAILS,
        //Vendor
        USPGETALLVENDOR,
        USPDELETEVENDOR,
        uspDeleteSchedule,
        uspGetSchedule,
        USPGETVEHICLE,
        //Washes
        USPGETJOBBYID,
        USPGETALLJOB,
        USPGETWASHDASHBOARD,
        USPGETCLIENTANDVEHICLEDETAIL,
        //Schedule
        USPSAVESCHEDULE,
        uspGetGiftCardHistoryByNumber,
        uspGetGiftCardByLocation,
        USPDELETEWASHES,
        uspGetClientName,
        uspGetClientCodes,
        uspGetGiftCardBalance
    }


}
