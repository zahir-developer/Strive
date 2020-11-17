using Strive.BusinessEntities.DTO.Dashboard;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.DashboardStatistics
{
    public interface IDashboardBpl
    {
        Result GetDashboardStatistics(DashboardDto dashboard);
    }
}
