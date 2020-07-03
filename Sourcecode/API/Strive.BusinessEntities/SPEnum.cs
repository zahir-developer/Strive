using System;
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
        USPGETEMPLOYEE,
        USPSAVEEMPLOYEE,
        USPGETSCHEMABYGUID,
        USPGETCODES,
        USPGETUSERBYAUTHID,
        //Location
        USPGETLOCATION,
        USPSAVELOCATION,
        USPDELETELOCATION,
        USPGETPRODUCT,
        USPDELETEPRODUCT,
        USPGETAllPRODUCT,
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
        uspGetCashRegisterDetails

    }

}
