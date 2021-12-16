using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.EmailHelper.Dto
{
    public class MessageDto
    {
        public string Subject { get; set; }

        public string From { get; set; }

        public List<MimeKit.InternetAddress> To { get; set; }

        public string Content { get; set; }
    }
}
