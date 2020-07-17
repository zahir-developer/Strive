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
using System.Linq;

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

        public bool SaveTodayCashRegister(CashRegisterView cashRegister)
        {
            DynamicParameters dynParams = new DynamicParameters();
            List<CashRegister> lstCashReg = new List<CashRegister>();

            var lstCashRegister = new List<CashRegister>();
            var lstCashRegisterBill = new List<CashRegisterBill>();
            var lstCashRegisterCoin = new List<CashRegisterCoin>();
            var lstCashRegisterRoll = new List<CashRegisterRoll>();
            var lstCashRegisterOther = new List<CashRegisterOther>();

            lstCashRegister.Add(cashRegister);
            lstCashRegisterBill.Add(cashRegister.CashRegisterBill);
            lstCashRegisterCoin.Add(cashRegister.CashRegisterCoin);
            lstCashRegisterRoll.Add(cashRegister.CashRegisterRoll);
            lstCashRegisterOther.Add(cashRegister.CashRegisterOther);

            dynParams.Add("@tvpCashRegister", lstCashRegister.ToDataTable().AsTableValuedParameter("tvpCashRegister"));
            dynParams.Add("@tvpCashRegisterBills", lstCashRegisterBill.ToDataTable().AsTableValuedParameter("tvpCashRegisterBills"));
            dynParams.Add("@tvpCashRegisterCoins", lstCashRegisterCoin.ToDataTable().AsTableValuedParameter("tvpCashRegisterCoins"));
            dynParams.Add("@tvpCashRegisterOthers", lstCashRegisterOther.ToDataTable().AsTableValuedParameter("tvpCashRegisterOthers"));
            dynParams.Add("@tvpCashRegisterRolls", lstCashRegisterRoll.ToDataTable().AsTableValuedParameter("tvpCashRegisterRolls"));
            CommandDefinition cmd = new CommandDefinition(SPEnum.USPSAVECASHREGISTER.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

        public bool SaveCashRegisterNewApproach(List<CashRegisterView> lstCashRegisterConsolidate)
        {
            var lstCmd = new List<(CommandDefinition, object)>();
            var CRModel = lstCashRegisterConsolidate.FirstOrDefault();
            var cr = new CR()
            {
                LocationId = 1,
                DrawerId = 1,
                CashRegisterType = 119,
                EnteredDateTime = DateTime.Now,
                UserId = 1
            };


            lstCmd.Add(GetCmd(CRModel.CashRegisterBill, "tvpCashRegisterBills", SPEnum.USPSAVECASHREGISTERBILLS.ToString(), "CashRegBillId"));
            lstCmd.Add(GetCmd(CRModel.CashRegisterCoin, "tvpCashRegisterCoins", SPEnum.USPSAVECASHREGISTERCOINS.ToString(), "CashRegCoinId"));
            lstCmd.Add(GetCmd(CRModel.CashRegisterOther, "tvpCashRegisterOthers", SPEnum.USPSAVECASHREGISTEROTHERS.ToString(), "CashRegOtherId"));
            lstCmd.Add(GetCmd(CRModel.CashRegisterRoll, "tvpCashRegisterRolls", SPEnum.USPSAVECASHREGISTERROLLS.ToString(), "CashRegRollId"));
            lstCmd.Add(GetCmd(cr, "tvpCashRegister", SPEnum.USPSAVECASHREGISTERMAIN.ToString(), null));
            var BillId = db.SaveParentChild(lstCmd);
            return true;
        }





        public List<CashRegisterView> GetCashRegisterDetails(CashRegisterType cashRegisterType, int locationId, DateTime dateTime)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@LocationId", locationId);
            dynParams.Add("@CashRegisterType", cashRegisterType.ToString());
            dynParams.Add("@EnteredDate", dateTime.ToString("yyy-MM-dd"));
            var result = db.FetchRelation4<CashRegisterView, CashRegisterCoin, CashRegisterBill, CashRegisterRoll, CashRegisterOther>(SPEnum.USPGETCASHREGISTERDETAILS.ToString(), dynParams);
            return result;
        }


        private (CommandDefinition, object) GetCmd<T>(T model, string name, string spName, object parentmapId) where T : new()
        {
            List<T> lstModel = new List<T>
            {
                model
            };
            DynamicParameters dyn = new DynamicParameters();
            dyn.Add("@" + name, lstModel.ToDataTable().AsTableValuedParameter(name));
            CommandDefinition cmd = new CommandDefinition(spName, dyn, commandType: CommandType.StoredProcedure);

            if (parentmapId is null)
            {
                parentmapId = lstModel.ToDataTable(name);
            }

            return (cmd, parentmapId);
        }
    }

}
