using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Checkout;
using Strive.BusinessEntities.DTO.Details;
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
            _prm.Add("@StartDate", checkoutDto.StartDate);
            _prm.Add("@EndDate", checkoutDto.EndDate);

            var result =  db.FetchMultiResult<CheckOutGridViewModel>(EnumSP.Checkout.USPGETAllCHECKOUTDETAILS.ToString(), _prm);
            return result;
        }
        public bool UpdateCheckoutDetails(CheckOutDto checkoutDto)
        {
            _prm.Add("JobId", checkoutDto.JobId);
            _prm.Add("CheckOut", checkoutDto.CheckOut);
            _prm.Add("CheckOutTime", checkoutDto.CheckOutTime);
            db.Save(EnumSP.Checkout.USPUPDATECHECKOUTDETAIL.ToString(), _prm);
            return true;
        }
        public bool UpdateJobStatusHold(CheckoutHoldDto checkoutHoldDto)
        {
            _prm.Add("JobId", checkoutHoldDto .id);

            _prm.Add("IsHold", checkoutHoldDto.IsHold);
            db.Save(EnumSP.Checkout.USPUPDATEJOBSTATUSHOLD.ToString(), _prm);
          
            return true;
        }
        public bool UpdateJobStatusComplete(JobStatusDto jobStatusDto)
        {
            _prm.Add("JobId", jobStatusDto.JobId);
            _prm.Add("ActualTimeout", jobStatusDto.ActualTimeOut);
            db.Save(EnumSP.Checkout.USPUPDATEJOBSTATUSCOMPLETE.ToString(), _prm);
          
            return true;
        }

        public CustomerHistoryGridViewModel GetCustomerHistory(CustomerHistorySearchDto salesReportDto)
        {
            _prm.Add("locationid", salesReportDto.LocationId);
            _prm.Add("fromDate", salesReportDto.FromDate);
            _prm.Add("toDate", salesReportDto.EndDate);
            _prm.Add("@PageNo", salesReportDto.PageNo);
            _prm.Add("@PageSize", salesReportDto.PageSize);
            _prm.Add("@Query", salesReportDto.Query);
            _prm.Add("@SortOrder", salesReportDto.SortOrder);
            _prm.Add("@SortBy", salesReportDto.SortBy);

            return db.FetchMultiResult<CustomerHistoryGridViewModel>(EnumSP.Checkout.USPGETCUSTOMERHISTORY.ToString(), _prm);

        }
    }
}
