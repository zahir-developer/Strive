using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Strive.BusinessEntities.Client
{
    public class ClientView: Client
    {
        public List<ClientAddress> ClientAddress { get; set; }

    }
}
