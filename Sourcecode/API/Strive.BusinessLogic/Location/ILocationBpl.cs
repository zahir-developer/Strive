using Strive.BusinessEntities;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic
{
    public interface ILocationBpl
    {
        Result GetAllLocation();

        Result SaveLocation(List<Location> Location);
        
    }
}
