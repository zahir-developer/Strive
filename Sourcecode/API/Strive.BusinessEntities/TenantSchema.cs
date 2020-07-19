using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.BusinessEntities
{
    public class TenantSchema
    {
        public int AuthId { get; set; }
        public string UserGuid { get; set; }
        public string Schemaname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Guid TenantGuid { get; set; }
        public DateTime Firstlogin { get; set; }
        public DateTime Lastlogin { get; set; }
        public int ActionTypeId { get; set; }
    }
}
