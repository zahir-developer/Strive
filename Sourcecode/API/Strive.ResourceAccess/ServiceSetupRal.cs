using Strive.BusinessEntities;
using Strive.BusinessEntities.Code;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.ServiceSetup;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.RepositoryCqrs;
using System.Collections.Generic;
using ServiceItem = Strive.BusinessEntities.DTO.ServiceSetup.ServiceItem;

namespace Strive.ResourceAccess
{
    public class ServiceSetupRal : RalBase
    {
        public ServiceSetupRal(ITenantHelper tenant) : base(tenant) { }

        public bool AddService(ServiceDto service)
        {
            return dbRepo.InsertPc(service,"ServiceId");
        }

        public bool UpdateService(Service service)
        {
            return dbRepo.Update(service);
        }

        public List<Code> GetAllServiceType()
        {
            return new CommonRal(_tenant).GetCodeByCategory(GlobalCodes.SERVICETYPE);
        }

        public ServiceListViewModel GetAllServiceSetup(SearchDto searchDto)
        {

            _prm.Add("@locationId", searchDto.LocationId);
            _prm.Add("@PageNo", searchDto.PageNo);
            _prm.Add("@PageSize", searchDto.PageSize);
            _prm.Add("@Query", searchDto.Query);
            _prm.Add("@SortOrder", searchDto.SortOrder);
            _prm.Add("@SortBy", searchDto.SortBy);
            _prm.Add("@Status", searchDto.Status);
            var result= db.FetchMultiResult<ServiceListViewModel>(SPEnum.USPGETSERVICES.ToString(), _prm);
            return result;
        }

        public ServiceViewModel GetServiceSetupById(int id)
        {
            _prm.Add("@ServiceId", id);
            var result = db.FetchSingle<ServiceViewModel>(SPEnum.USPGETSERVICEBYID.ToString(), _prm);
            return result;
        }

        public bool DeleteServiceById(int id)
        {
            _prm.Add("@tblServiceId", id.toInt());
            db.Save(SPEnum.USPDELETESERVICEBYID.ToString(), _prm);
            return true;
        }
       
        public List<ServiceDetailViewModel> GetAllServiceDetail(int locationId)
        {

            _prm.Add("@locationId", locationId);
            return db.Fetch<ServiceDetailViewModel>(SPEnum.USPGETALLSERVICEDETAIL.ToString(), _prm);
        }

        public List<ServiceItem> GetServicesWithPrice()
        {
            return db.Fetch<ServiceItem>(SPEnum.USPGETSERVICELIST.ToString(), null);
        }
    }
}

