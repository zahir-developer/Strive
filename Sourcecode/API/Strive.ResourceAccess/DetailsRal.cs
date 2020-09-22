using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Details;
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

        public bool AddDetails(DetailsDto details)
        {
            return dbRepo.InsertPc(details, "JobId");
            //Job a = details.Job;
            ////return dbRepo.Insert(a);
            //int id = dbRepo.Add(a);
            //TestDto detail = new TestDto();
            //detail.JobDetail = details.JobDetail;
            //detail.JobItem = details.JobItem;
            //detail.BaySchedule = details.BaySchedule;

            //detail.JobDetail.JobId = id;
            //detail.BaySchedule.JobId = id;
            //foreach (var item in detail.JobItem)
            //{
            //    item.JobId = id;
            //}
            //Insert(detail);
            //return id;
        }
        //public bool Insert(TestDto details)
        //{
        //    return dbRepo.InsertPc(details,"JobId");
        //}
        public bool UpdateDetails(DetailsDto details)
        {
            return dbRepo.UpdatePc(details);
        }
        public bool AddEmployeeWithService(JobServiceEmployeeDto jobServiceEmployee)
        {
            return dbRepo.InsertPc(jobServiceEmployee,"JobServiceEmployeeId");
        }
        public BaySchedulesDetails GetBaySchedulesDetails(DetailsGridDto detailsGrid)
        {
            _prm.Add("@JobDate", detailsGrid.JobDate);
            _prm.Add("@LocationId", detailsGrid.LocationId);
            var result = db.FetchMultiResult<BaySchedulesDetails>(SPEnum.USPGETBAYSCHEDULESDETAILS.ToString(), _prm);
            return result;
        }
        public DetailViewModel GetDetailsById(int id)
        {

            _prm.Add("@JobId", id);
            var result = db.FetchMultiResult<DetailViewModel>(SPEnum.USPGETDETAILJOBBYID.ToString(), _prm);
            return result;
        }
        public List<BayListViewModel> GetAllBayById(int id)
        {

            _prm.Add("@LocationId", id);
            var result = db.Fetch<BayListViewModel>(SPEnum.USPGETALLBAYLISTBYID.ToString(), _prm);
            return result;
        }
        public List<VehiclePastHistoryViewModel> GetPastClientNotesById(int id)
        {
            _prm.Add("@ClientId", id);
            var result = db.Fetch<VehiclePastHistoryViewModel>(SPEnum.USPGETPASTCLIENTNOTESBYCLIENTID.ToString(), _prm);
            return result;
        }
        
        public List<JobTypeViewModel> GetJobType()
        {
            var result = db.Fetch<JobTypeViewModel>(SPEnum.USPGETJOBTYPE.ToString(), null);
            return result;
        }
        public DetailsGridViewModel GetAllDetails(DetailsGridDto detailsGrid)
        {
            _prm.Add("@JobDate", detailsGrid.JobDate);
            _prm.Add("@LocationId", detailsGrid.LocationId);
            var result = db.FetchMultiResult<DetailsGridViewModel>(SPEnum.USPGETALLDETAILS.ToString(), _prm);
            return result;
        }
        public bool DeleteDetails(int id)
        {
            _prm.Add("@JobId", id);
            db.Save(SPEnum.USPDELETEDETAILSCHEDULE.ToString(), _prm);
            return true;
        }
       
    }
}
