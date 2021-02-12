using Strive.BusinessEntities;
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
        public List<CheckOutViewModel> GetAllCheckoutDetails(int locationId)
        {
            _prm.Add("locationid", locationId);
            return db.Fetch<CheckOutViewModel>(SPEnum.USPGETAllCHECKOUTDETAILS.ToString(), _prm);
        }
        public bool UpdateCheckoutDetails(CheckoutEntryDto checkoutEntry)
        {
            _prm.Add("JobId", checkoutEntry.id);
            _prm.Add("CheckOut", checkoutEntry.CheckOut);
            _prm.Add("ActualTimeOut", checkoutEntry.ActualTimeOut);
            db.Save(SPEnum.USPUPDATECHECKOUTDETAILFORJOBID.ToString(), _prm);
            return true;
        }
        public bool UpdateJobStatusHold(JobIdDto jobIdDto)
        {
            _prm.Add("JobId", jobIdDto.id); 
            db.Save(SPEnum.USPUPDATEJOBSTATUSHOLDBYJOBID.ToString(), _prm);
            return true;
        }
        public bool UpdateJobStatusComplete(JobIdDto jobIdDto)
        {
            _prm.Add("JobId", jobIdDto.id);
            db.Save(SPEnum.USPUPDATEJOBSTATUSCOMPLETEBYJOBID.ToString(), _prm);
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

            return db.Fetch<CheckOutViewModel>(SPEnum.USPGETCUSTOMERHISTORY.ToString(), _prm);

        }
    }
}
