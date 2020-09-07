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
        public bool DeleteItemById(int serviceId)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@ServiceId", serviceId);
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
    }
}
