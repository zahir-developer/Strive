using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Collisions
{
    public class GetCollisions
    {
        public Collision Collision { get; set; }
    }
    public class Collision
    {
        public List<Liability> Liability { get; set; }
        public List<LiabilityDetail> LiabilityDetail { get; set; }
    }
    public class Liability
    {
        public string LiabilityId { get; set; }
        public int EmployeeId { get; set; }
        public int? ClientId { get; set; }
        public int? VehicleId { get; set; }
        public int? TypeId { get; set; }
        public string LiabilityType { get; set; }
        public string Description { get; set; }
        public float Amount { get; set; }
        public string CreatedDate { get; set;}

    }
    public class LiabilityDetail
    {
        public int LiabilityDetailId { get; set; }
        public int LiabilityId { get; set; }
        public int LiabilityDetailType { get; set; }
        public float Amount { get; set; }
        public int PaymentType { get; set; }
        public string DocumentPath { get; set; }
        public string Description { get; set; }
        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
