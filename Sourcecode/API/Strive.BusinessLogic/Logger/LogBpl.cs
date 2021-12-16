using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Strive.Common;

namespace Strive.BusinessLogic.Logger
{
    public class LogBpl : Strivebase, ILogBpl
    {
        public LogBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {

        }

        public string ErrorLog(string log)
        {
            return log;
        }
    }
}
