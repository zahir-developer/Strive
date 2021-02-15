using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Documents
{
    public class DownloadDocuments
    {
        public Document Document { get; set; }
    }
    public class Document
    {
        public int EmployeeDocumentId { get; set; }
        public int EmployeeId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Password { get; set; }
        public bool IsPasswordProtected { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public string Base64Url { get; set; }
    }
}
