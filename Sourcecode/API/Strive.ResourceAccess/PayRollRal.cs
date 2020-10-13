using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class PayRollRal : RalBase
    {
        public PayRollRal(ITenantHelper tenant) : base(tenant) { }

        public PayRollViewModel GetPayRoll(PayRollDto payRoll)
        {
            _prm.Add("@LocationId", payRoll.LocationId);
            _prm.Add("@StartDate", payRoll.StartDate);
            _prm.Add("@EndDate", payRoll.EndDate);
            var res = db.FetchMultiResult<PayRollViewModel>(SPEnum.uspGetPayrollList.ToString(), _prm);
            return res;
        }
    }
}
