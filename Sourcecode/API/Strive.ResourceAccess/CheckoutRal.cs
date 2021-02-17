﻿using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CheckoutEntry;
using Strive.BusinessEntities.DTO.Report;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class CheckoutRal : RalBase
    {
        public CheckoutRal(ITenantHelper tenant) : base(tenant) { }
        public CheckOutGridViewModel GetAllCheckoutDetails(SearchDto checkoutDto)
        {
            _prm.Add("locationid", checkoutDto.LocationId);
            _prm.Add("PageNo", checkoutDto.PageNo);

            _prm.Add("PageSize", checkoutDto.PageSize);

            _prm.Add("@Query", checkoutDto.Query);
            _prm.Add("@SortOrder", checkoutDto.SortOrder);
            _prm.Add("@SortBy", checkoutDto.SortBy);


            var result =  db.FetchMultiResult<CheckOutGridViewModel>(EnumSP.Checkout.USPGETAllCHECKOUTDETAILS.ToString(), _prm);
            return result;
        }
        public bool UpdateCheckoutDetails(CheckoutEntryDto checkoutEntry)
        {
            _prm.Add("JobId", checkoutEntry.id);
            _prm.Add("CheckOut", checkoutEntry.CheckOut);
            _prm.Add("ActualTimeOut", checkoutEntry.ActualTimeOut);
            db.Save(EnumSP.Checkout.USPUPDATECHECKOUTDETAILFORJOBID.ToString(), _prm);
            return true;
        }
        public bool UpdateJobStatusHold(CheckoutHoldDto checkoutHoldDto)
        {
            _prm.Add("JobId", checkoutHoldDto .id); 
            db.Save(EnumSP.Checkout.USPUPDATEJOBSTATUSHOLDBYJOBID.ToString(), _prm);
          
            return true;
        }
        public bool UpdateJobStatusComplete(JobIdDto jobIdDto)
        {
            _prm.Add("JobId", jobIdDto.id);
            db.Save(EnumSP.Checkout.USPUPDATEJOBSTATUSCOMPLETEBYJOBID.ToString(), _prm);
          
            return true;
        }

        public List<CheckOutViewModel> GetCustomerHistory(CustomerHistorySearchDto salesReportDto)
        {
            _prm.Add("locationid", salesReportDto.LocationId);
            _prm.Add("fromDate", salesReportDto.FromDate);
            _prm.Add("toDate", salesReportDto.EndDate);
            _prm.Add("@PageNo", salesReportDto.PageNo);
            _prm.Add("@PageSize", salesReportDto.PageSize);
            _prm.Add("@Query", salesReportDto.Query);
            _prm.Add("@SortOrder", salesReportDto.SortOrder);
            _prm.Add("@SortBy", salesReportDto.SortBy);

            return db.Fetch<CheckOutViewModel>(EnumSP.Checkout.USPGETCUSTOMERHISTORY.ToString(), _prm);

        }
    }
}
