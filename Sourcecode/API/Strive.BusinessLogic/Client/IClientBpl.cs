using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strive.BusinessEntities.Client;
using Strive.Common;

namespace Strive.BusinessLogic.Client
{
    public interface IClientBpl
    {
       Result SaveClientDetails(ClientList lstClient);
    }
}
