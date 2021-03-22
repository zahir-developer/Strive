namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeLocationDto
    {
        public int EmployeeLocationId { get; set; }
        public int EmployeeId { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public string CityName { get; set; }
    }
}