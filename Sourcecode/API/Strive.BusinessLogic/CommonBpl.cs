using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Strive.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessLogic
{
    public class CommonBpl : Strivebase
    {
        public CommonBpl(IDistributedCache cache) : base(cache) { }

    }

}
