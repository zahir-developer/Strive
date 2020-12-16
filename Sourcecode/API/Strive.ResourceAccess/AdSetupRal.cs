﻿using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.AdSetup;
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
        private readonly Db _db;

        public AdSetupRal(ITenantHelper tenant) : base(tenant)
        {
            var dbConnection = tenant.db();
            _db = new Db(dbConnection);
        }
        public bool AddAdSetup(AdSetupDto adSetup)
        {
            return dbRepo.InsertPc(adSetup, "AdSetupId");
        }

        public bool UpdateAdSetup(AdSetupDto adSetup)
        {
            return dbRepo.UpdatePc(adSetup);
        }

        public bool DeleteAdSetup(int id)
        {
            _prm.Add("AdSetup", id.toInt());
            db.Save(EnumSP.AdSetup.USPDELETEADSETUP.ToString(), _prm);
            return true;
        }
        public List<AdSetupViewModel> GetAllAdSetup()
        {
            return db.Fetch<AdSetupViewModel>(EnumSP.AdSetup.USPGETALLADSETUP.ToString(), _prm);
        }

        public AdSetupViewModel GetAdSetupById(int id)
        {
            _prm.Add("@AdSetupId", id);
            var result = db.FetchSingle<AdSetupViewModel>(EnumSP.AdSetup.USPGETADSETUPBYID.ToString(), _prm);
            return result;
        }
    }
}
