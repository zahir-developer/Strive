using Strive.BusinessEntities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.DTO.Client
{
    public class ClientDto
    {
        public Model.Client Client { get; set; }
        public ClientAddress ClientAddress { get; set; }
    }
}
