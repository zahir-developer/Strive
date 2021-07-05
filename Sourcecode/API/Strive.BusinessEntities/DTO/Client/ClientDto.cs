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
        public List<ClientVehicle> ClientVehicle { get; set; }
        public List<ClientAddress> ClientAddress { get; set; }

        public Guid? Token { get; set; }
    }
}
