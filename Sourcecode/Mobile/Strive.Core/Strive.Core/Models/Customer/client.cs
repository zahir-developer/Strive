using Strive.Core.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class client
    {
        public  int clientId { get; set; }
        public  string firstName { get; set; }
        public string middleName { get; set; } = "";
        public  string lastName { get; set; }
        public int? gender { get; set; } = null;
        public  int? maritalStatus { get; set; } = null;
        public  string birthDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public string notes { get; set; } = "";
        public string recNotes { get; set; } = "";
        public  int score { get; set; }
        public  bool noEmail { get; set; }
        public  int? clientType { get; set; } = 199;
        public int authId { get; set; }
        public bool isActive { get; set; } = true;
        public  bool isDeleted { get; set; }
        public  int createdBy { get; set; }
        public  string createdDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
        public  int updatedBy { get; set; }
        public  string updatedDate { get; set; } = DateUtils.ConvertDateTimeWithZ();
    }
}
