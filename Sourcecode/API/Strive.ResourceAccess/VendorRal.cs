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
        public int AddVendor(VendorDTO vendor)
        {
            return dbRepo.InsertPK(vendor, "VendorId");
        }
        public bool UpdateVendor(VendorDTO vendor)
        {
            return dbRepo.UpdatePc(vendor);
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
            var result = db.Fetch<VendorViewModel>(SPEnum.USPGETVENDORBYID.ToString(), _prm);
            return result;
        }

        public List<VendorViewModel> GetVendorByIds(string ids)
        {
            _prm.Add("@VendorId", ids);
            var result = db.Fetch<VendorViewModel>(SPEnum.USPGETVENDORBYID.ToString(), _prm);
            return result;
        }

        public List<VendorViewModel> GetVendorSearch(VendorSearchDto search)
        {
            _prm.Add("@VendorSearch", search.VendorSearch);
            var result = db.Fetch<VendorViewModel>(SPEnum.USPGETALLVENDOR.ToString(), _prm);
            return result;
        }
        public List<VendorNameViewModel> GetAllVendorName()
        {
            return db.Fetch<VendorNameViewModel>(SPEnum.USPGETALLVENDORNAME.ToString(), null);
        }

        public List<VendorProductViewModel> GetVendorByProductId(int id)
        {
            _prm.Add("@ProductId", id);
            return db.Fetch<VendorProductViewModel>(SPEnum.USPGETVENDORBYPRODUCTID.ToString(), _prm);
        }
    }

}