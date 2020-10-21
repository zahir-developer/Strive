using Strive.BusinessEntities.DTO;
using Strive.BusinessEntities.DTO.PayRoll;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.PayRoll
{
    public interface IPayRollBpl
    {
        Result GetPayRoll(PayRollDto payRoll);
        Result AddPayRoll(PayRollAddDto payRollAdd);
    }
}
