using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
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
    public class DetailsRal : RalBase
    {
        public DetailsRal(ITenantHelper tenant) : base(tenant) { }

        public DetailViewModel GetDetailsById(int id)
        {

            _prm.Add("@JobId", id);
            var result = db.FetchMultiResult<DetailViewModel>(SPEnum.USPGETDETAILJOBBYID.ToString(), _prm);
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
        public List<BayListViewModel> GetAllBayById(int id)
        {

            _prm.Add("@LocationId", id);
            var result = db.Fetch<BayListViewModel>(SPEnum.USPGETALLBAYLISTBYID.ToString(), _prm);
            return result;
        }
        public DetailScheduleViewModel GetScheduleDetailsByDate(DateTime date)
        {
            _prm.Add("@JobDate", date);
            var result = db.FetchMultiResult<DetailScheduleViewModel>(SPEnum.USPGETSCHEDULEDETAILSBYDATE.ToString(), _prm);
            return result;
        }
        public List<JobTypeViewModel> GetJobType()
        {
            var result = db.Fetch<JobTypeViewModel>(SPEnum.USPGETJOBTYPE.ToString(), null);
            return result;
        }
        public bool DeleteDetails(int id)
        {
            _prm.Add("@JobId", id);
            db.Save(SPEnum.USPDELETEDETAILSCHEDULE.ToString(), _prm);
            return true;
        }
        public bool AddEmployeeScheduleToDetails(EmployeeScheduleDetailsDto empSchedule)
        {
            return dbRepo.InsertPc(empSchedule, "EmployeeScheduleId");
        }
        public List<DetailsGridViewModel> GetAllDetails(DetailsGridDto detailsGrid)
        {
            _prm.Add("@JobDate", detailsGrid.JobDate);
            _prm.Add("@LocationId", detailsGrid.LocationId);
            var result = db.Fetch<DetailsGridViewModel>(SPEnum.USPGETALLDETAILS.ToString(), _prm);
            return result;
        }
    }
}
