using Strive.BusinessEntities.DTO.Washes;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Washes
{
    public interface IWashesBpl
    {
        Result GetAllWashTime();
        Result GetWashTimeDetail(int id);
        Result AddWashTime(WashesDto washes);
        Result UpdateWashTime(WashesDto washes);
        Result GetDailyDashboard(DashboardDto dashboard);
        Result GetByBarCode(string barcode);
    }
}
