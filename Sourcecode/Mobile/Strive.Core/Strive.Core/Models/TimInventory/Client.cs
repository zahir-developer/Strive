using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class ClientDetail
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public int ClientType { get; set; }
        public string PhoneNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
    }

    public class Clients
    {
        public List<ClientDetail> Client { get; set; }
    }
}
