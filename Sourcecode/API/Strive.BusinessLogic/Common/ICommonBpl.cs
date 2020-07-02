using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Strive.BusinessEntities;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic
{
    public interface ICommonBpl 
    {
        Result GetAllCodes();

        Result GetCodesByCategory(GlobalCodes CodeCategory);

        Result GetCodesByCategory(int CategoryId);

    }

}
