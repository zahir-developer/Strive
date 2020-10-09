using Strive.BusinessEntities.Model;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic
{
    public interface IWhiteLabelBpl
    {
        Result AddWhiteLabelling(WhiteLabel whiteLabel);
    }
}
