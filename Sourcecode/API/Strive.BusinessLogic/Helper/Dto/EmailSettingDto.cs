using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.EmailHelper.Dto
{
    public class EmailSettingDto
    {
        public string FromEmail { get; internal set; }
        public string PrimaryDomain { get; internal set; }
        public int PrimaryPort { get; internal set; }
        public string UsernameEmail { get; internal set; }
        public string UsernamePassword { get; internal set; }
    }
}
