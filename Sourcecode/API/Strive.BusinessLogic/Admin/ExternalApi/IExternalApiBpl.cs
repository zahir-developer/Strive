using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Admin.ExternalApi
{
    public interface IExternalApiBpl
    {
        void GoogleCalendarApiWebHook();
        void GetGoogleCalendarEvents();
    }
}
