using System;

namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeDocumentDto
    {
        public int DocumentSequence { get; set; }
        public int EmployeeId { get; set; }
        public int EmployeeDocumentId { get; set; }
        public string FileName { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
    }
}