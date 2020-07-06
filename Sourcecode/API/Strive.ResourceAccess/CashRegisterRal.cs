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
     
        public bool SaveTodayCashRegister(List<CashRegister> lstCashRegisterConsolidate)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@tvpCashRegister", lstCashRegisterConsolidate.ToDataTable().AsTableValuedParameter("tvpCashRegister"));
            dynParams.Add("@tvpCashRegisterBills", lstCashRegisterConsolidate.ToDataTable().AsTableValuedParameter("tvpCashRegisterBills"));
            dynParams.Add("@tvpCashRegisterCoins", lstCashRegisterConsolidate.ToDataTable().AsTableValuedParameter("tvpCashRegisterCoins"));
            dynParams.Add("@tvpCashRegisterOthers", lstCashRegisterConsolidate.ToDataTable().AsTableValuedParameter("tvpCashRegisterOthers"));
            dynParams.Add("@tvpCashRegisterRolls", lstCashRegisterConsolidate.ToDataTable().AsTableValuedParameter("tvpCashRegisterRolls"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVETODAYCASHREGISTER.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public List<CashRegister> GetCashRegisterDetails(CashRegisterType cashRegisterType, int locationId, DateTime dateTime)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@LocationId", locationId);
            dynParams.Add("@CashRegisterType", cashRegisterType.ToString());
            dynParams.Add("@EnteredDate", dateTime.ToString("yyy-MM-dd"));
            var result = db.FetchRelation4<CashRegister, CashRegisterCoin, CashRegisterBill, CashRegisterRoll, CashRegisterOther>(SPEnum.USPGETCASHREGISTERDETAILS.ToString(), dynParams);
            return result;
        }
    }
}
