using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerUpdateInfo
    {
        public client client { get; set; }
        public List<clientAddress> clientAddress { get; set; }
    }
}
