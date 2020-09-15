using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
   public class CustomerInfoModel
    {
        public client client { get; set;}
        public List<clientVehicle> clientVehicle { get; set; }
        public List<clientAddress> clientAddress { get; set; }
    }
    
}
