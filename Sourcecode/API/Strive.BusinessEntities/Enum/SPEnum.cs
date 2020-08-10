﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{
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
        //Document
        USPSAVEDOCUMENT,
        USPGETDOCUMENTBYEMPID,
        USPUPDATEDOCUMENTPASSWORD,
        USPGETALLDOCUMENTBYID,
        USPDELETEDOCUMENT,
        //Client
        USPGETALLCLIENT,
        USPDELETECLIENT,
        USPGETCLIENTBYID,
        //GiftCard
        USPGETALLGIFTCARD,
        USPGETGIFTCARDBYID,
        USPGIFTCARDCHANGESTATUS,
        USPSAVEGIFTCARD,
        //Location
        USPGETLOCATION,
        USPSAVELOCATION,
        USPDELETELOCATION,
        USPGETPRODUCT,
        USPDELETEPRODUCT,
        USPGETAllPRODUCT,
        USPGETAllPRODUCTNEW,
        USPGETAllLOCATION,
        USPSaveLOCATION,
        USPGETLOCATIONBYID,
        //Service Setup
        USPGETSERVICE,
        USPSAVESERVICE,
        USPDELETESERVICEBYID,
        USPGETSERVICEBYID,
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
        USPGETCOLLISION,
        USPGETCOLLISIONBYID,
        USPSAVECOLLISION,
        USPDELETECOLLISION,
        USPGETALLVENDOR,
        USPSAVEVENDOR,
        USPDELETEVENDOR,
        USPGETVENDORBYID,
        USPGETPASSWORDHASH,
        USPSAVEOTP,
        USPRESETPASSWORD,
        USPSAVECLIENT,
        USPVERIFYOTP,
        USPGETEMPLOYEELIST,
        USPGETEMPLOYEEBYID,
        USPGETALLLOCATION,
        USPGETALLSERVICE,
        USPGETSERVICES,
        USPGETPRODUCTS,
        USPGETCASHREGISTER,
        USPGETCOLLISIONBYEMPID,
        //MembershipSetup
        USPGETMEMBERSHIPSETUP,
        USPGETSERVICEWITHPRICE,
        USPDELETEMEMBERSHIP,
        USPGETMEMBERSHIPBYID,
        USPSAVEMEMBERSHIPSETUP,
        USPGETALLVEHICLE,
        USPUPDATEVEHICLE,
        USPDELETECLIENTVEHICLE,
        USPGETVEHICLEBYID,
        USPSAVEVEHICLE,
        USPGETVEHICLE,
        uspGetVihicleMembership,
        uspUpdateVechicleMembership,
        uspGetVehicleById,
        USPCLOCKTIMEDETAILS
    }


}
