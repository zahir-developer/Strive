using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class clientAddress
    {
        public int addressId { get; set; }
        public int relationshipId { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string phoneNumber { get; set; }
        public string phoneNumber2 { get; set; }
        public string email { get; set; }
        public int city { get; set; }
        public int state { get; set; }
        public int country { get; set; }
        public string zip { get; set; }
        public bool isActive { get; set; }



    }
}
