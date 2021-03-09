﻿using Dapper;
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
            dynParams.Add("@ServiceId", salesItemUpdateDto.ServiceId);
            dynParams.Add("@Quantity", salesItemUpdateDto.Quantity);
            dynParams.Add("@Price", salesItemUpdateDto.Price);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Sales.uspUpdateSalesItem.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }

        public bool SaveProductItem(SalesProductItemDto salesProductItemDto)
        {
            return dbRepo.InsertPc(salesProductItemDto, "JobProductItemId");
        }

        public bool DeleteItemById(DeleteItemDto itemDto)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@JobItemId", itemDto.ItemId);
            dynParams.Add("@IsJobItem", itemDto.IsJobItem);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Sales.uspDeleteSalesItemById.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public SalesViewModel GetItemList(SalesListItemDto salesListItemDto)
        {
            _prm.Add("@TicketNumber", salesListItemDto.TicketNumber);
            _prm.Add("@Quantity", salesListItemDto.Quantity);
            var result = db.FetchSingle<SalesViewModel>(EnumSP.Sales.uspGetItemList.ToString(), _prm);
            return result;
        }
        public SalesAccountDeatilViewModel GetAccountDetails(SalesAccountDto salesAccountDto)
        {
            _prm.Add("@TicketNumber", salesAccountDto.TicketNumber);
            return  db.FetchMultiResult<SalesAccountDeatilViewModel>(EnumSP.Sales.USPGETACCOUNTDETAILS.ToString(), _prm);
            
        }
        public List<EmailListViewModel> GetEmailId()
        {
            return db.Fetch<EmailListViewModel>(EnumSP.Sales.USPGETEMAILID.ToString(), _prm);

        }
        public string GetTicketNumber()
        {
            return db.FetchSingle<string>(SPEnum.USPGETTICKETNUMBER.ToString(), _prm);

        }
        public SalesItemListViewModel GetScheduleByTicketNumber(string ticketNumber)
        {
            _prm.Add("@TicketNumber", ticketNumber);
            return db.FetchMultiResult<SalesItemListViewModel>(EnumSP.Sales.uspGetItemListByTicketNumber.ToString(), _prm);

        }
        public int AddPayment(SalesPaymentDto salesPayment)
        {
            return dbRepo.InsertPK(salesPayment, "JobPaymentId");
        }
        public bool AddListItem(SalesAddListItemDto salesAddListItem)
        {
            return dbRepo.InsertPc(salesAddListItem, "JobId");
        }
        public bool UpdateListItem(SalesUpdateItemDto salesUpdateItemDto)
        {
            return dbRepo.InsertPc(salesUpdateItemDto, "JobId");
        }
        public List<ServiceItemDto> GetServicesWithPrice()
        {
            return db.Fetch<ServiceItemDto>(EnumSP.Sales.uspGetServiceByItemList.ToString(), null);
        }
        public bool DeleteJob(SalesItemDeleteDto salesItemDeleteDto)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@TicketNumber", salesItemDeleteDto.TicketNumber);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Sales.USPDELETEJOBITEMS.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public bool RollBackPayment(SalesItemDeleteDto salesItemDeleteDto)
        {
            DynamicParameters dynParams = new DynamicParameters();
            dynParams.Add("@TicketNumber", salesItemDeleteDto.TicketNumber);
            CommandDefinition cmd = new CommandDefinition(EnumSP.Sales.USPROLLBACKPAYMENT.ToString(), dynParams, commandType: CommandType.StoredProcedure);
            db.Save(cmd);
            return true;
        }
        public ServiceAndProductViewModel GetServicesAndProduct()
        {
            return db.FetchMultiResult<ServiceAndProductViewModel>(EnumSP.Sales.USPGETALLSERVICEANDPRODUCTLIST.ToString(), null);
        }

        public bool UpdateJobPayement(int? jobId, int jobPaymentid)
        {
            _prm.Add("JobId", jobId);
            _prm.Add("JobPaymentid", jobPaymentid);
            db.Save(EnumSP.Sales.USPUPDATEJOBPAYMENT.ToString(), _prm);
            return true;
        }

        public bool UpdateProductQuantity(int? qaunatity, int? productId)
        {
            _prm.Add("Quantity", qaunatity);
            _prm.Add("ProductId", productId);
            db.Save(EnumSP.Sales.USPUPDATEPRODUCTQUANTITY.ToString(), _prm);
            return true;
        }
    }
}
