using Microsoft.Office.Interop.Excel;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.AdSetup;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class AdSetupRal : RalBase
    {
        public AdSetupRal(ITenantHelper tenant) : base(tenant)
        {
           
        }
        public bool AddAdSetup(AdSetupAddDto adSetup)
        {
            return dbRepo.InsertPc(adSetup, "AdSetupId");
        }

        public bool UpdateAdSetup(AdSetupAddDto adSetup)
        {
            return dbRepo.UpdatePc(adSetup);
        }

        public bool DeleteAdSetup(int id)
        {
            _prm.Add("@AdSetupId", id.toInt());
            db.Save(EnumSP.AdSetup.USPDELETEADSETUP.ToString(), _prm);
            return true;
        }
        public List<AdSetupViewModel> GetAllAdSetup()
        {
            return db.Fetch<AdSetupViewModel>(EnumSP.AdSetup.USPGETALLADSETUP.ToString(), _prm);
        }

        public AdSetupDetailViewModel GetAdSetupById(int id)
        {
            _prm.Add("@AdSetupId", id);
            var result = db.FetchSingle<AdSetupDetailViewModel>(EnumSP.AdSetup.USPGETADSETUPBYID.ToString(), _prm);
            return result;
        }

    }
}
