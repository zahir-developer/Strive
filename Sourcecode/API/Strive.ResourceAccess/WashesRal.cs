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

            return db.FetchMultiResult<WashesListViewModel>(EnumSP.Washes.USPGETALLJOB.ToString(), _prm);
        }

        public WashDetailViewModel GetWashTimeDetail(int id)
        {

            _prm.Add("@JobId", id);
            var result = db.FetchMultiResult<WashDetailViewModel>(EnumSP.Washes.USPGETWASHBYJOBID.ToString(), _prm);
            return result;
        }

        public WashDetailViewModel GetLastServiceVisit(SearchDto searchDto)
        {

            _prm.Add("@ClientId", searchDto.ClientId);
            _prm.Add("@VehicleId", searchDto.VehicleId);
            _prm.Add("@locationId", searchDto.LocationId);
            _prm.Add("@PageNo", searchDto.PageNo);
            _prm.Add("@PageSize", searchDto.PageSize);
            _prm.Add("@Query", searchDto.Query);
            _prm.Add("@SortOrder", searchDto.SortOrder);
            _prm.Add("@SortBy", searchDto.SortBy);
            _prm.Add("@StartDate", searchDto.StartDate);
            _prm.Add("@EndDate", searchDto.EndDate);
            var result = db.FetchMultiResult<WashDetailViewModel>(EnumSP.Washes.USPGETLASTSERVICEVISIT.ToString(), _prm);
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

            DateTime lastMonth = dashboard.date.AddMonths(-1).Date;
            DateTime lastweek = dashboard.date.AddDays(-7).Date;
            DateTime lastThirdMonth = dashboard.date.AddMonths(-3).Date;

            _prm.Add("@LocationId",dashboard.id);
            _prm.Add("@CurrentDate",dashboard.date);
            _prm.Add("@JobType", dashboard.JobType);
            //for forecasted car and employee
            _prm.Add("@lastweek", lastweek.ToString("yyyy-MM-dd"));
            _prm.Add("@lastMonth", lastMonth.ToString("yyyy-MM-dd"));
            _prm.Add("@lastThirdMonth", lastThirdMonth.ToString("yyyy-MM-dd"));
            var result =  db.FetchSingle<WashesDashboardViewModel>(EnumSP.Washes.USPGETWASHDASHBOARD.ToString(), _prm);
            return result;
        }
        public List<ClientVehicleViewModel> GetByBarCode(string barcode)
        {

            _prm.Add("@BarCode", barcode);
            var result = db.Fetch<ClientVehicleViewModel>(EnumSP.Washes.USPGETCLIENTANDVEHICLEDETAIL.ToString(), _prm);
            return result;
        }
        public List<ClientVehicleViewModel> GetMembershipListByVehicleId(int vehicleId)
        {
            _prm.Add("@VehicleId", vehicleId);
            var result = db.Fetch<ClientVehicleViewModel>(SPEnum.USPGETMEMBERSHIPLISTBYVEHICLEID.ToString(), _prm);
            return result;
        }
        public bool DeleteWashes(int id)
        {
            _prm.Add("@JobId", id);
            db.Save(SPEnum.USPDELETEWASHES.ToString(), _prm);
            return true;
        }

        public List<LocationDetailViewModel> GetWashTime(WashTimeDto washTimeDto)
        {

            _prm.Add("@LocationId",washTimeDto.LocationId);

            _prm.Add("@DateTime", washTimeDto.DateTime);
            var result = db.Fetch<LocationDetailViewModel>(EnumSP.Washes.USPGETWASHTIMEBYLOCATIONID.ToString(), _prm);
            return result;
        }


        public List<LocationWashTimeDto> GetAllLocationWashTime(int id)
        {

            _prm.Add("@LocationId", id);
            var result = db.Fetch<LocationWashTimeDto>(EnumSP.Washes.USPGETALLLOCATIONWASHTIME.ToString(), _prm);
            return result;
        }

        public List<LocationWashTimeDto> GetAllLocationWashTime(LocationStoreStatusDto locationStoreStatus)
        {

            _prm.Add("@LocationId", locationStoreStatus.LocationId);
            _prm.Add("@Date", locationStoreStatus.Date);
            var result = db.Fetch<LocationWashTimeDto>(EnumSP.Washes.USPGETALLLOCATIONWASHTIME.ToString(), _prm);
            return result;
        }
    }
}