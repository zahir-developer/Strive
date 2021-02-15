using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Washes;
using Strive.BusinessEntities.Model;
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
        public WashesListViewModel GetAllWashTime(SearchDto searchDto)
        {
            _prm.Add("@locationId", searchDto.LocationId);
            _prm.Add("@PageNo", searchDto.PageNo);
            _prm.Add("@PageSize", searchDto.PageSize);
            _prm.Add("@Query", searchDto.Query);
            _prm.Add("@SortOrder", searchDto.SortOrder);
            _prm.Add("@SortBy", searchDto.SortBy);
            _prm.Add("@StartDate", searchDto.StartDate);
            _prm.Add("@EndDate", searchDto.EndDate);

            return db.FetchMultiResult<WashesListViewModel>(SPEnum.USPGETALLJOB.ToString(), _prm);
        }

        public WashDetailViewModel GetWashTimeDetail(int id)
        {

            _prm.Add("@JobId", id);
            var result = db.FetchMultiResult<WashDetailViewModel>(SPEnum.USPGETJOBBYID.ToString(), _prm);
            return result;
        }
        public bool AddWashTime(WashesDto washes)
        {
            return dbRepo.InsertPc(washes, "JobId");
        }
        public bool UpdateWashTime(WashesDto washes)
        {
            return dbRepo.UpdatePc(washes);
        }
        public WashesDashboardViewModel GetDailyDashboard(WashesDashboardDto dashboard)
        {
            _prm.Add("@LocationId",dashboard.id);
            _prm.Add("@CurrentDate",dashboard.date);
            _prm.Add("@JobType", dashboard.JobType);
            var result =  db.FetchSingle<WashesDashboardViewModel>(SPEnum.USPGETWASHDASHBOARD.ToString(), _prm);
            return result;
        }
        public List<ClientVehicleViewModel> GetByBarCode(string barcode)
        {

            _prm.Add("@BarCode", barcode);
            var result = db.Fetch<ClientVehicleViewModel>(SPEnum.USPGETCLIENTANDVEHICLEDETAIL.ToString(), _prm);
            return result;
        }
        public List<ClientVehicleViewModel> GetMembershipListByVehicleId(int vehicleId)
        {
            _prm.Add("@VehicleId", vehicleId);
            var result = db.Fetch<ClientVehicleViewModel>(SPEnum.uspGetMembershipListByVehicleId.ToString(), _prm);
            return result;
        }
        public bool DeleteWashes(int id)
        {
            _prm.Add("@JobId", id);
            db.Save(SPEnum.USPDELETEWASHES.ToString(), _prm);
            return true;
        }
    }
}