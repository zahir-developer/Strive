using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.Model;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Details
{
    public interface IDetailsBpl
    {
        Result GetDetailsById(int id);
        Result AddDetails(DetailsDto details);
        Result UpdateDetails(DetailsDto details);
        Result GetAllBayById(int id);
        Result GetBaySchedulesDetails(DetailsGridDto detailsGrid);
        Result GetJobType();
        Result DeleteDetails(int id);
        Result AddEmployeeScheduleToDetails(EmployeeScheduleDetailsDto empSchedule);
        Result GetAllDetails(DetailsGridDto detailsGrid);
    }
}
