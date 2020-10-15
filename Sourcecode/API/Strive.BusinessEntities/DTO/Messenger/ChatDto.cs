using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Messenger
{
    public class ChatDto
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public int GroupId { get; set; }
    }
}
