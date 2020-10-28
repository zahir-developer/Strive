using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.CheckoutEntry;
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
        public List<CheckOutViewModel> GetCheckedInVehicleDetails()
        {
            return db.Fetch<CheckOutViewModel>(SPEnum.USPGETCHECKEDINVEHICLEDETAILS.ToString(), _prm);
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
    }
}
