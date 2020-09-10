using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerSignUpRequest
    {
       public int clientId { get; set; }
       public string firstName { get; set; }
       public string middleName { get; set; }
       public string lastName { get; set; }
       public int gender { get; set; }
       public int maritalStatus { get; set; }
       public DateTime birthDate { get; set; }
       public DateTime createdDate { get; set; }
       public bool isActive { get; set; }
       public string notes { get; set; }
       public string recNotes { get; set; }
       public int score { get; set; }
       public bool noEmail { get; set; }
       public int clientType { get; set; }

    }
}
