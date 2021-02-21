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
    public class DealSetupRal : RalBase
    {
        public DealSetupRal(ITenantHelper tenant) : base(tenant) { }
    
        public bool AddDealSetup(Deals dealSetup)
        {
            return dbRepo.SavePc(dealSetup, "dealId");
        }
        public List<DealSetUpViewModel> GetAllDeals()
        {
            return db.Fetch<DealSetUpViewModel>(SPEnum.USPGETALLDEALS.ToString(), null);
        }
    }
}
