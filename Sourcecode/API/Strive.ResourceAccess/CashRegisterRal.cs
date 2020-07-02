using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.CashRegister;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Strive.ResourceAccess
{
    public class CashRegisterRal
    {
        IDbConnection _dbconnection;
        public Db db;
        public CashRegisterRal(IDbConnection dbconnection)
        {
            _dbconnection = dbconnection;
        }

        public CashRegisterRal(ITenantHelper tenant)
        {
            _dbconnection = tenant.db();
            db = new Db(_dbconnection);
        }
     
        public bool SaveTodayCashRegister(List<CashRegisterConsolidate> lstCashRegisterConsolidate)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpCashRegister", lstCashRegisterConsolidate.ToDataTable().AsTableValuedParameter("tvpCashRegister"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVETODAYCASHREGISTER.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public List<CashRegisterConsolidate> GetCashRegisterByDate(DateTime dateTime)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<CashRegisterConsolidate> lstResource = new List<CashRegisterConsolidate>();
            dynParams.Add("@currentDate", dateTime);
            var res = db.Fetch<CashRegisterConsolidate>(SPEnum.USPGETCASHREGISTERBYDATE.ToString(), dynParams);
            return res;
        }
    }
}
