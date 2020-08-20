using Dapper;
using Strive.BusinessEntities;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Strive.BusinessEntities.Vendor;
using Strive.BusinessEntities.ViewModel;
using Strive.BusinessEntities.DTO.Vendor;

namespace Strive.ResourceAccess
{
    public class VendorRal : RalBase
    {
        public VendorRal(ITenantHelper tenant) : base(tenant) { }
        public List<VendorViewModel> GetVendorDetails()
        {
            return db.Fetch<VendorViewModel>(SPEnum.USPGETALLVENDOR.ToString(), null);
        }
        public bool AddVendor(VendorDTO vendor)
        {
            return dbRepo.SavePc(vendor, "VendorId");
        }
        public bool UpdateVendor(VendorDTO vendor)
        {
            return dbRepo.SavePc(vendor, "VendorId");
        }

        public bool DeleteVendorById(int id)
        {
            _prm.Add("@VendorId", id);
            db.Save(SPEnum.USPDELETEVENDOR.ToString(), _prm);
            return true;
        }

        public List<VendorViewModel> GetVendorById(int id)
        {
            _prm.Add("@VendorId", id);
            var result = db.Fetch<VendorViewModel>(SPEnum.USPGETALLVENDOR.ToString(), _prm);
            return result;
        }
        public List<VendorViewModel> GetVendorSearch(VendorSearchDto search)
        {
            _prm.Add("@VendorSearch", search.VendorSearch);
            var result = db.Fetch<VendorViewModel>(SPEnum.USPGETALLVENDOR.ToString(), _prm);
            return result;
        }
    }

}