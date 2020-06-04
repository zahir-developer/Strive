using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Common
{
    public class GlobalConstants
    {
    }

    public class GData
    {
        public string gMasterConnectionString { get; set; }
        public string gTenantConnectionString { get; set; }
        public string gToken { get; set; }
        public string gSecretKey { get; set; }
        public string gDbName { get; set; }
        public string gCompany { get; set; }
        public string gSMTPClient { get; set; }
        public string gFromAddress { get; set; }
        public string gPassword { get; set; }
        public string gPort { get; set; }

        public string LoginId { get; set; }

        public string AheadLogFilePath { get; set; }
        public string AheadLogFileName { get; set; }
    }

}
