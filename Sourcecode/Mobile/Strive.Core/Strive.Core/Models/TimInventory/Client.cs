using System;
using System.Collections.Generic;

namespace Strive.Core.Models.TimInventory
{
    public class ClientInfo
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
        public List<ClientInfo> Client { get; set; }
    }

    public class ClientsSearch
    {
        public List<ClientInfo> ClientSearch { get; set; }
    }

    public class ClientDetail
    {
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public int MaritalStatus { get; set; }
        public DateTime BirthDate { get; set; }
        public string Notes { get; set; }
        public string RecNotes { get; set; }
        public int Score { get; set; }
        public bool NoEmail { get; set; }
        public int ClientType { get; set; }
        public int ClientAddressId { get; set; }
        public int ClientRelatioshipId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string Email { get; set; }
        public int City { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public string Zip { get; set; }
        public bool IsActive { get; set; }
    }

    public class ClientStatus
    {
        public List<ClientDetail> Status { get; set; }
    }
}
