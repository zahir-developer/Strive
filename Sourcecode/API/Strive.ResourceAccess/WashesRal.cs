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
        public List<AllWashesViewModel> GetAllWashTime()
        {
            return db.Fetch<AllWashesViewModel>(SPEnum.USPGETALLJOB.ToString(), null);
        }

        public List<WashesViewModel> GetWashTimeDetail(int id)
        {

            _prm.Add("@JobId", id);
            var result = db.Fetch<WashesViewModel>(SPEnum.USPGETJOBBYID.ToString(), _prm);
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
        public WashesDashboardViewModel GetDailyDashboard(DashboardDto dashboard)
        {
            _prm.Add("@LocationId",dashboard.id);
            _prm.Add("@CurrentDate",dashboard.date);
            var result =  db.FetchMultiResult<WashesDashboardViewModel>(SPEnum.USPGETWASHDASHBOARD.ToString(), _prm);
            return result;
        }
        public List<ClientVehicleViewModel> GetByBarCode(string barcode)
        {

            _prm.Add("@BarCode", barcode);
            var result = db.Fetch<ClientVehicleViewModel>(SPEnum.USPGETCLIENTANDVEHICLEDETAIL.ToString(), _prm);
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