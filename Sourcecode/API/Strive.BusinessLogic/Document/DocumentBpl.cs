using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Strive.BusinessEntities.Model;
using Strive.Common;
using Strive.ResourceAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessLogic.Document
{
    public class DocumentBpl : Strivebase, IDocumentBpl
    {
        public DocumentBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }

        public Result UploadDocument(EmployeeDocumentModel documentModel)
        {
            try
            {
                documentModel.EmployeeDocument = UploadFiles(documentModel.EmployeeDocument);
                var documentSave = false;
                if (documentModel.EmployeeDocument != null)
                {
                    if (!documentModel.EmployeeDocument.Any(s => s.Filename == string.Empty))
                    {
                        documentSave = SaveDocument(documentModel);

                        if (!documentSave)
                        {
                            DeleteFiles(documentModel.EmployeeDocument);
                        }

                        _resultContent.Add(documentSave.WithName("Status"));
                        _result = Helper.BindSuccessResult(_resultContent);
                    }
                    else
                    {
                        documentSave = false;
                        _resultContent.Add(documentSave.WithName("Status"));
                        _result = Helper.BindSuccessResult(_resultContent);
                    }

                }
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public List<EmployeeDocument> UploadFiles(List<EmployeeDocument> employeeDocuments)
        {
            foreach (var doc in employeeDocuments)
            {
                doc.Filename = Upload(doc.Base64, doc.Filename);
                doc.FileType = Path.GetExtension(doc.Filename);
                if (doc.Filename == string.Empty)
                {
                    DeleteFiles(employeeDocuments);
                }
            }

            return employeeDocuments;
        }

        public string Upload(string Base64Url, string fileName)
        {
            fileName = fileName.Replace(Path.GetExtension(fileName), string.Empty) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(fileName);
            var fileFormat = Path.GetExtension(fileName);
            if (ValidateFileFormat(fileName))
            {
                string UploadPath = _tenant.DocumentUploadFolder + fileName;
                byte[] tempBytes = Convert.FromBase64String(Base64Url);
                File.WriteAllBytes(UploadPath, tempBytes);
                return fileName;
            }
            else
            {
                string errorMessage = "Invalid File Format. Valid file formats: " + _tenant.DocumentFormat;
                _result = Helper.ErrorMessageResult(errorMessage);
                return string.Empty;
            }
        }

        public bool ValidateFileFormat(string fileName)
        {
            return _tenant.DocumentFormat.Contains(Path.GetExtension(fileName));
        }

        public void DeleteFiles(List<EmployeeDocument> documents)
        {
            string UploadPath = _tenant.DocumentUploadFolder;

            string filePath = string.Empty;
            foreach (var doc in documents)
            {
                filePath = UploadPath + doc.Filename;
                if (File.Exists(UploadPath))
                {
                    File.Delete(UploadPath);
                }
            }
        }

        public bool SaveDocument(EmployeeDocumentModel documentModel)
        {
            return new DocumentRal(_tenant).SaveDocument(documentModel);
        }


        public Result GetDocumentById(int documentId, string password)
        {
            try
            {
                var document = new DocumentRal(_tenant).GetDocumentById(documentId);
                string base64 = string.Empty;
                if (document != null)
                {
                    if (document.IsPasswordProtected)
                    {
                        if (document.Password == password)
                            base64 = GetBase64(document.FileName);
                        else
                        {
                            string errorMessage = "Invalid Password !!!";
                            _result = Helper.ErrorMessageResult(errorMessage);
                            return _result;
                        }
                    }
                    else
                    {
                        base64 = GetBase64(document.FileName);
                    }
                }

                _resultContent.Add(base64.WithName("Document"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public string GetBase64(string fileName)
        {
            string path = _tenant.DocumentUploadFolder + fileName;
            string base64data = string.Empty;

            if (!File.Exists(path))
                return string.Empty;

            using (FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[(int)fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                base64data = Convert.ToBase64String(data);
            }

            return base64data;
        }

        public Result GetDocumentByEmployeeId(int employeeId)
        {
            try
            {
                var lstDocumentById = new DocumentRal(_tenant).GetDocumentByEmployeeId(employeeId);
                if (lstDocumentById.Count > 0)
                {
                    foreach (var item in lstDocumentById)
                    {
                        item.Base64Url = GetBase64(item.FileName);
                    }
                }

                _resultContent.Add(lstDocumentById.WithName("GetAllDocuments"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }
        public Result UpdatePassword(int documentId, string password)
        {
            try
            {
                return ResultWrap(new DocumentRal(_tenant).UpdatePassword, documentId, password, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;

        }
        public Result DeleteDocument(int documentId)
        {
            try
            {
                return ResultWrap(new DocumentRal(_tenant).DeleteDocument, documentId, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

    }
}
