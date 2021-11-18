using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.Details;
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
        Result AddDetails(DetailsDto details);
        Result UpdateDetails(DetailsDto details);
        Result AddServiceEmployee(JobServiceEmployeeDto jobServiceEmployee);
        Result GetBaySchedulesDetails(DetailsGridDto detailsGrid);
        Result GetDetailsById(int id);
        Result GetAllBayById(int id);
        Result GetPastClientNotesById(int id);
        Result GetJobType();
        Result GetAllDetails(DetailsGridDto detailsGrid);
        Result DeleteDetails(int id);
        Result GetDetailScheduleStatus(DetailScheduleDto scheduleDto);
        Result UpdateJobStatus(JobStatusDto jobStatus);
    }
}
