using Strive.BusinessEntities;
using Strive.BusinessEntities.Code;
using Strive.BusinessEntities.DTO.ServiceSetup;
using Strive.BusinessEntities.Model;
using Strive.Common;
using Strive.RepositoryCqrs;
using System.Collections.Generic;

namespace Strive.ResourceAccess
{
    public class ServiceSetupRal : RalBase
    {
        public ServiceSetupRal(ITenantHelper tenant) : base(tenant) { }

        public bool AddService(Service service)
        {
            return dbRepo.Insert(service);
        }

        public bool UpdateService(Service service)
        {
            return dbRepo.Update(service);
        }

        public List<Code> GetAllServiceType()
        {
            return new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.SERVICETYPE);
        }

        public List<ServiceViewModel> GetAllServiceSetup()
        {
            return db.Fetch<ServiceViewModel>(SPEnum.USPGETSERVICES.ToString(), _prm);
        }

        public ServiceViewModel GetServiceSetupById(int id)
        {
            _prm.Add("@ServiceId", id);
            return db.FetchSingle<ServiceViewModel>(SPEnum.USPGETSERVICES.ToString(), _prm);
        }

        public bool DeleteServiceById(int id)
        {
            _prm.Add("@tblServiceId", id.toInt());
            db.Save(SPEnum.USPDELETESERVICEBYID.ToString(), _prm);
            return true;
        }
        public List<ServiceViewModel> GetServiceSearch(ServiceSearchDto search)
        {
            _prm.Add("@ServiceType", search.ServiceType);
            _prm.Add("@ServiceName", search.ServiceName);
            _prm.Add("@Status", search.Status);
            return db.Fetch<ServiceViewModel>(SPEnum.USPGETSERVICES.ToString(), _prm);
        }

    }
}

