using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Documents
{
    public class AddDocuments
    {
        public List<employeeDocument> employeeDocument { get; set; }
    }
    public class employeeDocument
    {
        public int employeeDocumentId { get; set; } = 0;
        public int employeeId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string base64 { get; set; }
        public string fileType { get; set; }
        public bool isPasswordProtected { get; set; }
        public string password { get; set; }
        public string comments { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int documentType { get; set; }
        public int documentSubType { get; set; }
        public int createdBy { get; set; } = 0;
        public string createdDate { get; set; }
        public int updatedBy { get; set; } = 0;
        public string updatedDate { get; set; }
    }
}
