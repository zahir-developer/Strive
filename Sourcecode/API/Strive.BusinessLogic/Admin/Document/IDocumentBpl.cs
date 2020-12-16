using Strive.BusinessEntities.Document;
using Strive.BusinessEntities.Model;
using Strive.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Document
{
    public interface IDocumentBpl
    {
        Result UploadEmployeeDocument(EmployeeDocumentModel documentModel);
        bool SaveEmployeeDocument(EmployeeDocumentModel documentModel);
        List<EmployeeDocument> UploadEmployeeFiles(List<EmployeeDocument> employeeDocuments);
        void ArchiveEmployeeFiles(List<EmployeeDocument> documents);
        string Upload(GlobalUpload.DocumentType uploadFolder, string Base64Url, string fileName);
        Result GetEmployeeDocumentById(int documentId, string password);
        Result UpdatePassword(int documentId, string password);
        Result GetEmployeeDocumentByEmployeeId(int employeeId);
        Result DeleteEmployeeDocument(int documentId);
        void DeleteFile(GlobalUpload.DocumentType uploadFolder, string fileName);
        void SaveThumbnail(int Width, int Height, string base64String, string saveFilePath);
        string ValidateEmployeeFiles(List<EmployeeDocument> employeeDocument);
        Result AddDocument(DocumentDto documentModel);
        Result GetDocument(int documentTypeId, GlobalUpload.DocumentType documentType);
        Result DeleteDocument(int documentTypeId, GlobalUpload.DocumentType documentType);
        void ArchiveFile(GlobalUpload.DocumentType uploadFolder, string fileName);
        DocumentViewModel GetDocumentById(int documentId, GlobalUpload.DocumentType documentType);
        bool DeleteDocumentById(int documentId, GlobalUpload.DocumentType documentType);

    }
}
