using System;
using Newtonsoft.Json;
namespace Strive.Core.Models.Customer
{
    
    public partial class AddDocument
    {
        [JsonProperty("document")]
        public Document Document { get; set; }

        [JsonProperty("documentType")]
        public string DocumentType { get; set; }
    }

    public partial class Document
    {
        [JsonProperty("documentId")]
        public int DocumentId { get; set; }

        [JsonProperty("documentName")]
        public string DocumentName { get; set; }

        [JsonProperty("documentType")]
        public int DocumentType { get; set; }

        [JsonProperty("documentSubType")]
        public int? DocumentSubType { get; set; }

        [JsonProperty("roleId")]
        public int? RoleId { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("originalFileName")]
        public string OriginalFileName { get; set; }

        [JsonProperty("filePath")]
        public string FilePath { get; set; }

        [JsonProperty("base64")]
        public string Base64 { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdBy")]
        public long CreatedBy { get; set; }

        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("updatedBy")]
        public long UpdatedBy { get; set; }

        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }
    }

}
