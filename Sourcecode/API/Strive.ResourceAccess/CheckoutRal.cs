using Strive.BusinessEntities;
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
        public List<CheckOutViewModel> GetUncheckedVehicleDetails()
        {
            return db.Fetch<CheckOutViewModel>(SPEnum.USPGETUNCHECKEDVEHICLEDETAILS.ToString(), _prm);
        }
    }
}
