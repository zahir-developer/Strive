using System;

namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeLiabilityDto
    {
        public int CollisionSequence { get; set; }
        public int EmployeeId { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public string VehicleName { get; set; }


        public int? VehicleId { get; set; }

        public int LiabilityId { get; set; }
        public int liabilityDetailId { get; set; }
        public string LiabilityType { get; set; }
        public string LiabilityDetailType { get; set; }
        public string LiabilityDescription { get; set; }
        public decimal? Amount { get; set; }
        public string LiabilityDetailTypeId { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
    }
}