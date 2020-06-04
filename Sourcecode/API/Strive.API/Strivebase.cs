using Microsoft.AspNetCore.Mvc;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strive.API
{
    public class Strivebase : ControllerBase
    {
        public Strivebase()
        {

        }

        protected GData GetGlobalData() => (GData)HttpContext.Items["gdata"];
    }
}
