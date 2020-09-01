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
    public class DetailsRal: RalBase
    {
        public DetailsRal(ITenantHelper tenant) : base(tenant) { }
        public List<AllWashesViewModel> GetAllDetails()
        {
            return db.Fetch<AllWashesViewModel>(SPEnum.USPGETALLDETAILJOB.ToString(), null);
        }

        public WashDetailViewModel GetDetailsById(int id)
        {

            _prm.Add("@JobId", id);
            var result = db.FetchMultiResult<WashDetailViewModel>(SPEnum.USPGETDETAILJOBBYID.ToString(), _prm);
            return result;
        }
        public bool AddDetails(DetailsDto details)
        {
            return dbRepo.InsertPc(details, "JobId");
        }
        public bool UpdateDetails(DetailsDto details)
        {
            return dbRepo.UpdatePc(details);
        }
    }
}
