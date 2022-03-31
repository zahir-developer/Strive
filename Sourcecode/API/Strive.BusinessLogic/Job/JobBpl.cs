using Microsoft.Extensions.Caching.Distributed;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Job
{
    public class JobBpl : Strivebase, IJobBpl
    {
        public JobBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {

        }

        public Result GetPrintJobDetail(int jobId)
        {
            return ResultWrap(new JobRal(_tenant).GetPrintJobDetail, jobId, "PrintJobDetail");
        }
    }
}
