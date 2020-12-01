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
        public int employeeDocumentId { get; set; }
        public int employeeId { get; set; }
        public string filename { get; set; }
        public string filepath { get; set; }
        public string base64 { get; set; }
        public string fileType { get; set; }
        public bool isPasswordProtected { get; set; }
        public string password { get; set; }
        public string comments { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public int createdBy { get; set; }
        public string createdDate { get; set; }
        public int updatedBy { get; set; }
        public string updatedDate { get; set; }


    }
}
