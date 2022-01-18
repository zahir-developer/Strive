using Strive.BusinessEntities.DTO;
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
        Result GetAllWashTime(SearchDto searchDto);
        Result GetWashTimeDetail(int id);
        Result GetLastServiceVisit(SearchDto searchDto);
        Result AddWashTime(WashesDto washes);
        Result UpdateWashTime(WashesDto washes);
        Result GetDailyDashboard(WashesDashboardDto dashboard);
        Result GetByBarCode(string barcode);
        Result GetMembershipListByVehicleId(int vehicleId);
        Result DeleteWashes(int id);

        Result GetWashTimeByLocationId(WashTimeDto washTimeDto);

        Result GetAllLocationWashTime(LocationStoreStatusDto locationStoreStatusDto);

        Result GetWashVehiclePrint(PrintTicketDto printTicketDto);
        
    }
}
