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

namespace Strive.ResourceAccess
{
    public class VendorRal
    {
        IDbConnection _dbconnection;
        public Db db;
        public VendorRal(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        public VendorRal(ITenantHelper tenant)
        {
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }
        public List<VendorView> GetVendorDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<VendorView> lstVendor = new List<VendorView>();
            lstVendor= db.FetchRelation1<VendorView, VendorAddress>(SPEnum.USPGETALLVENDOR.ToString(), dynParams);
            return lstVendor;
        }

        public bool SaveVendorDetails(VendorView vendor)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Vendor> lstvendorlst = new List<Vendor>();
            dynParams.Add("@tvpVendor", vendor.TableName<Vendor>("tvpVendor"));
            dynParams.Add("@tvpVendorAddress", vendor.VendorAddress.ToDataTable().AsTableValuedParameter("tvpVendorAddress"));

            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVEVENDOR.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

        public bool DeleteVendorById(int id)
        { 
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@VendorId", id);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETEVENDOR.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);

            return true;
        }

        public List<VendorView> GetVendorById(int id)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@VendorId", id);
            List<VendorView> lstVendorById = new List<VendorView>();
            lstVendorById = db.FetchRelation1<VendorView, VendorAddress>(SPEnum.USPGETVENDORBYID.ToString(), dynParams);
            return lstVendorById;
        }
    }
}