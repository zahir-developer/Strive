using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerUpdateVehicle
    {
        public client client { get; set; }
        public List<clientVehicle> clientVehicle { get; set; }

    }
}
