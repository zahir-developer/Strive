using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ChatMessageDetail
    {
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public int ReceipientId { get; set; }
        public string ReceipientName { get; set; }
        public string MessageBody { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
