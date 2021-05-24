namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeHourlyRateDto
    {
        public int EmployeeHourlyRateId { get; set; }
        public int EmployeeId { get; set; }
        public int LocationId { get; set; }
        public int? RoleId { get; set; }
        public decimal HourlyRate { get; set; }
        public string RoleName { get; set; }
        public string LocationName { get; set; }
    }
}