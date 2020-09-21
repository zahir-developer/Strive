using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class clientAddress
    {
        public  int clientAddressId { get; set; }
        public  int clientId { get; set; }
        public  string address1 { get; set; }
        public  string address2 { get; set; }
        public  string phoneNumber { get; set; }
        public  string phoneNumber2 { get; set; }
        public  string email { get; set; }
        public int? city { get; set; } = null;
        public  int? state { get; set; } = null;
        public  int? country { get; set; } = null;
        public  string zip { get; set; }
        public  bool isActive { get; set; }
        public  bool isDeleted { get; set; }
        public  int createdBy { get; set; }
        public  string createdDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public  int updatedBy { get; set; }
        public  string updatedDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
    }
}
