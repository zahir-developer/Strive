using Strive.BusinessEntities.DTO.AdSetup;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.AdSetup
{
    public interface IAdSetupBpl
    {
        Result AddAdSetup(AdSetupDto adSetup);
        Result UpdateAdSetup(AdSetupDto adSetup);
        Result DeleteAdSetup(int id);
        Result GetAllAdSetup();
        Result GetAdSetupById(int id);
    }
}
