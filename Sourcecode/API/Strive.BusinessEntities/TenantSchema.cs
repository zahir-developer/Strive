using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{
    public class TenantSchema
    {
        public string UserGuid { get; set; }
        public string Schemaname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime Firstlogin { get; set; }
        public DateTime Lastlogin { get; set; }
    }
}
