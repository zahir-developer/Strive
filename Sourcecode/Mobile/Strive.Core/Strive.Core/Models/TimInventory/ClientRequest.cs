using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class ClientRequest
    {
        public int locationId { get; set; }
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public string query { get; set; }
        public string sortOrder { get; set; }
        public string sortBy { get; set; }
        public bool status { get; set; }
    }

    public class ClientViewModel
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Amount { get; set; }
        public bool IsActive { get; set; }
        public int ClientType { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Type { get; set; }
    }

    public class CountValue
    {
        public int Count { get; set; }
    }

    public class Client
    {
        public List<ClientViewModel> clientViewModel { get; set; }
        public CountValue Count { get; set; }
    }

    public class ClientResponse
    {
        public Client Client { get; set; }
    }
}
