using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.AdSetup;
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

        public bool UpdateToggledeal(bool status)
        {
            _prm.Add("@Status", status);
            db.Save(SPEnum.USPUPDATETOGGLEDEALSTATUS.ToString(), _prm);
            return true;
        }

        public bool AddClientDeal(ClientDealDto addClientDeal)
        {
            _prm.Add("@DealId", addClientDeal.DealId);
            _prm.Add("@Date", addClientDeal.Date);
            _prm.Add("@ClientId", addClientDeal.ClientId);
            db.Save(EnumSP.AdSetup.USPADDCLIENTDEAL.ToString(), _prm);
            return true;
        }

        public ClientDealViewModel GetClientDeal(ClientDealDto addClientDeal)
        {
            _prm.Add("@DealId", addClientDeal.DealId);
            _prm.Add("@Date", addClientDeal.Date);
            _prm.Add("@ClientId", addClientDeal.ClientId);
            return db.FetchMultiResult<ClientDealViewModel>(EnumSP.AdSetup.USPGETCLIENTDEAL.ToString(), _prm);
        }

    }
}
