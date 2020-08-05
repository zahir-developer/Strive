using System;

namespace Strive.BusinessEntities.DTO.Employee
{
    public class EmployeeDocumentDto
    {
        public int DocumentSequence { get; set; }
        public int EmployeeId { get; set; }
        public int DocumentId { get; set; }
        public string FileName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
    }
}