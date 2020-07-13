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

namespace Strive.BusinessLogic
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
        public List<VendorList> GetVendorDetails()
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<VendorList> lstVendor = new List<VendorList>();
            lstVendor= db.FetchRelation1<VendorList, VendorAddress>(SPEnum.USPGETALLVENDOR.ToString(), dynParams);
            return lstVendor;
        }

        public bool SaveVendorDetails(List<VendorList> lstvendor)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<Vendor> lstvendorlst = new List<Vendor>();
            var vendorlst = lstvendor.FirstOrDefault();
            lstvendorlst.Add(new Vendor
            {
                VendorId = vendorlst.VendorId,
                VendorName = vendorlst.VendorName,
                VendorAlias = vendorlst.VendorAlias,
                VIN = vendorlst.VIN,
                IsActive = vendorlst.IsActive,

            });
            dynParams.Add("@tvpVendor", lstvendorlst.ToDataTable().AsTableValuedParameter("tvpVendor"));
            dynParams.Add("@tvpVendorAddress", vendorlst.VendorAddress.ToDataTable().AsTableValuedParameter("tvpVendorAddress"));

            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVEVENDOR.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        
        public bool DeleteVendorDetails(long empId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tblEmployeeId", empId);
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPDELETEVENDOR.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}