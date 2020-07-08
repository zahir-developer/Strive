using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.CashRegister;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
     
        public bool SaveTodayCashRegister(List<CashRegisterList> lstCashRegister)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<CashRegister> lstCashReg = new List<CashRegister>();
            var cashReg = lstCashRegister.FirstOrDefault();
            lstCashReg.Add(new CashRegister {
                CashRegisterId = cashReg.CashRegisterId,
                CashRegisterType = cashReg.CashRegisterType,
                LocationId = cashReg.LocationId,
                DrawerId = cashReg.DrawerId,
                UserId = cashReg.UserId,
                EnteredDateTime = cashReg.EnteredDateTime,
                CashRegRollId = cashReg.CashRegRollId,
                CashRegCoinId = cashReg.CashRegCoinId,
                CashRegBillId = cashReg.CashRegBillId,
                CashRegOthersId = cashReg.CashRegOthersId,
                
            });
            dynParams.Add("@tvpCashRegister", lstCashReg.ToDataTable().AsTableValuedParameter("tvpCashRegister"));
            dynParams.Add("@tvpCashRegisterBills", cashReg.CashRegisterBill.ToDataTable().AsTableValuedParameter("tvpCashRegisterBills"));
            dynParams.Add("@tvpCashRegisterCoins", cashReg.CashRegisterCoin.ToDataTable().AsTableValuedParameter("tvpCashRegisterCoins"));
            dynParams.Add("@tvpCashRegisterOthers", cashReg.CashRegisterOther.ToDataTable().AsTableValuedParameter("tvpCashRegisterOthers"));
            dynParams.Add("@tvpCashRegisterRolls", cashReg.CashRegisterRoll.ToDataTable().AsTableValuedParameter("tvpCashRegisterRolls"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVECASHREGISTER.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public List<CashRegisterList> GetCashRegisterByDate(DateTime dateTime)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<CashRegisterList> lstResource = new List<CashRegisterList>();
            dynParams.Add("@EnteredDate", dateTime);
            var res = db.FetchRelation4<CashRegisterList, CashRegisterCoin, CashRegisterBill, CashRegisterRoll, CashRegisterOther>(SPEnum.USPGETCASHREGISTERDETAILS.ToString(), dynParams);
            return res;
        }
    }
}
