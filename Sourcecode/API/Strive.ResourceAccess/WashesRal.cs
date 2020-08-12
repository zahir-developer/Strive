using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.Washes;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class WashesRal : RalBase
    {
        public WashesRal(ITenantHelper tenant) : base(tenant) { }
        public List<WashesViewModel> GetAllWashTime()
        {
            return db.Fetch<WashesViewModel>(SPEnum.USPGETJOB.ToString(), null);
        }

        public List<WashesViewModel> GetWashTimeDetail(int id)
        {

            _prm.Add("@JobId", id);
            var result = db.Fetch<WashesViewModel>(SPEnum.USPGETJOB.ToString(), _prm);
            return result;
        }
        public bool AddWashTime(WashesDto washes)
        {
            return dbRepo.SavePc(washes, "JobId");
        }
        public bool UpdateWashTime(WashesDto washes)
        {
            return dbRepo.SavePc(washes, "JobId");
        }
    }
}