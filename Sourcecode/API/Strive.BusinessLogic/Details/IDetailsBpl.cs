using Strive.BusinessEntities.DTO;
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
        Result GetScheduleDetailsByDate(DateTime date);
        Result GetJobType();
        Result DeleteDetails(int id);
    }
}
