using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.AdSetup;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strive.BusinessLogic.DealSetup
{
   public interface IdealSetupBpl
    {
        Result AddDealSetup(Deals dealSetup);
        Result GetAllDeals();
        Result UpdateToggledeal(bool status);
        Result AddClientDeal(ClientDealDto addClientDeal);
        Result GetClientDeal(ClientDealDto addClientDeal);
    }
}
