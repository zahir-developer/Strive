using Microsoft.Office.Interop.Excel;
using Strive.BusinessEntities;
using Strive.BusinessEntities.DTO.AdSetup;
using Strive.BusinessEntities.Model;
using Strive.BusinessEntities.ViewModel;
using Strive.Common;
using Strive.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.ResourceAccess
{
    public class JobRal : RalBase
    {
        public JobRal(ITenantHelper tenant) : base(tenant)
        {
           
        }
        public PrintJobDetail GetPrintJobDetail(int jobId)
        {
            _prm.Add("JobId", jobId);
            return db.FetchMultiResult<PrintJobDetail>(EnumSP.Job.USPGETPRINTJOBDETAILBYID.ToString(), _prm);
        }
        
    }
}
