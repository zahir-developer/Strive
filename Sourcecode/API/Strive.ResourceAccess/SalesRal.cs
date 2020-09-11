using Dapper;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Sales;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class SalesRal : RalBase
    {
        public SalesRal(ITenantHelper tenant) : base(tenant) { }

        //public bool UpdateSalesItem(SalesDto sales)
        //{
        //    return dbRepo.UpdatePc(sales);
        //}
        public bool UpdateItem(SalesItemUpdateDto salesItemUpdateDto)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@JobItemId", salesItemUpdateDto.JobItemId);
            dynParams.Add("@Quantity", salesItemUpdateDto.Quantity);
            dynParams.Add("@Price", salesItemUpdateDto.Price);
            CommandDefinition cmd = new CommandDefinition(SPEnum.uspUpdateSalesItem.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool DeleteItemById(int jobId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@JobId", jobId);
            CommandDefinition cmd = new CommandDefinition(SPEnum.uspDeleteSalesItemById.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public SalesViewModel GetItemList(SalesListItemDto salesListItemDto)
        {
            _prm.Add("@TicketNumber", salesListItemDto.TicketNumber);
            _prm.Add("@Quantity", salesListItemDto.Quantity);
            var result = db.FetchSingle<SalesViewModel>(SPEnum.uspGetItemList.ToString(), _prm);
            return result;
        }
        public List<ScheduleItemListViewModel> GetScheduleByTicketNumber(string ticketNumber)
        {
            _prm.Add("@TicketNumber", ticketNumber);
            var result = db.Fetch<ScheduleItemListViewModel>(SPEnum.uspGetItemListByTicketNumber.ToString(), _prm);
            return result;
        }
        public bool AddPayment(SalesPaymentDto salesPayment)
        {
            return dbRepo.SavePc(salesPayment, "JobPaymentId");
        }
        public bool AddListItem(SalesAddListItemDto salesAddListItem)
        {
            return dbRepo.InsertPc(salesAddListItem, "JobId");
        }
        public bool DeleteTransactions(SalesItemDeleteDto salesItemDeleteDto)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@TicketNumber", salesItemDeleteDto.TicketNumber);
            CommandDefinition cmd = new CommandDefinition(SPEnum.uspDeleteRollBackItems.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
    }
}
