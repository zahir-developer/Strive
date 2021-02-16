﻿using Strive.BusinessEntities;
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

        public int AddDetails(DetailsDto details)
        {
            return dbRepo.InsertPK<DetailsDto>(details, "JobId");    
        }
        
        public bool UpdateDetails(DetailsDto details)
        {
            return dbRepo.UpdatePc(details);
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
        public bool DeleteDetails(int id)
        {
            _prm.Add("@JobId", id);
            db.Save(EnumSP.Details.USPDELETEDETAILSCHEDULE.ToString(), _prm);
            return true;
        }
       
    }
}
