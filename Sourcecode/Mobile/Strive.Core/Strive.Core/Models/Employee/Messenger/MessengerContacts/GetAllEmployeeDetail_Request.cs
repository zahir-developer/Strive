using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Messenger.MessengerContacts
{
    public class GetAllEmployeeDetail_Request
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int? locationId { get; set; }
        public int? pageNo { get; set; }
        public int? pageSize { get; set; }
        public string query { get; set; }
        public string sortOrder { get; set; }
        public string sortBy { get; set; }
        public bool status { get; set; }

    }
}
