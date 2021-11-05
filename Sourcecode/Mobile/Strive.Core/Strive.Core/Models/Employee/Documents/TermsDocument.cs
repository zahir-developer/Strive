using System;
using System.Collections.Generic;
using System.Text;

namespace Strive.Core.Models.Employee.Documents
{
    public class TermsDocument
    {
        public Document2 Document { get; set; }
    }
    public class Document2
    {
        public Document1 Document { get; set; }
    }
    public class Document1
    {
        public int DocumentId { get; set; }
        public int EmployeeId { get; set; }
        public string FileName { get; set; }
        public string OriginalFileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
        public string Password { get; set; }
        public bool IsPasswordProtected { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedDate { get; set; }
        public bool IsActive { get; set; }
        public string Base64 { get; set; }
    }

}
