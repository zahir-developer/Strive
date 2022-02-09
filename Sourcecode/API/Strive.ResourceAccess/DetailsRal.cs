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
            return dbRepo.UpdatePc<DetailsDto>(details, "JobId");    
        }
        
        public bool UpdateDetails(DetailsDto details)
        {
            return dbRepo.UpdatePc(details, "Job");
        }
        public bool AddServiceEmployee(JobServiceEmployeeDto jobServiceEmployee)
        {
            return dbRepo.InsertPc(jobServiceEmployee,"JobServiceEmployeeId");
        }
        public BaySchedulesDetails GetBaySchedulesDetails(DetailsGridDto detailsGrid)
        {
            _prm.Add("@JobDate", detailsGrid.JobDate);
            _prm.Add("@LocationId", detailsGrid.LocationId);
            var result = db.FetchMultiResult<BaySchedulesDetails>(EnumSP.Details.USPGETBAYSCHEDULESDETAILS.ToString(), _prm);
            return result;
        }
        public DetailViewModel GetDetailsById(int id)
        {

            _prm.Add("@JobId", id);
            var result = db.FetchMultiResult<DetailViewModel>(EnumSP.Details.USPGETDETAILJOBBYID.ToString(), _prm);
            return result;
        }
        public List<BayListViewModel> GetAllBayById(int id)
        {

            _prm.Add("@LocationId", id);
            var result = db.Fetch<BayListViewModel>(EnumSP.Details.USPGETALLBAYLISTBYID.ToString(), _prm);
            return result;
        }
        public List<VehiclePastHistoryViewModel> GetPastClientNotesById(int id)
        {
            _prm.Add("@ClientId", id);
            var result = db.Fetch<VehiclePastHistoryViewModel>(EnumSP.Details.USPGETPASTCLIENTNOTESBYCLIENTID.ToString(), _prm);
            return result;
        }
        
        public List<JobTypeViewModel> GetJobType()
        {
            var result = db.Fetch<JobTypeViewModel>(EnumSP.Details.USPGETJOBTYPE.ToString(), null);
            return result;
        }
        public DetailsGridViewModel GetAllDetails(DetailsGridDto detailsGrid)
        {
            _prm.Add("@JobDate", detailsGrid.JobDate);
            _prm.Add("@LocationId", detailsGrid.LocationId);
            _prm.Add("@ClientId", detailsGrid.ClientId);

            var result = db.FetchMultiResult<DetailsGridViewModel>(EnumSP.Details.USPGETALLDETAILS.ToString(), _prm);
            return result;
        }

        public DetailsGridViewModel GetAllDetailSearch(SearchDto searchDto)
        {
            _prm.Add("@LocationId", searchDto.LocationId);
            _prm.Add("@ClientId", searchDto.ClientId);
            _prm.Add("@PageNo", searchDto.PageNo);
            _prm.Add("@PageSize", searchDto.PageSize);
            _prm.Add("@Search", searchDto.Query);
            _prm.Add("@SortOrder", searchDto.SortOrder);
            _prm.Add("@SortBy", searchDto.SortBy);
            _prm.Add("@StartDate", searchDto.StartDate);
            _prm.Add("@EndDate", searchDto.EndDate);
            var result = db.FetchMultiResult<DetailsGridViewModel>(EnumSP.Details.USPGETALLDETAILS.ToString(), _prm);
            return result;
        }
        
        public bool DeleteDetails(int id)
        {
            _prm.Add("@JobId", id);
            db.Save(EnumSP.Details.USPDELETEDETAILSCHEDULE.ToString(), _prm);
            return true;
        }

        public List<DetailScheduleStatusViewModel> GetDetailScheduleStatus(DetailScheduleDto scheduleDto)
        {
            _prm.Add("LocationId", scheduleDto.LocationId);
            _prm.Add("Date", scheduleDto.Date);
            var result = db.Fetch<DetailScheduleStatusViewModel>(EnumSP.Details.USPGETDETAILSCHEDULESTATUS.ToString(), _prm);
            return result;
        }

        public bool UpdateJobStatus(JobStatusDto jobStatus)
        {
            _prm.Add("@Date", jobStatus.ActualTimeOut);
            _prm.Add("@JobId", jobStatus.JobId);
            _prm.Add("@JobStatus", jobStatus.JobStatus);
            _prm.Add("@JobStatusId", jobStatus.JobStatusId);
            db.Save(EnumSP.Details.USPUPDATEJOBSTATUS.ToString(), _prm);
            return true;
        }

        public List<EmployeeDetailJobViewModel> GetEmployeeAssignedDetail(int employeeId, DateTime jobDate)
        {
            _prm.Add("EmployeeId", employeeId);
            _prm.Add("JobDate", jobDate);
            return db.Fetch<EmployeeDetailJobViewModel>(EnumSP.Details.USPGETEMPLOYEEASSIGNEDDETAIL.ToString(), _prm);
        }
    }
}
