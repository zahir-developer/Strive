using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Customer
{
    public class CustomerSignUp
    {
        public class SignUpRequest
        {
            public Client client { get; set; }
            public List<ClientAddress> clientAddress;
            public List<ClientVehicle> clientVehicle;
            public string token { get; set; }
            public string password { get; set; }
        }
       public class Client
       {
            public int clientId { get; set; }
            public string firstName { get; set; }
            public string middleName { get; set; }
            public string lastName { get; set; }
            public int? gender { get; set; }
            public int? maritalStatus { get; set; }
            public string birthDate { get; set; }
            public string notes { get; set; }
            public string recNotes { get; set; }
            public int? score { get; set; }
            public int? clientType { get; set; }
            public bool isActive { get; set; }
            public bool isDeleted { get; set; }
            public int createdBy { get; set; }
            public int updatedBy { get; set; }
            public string createdDate { get; set; }
            public string updatedDate { get; set; }
            public bool isCreditAccount { get; set; }
       }

        public class ClientAddress
        {

            public int clientAddressId { get; set; }
            public int clientId { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string phoneNumber { get; set; }
            public string phoneNumber2 { get; set; }
            public string email { get; set; }
            public int? city { get; set; }
            public int? state { get; set; }
            public int? country { get; set; }
            public string zip { get; set; }
            public bool isActive { get; set; }
            public bool isDeleted { get; set; }
            public int createdBy { get; set; }
            public int updatedBy { get; set; }
            public string createdDate { get; set; }
            public string updatedDate { get; set; }
        }
        public class ClientVehicle
        {
            public int vehicleId { get; set; }
            public int? locationId { get; set; }
            public string vehicleNumber { get; set; }
            public int vehicleMfr { get; set; }
            public int vehicleModel { get; set; }
            public int? vehicleModelNo { get; set; }
            public string vehicleYear { get; set; }
            public int vehicleColor { get; set; }
            public int? upcharge { get; set; }
            public string barcode { get; set; }
            public string notes { get; set; }
            public bool isActive { get; set; }
            public bool isDeleted { get; set; }
            public int createdBy { get; set; }
            public int updatedBy { get; set; }
            public string createdDate { get; set; }
            public string updatedDate { get; set; }
        }


    }
}
