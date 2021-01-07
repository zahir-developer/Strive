﻿using Microsoft.Extensions.Caching.Distributed;
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
using System.Drawing;
using System.Security.Cryptography;
using Strive.BusinessEntities.Document;

namespace Strive.BusinessLogic.Document
{
    public class DocumentBpl : Strivebase, IDocumentBpl
    {
        public DocumentBpl(IDistributedCache cache, ITenantHelper tenantHelper) : base(tenantHelper, cache)
        {
        }

        public Result UploadEmployeeDocument(EmployeeDocumentModel documentModel)
        {
            try
            {
                string error = ValidateEmployeeFiles(documentModel.EmployeeDocument);
                if (!(error == string.Empty))
                {
                    _result = Helper.ErrorMessageResult(error);
                }

                documentModel.EmployeeDocument = UploadEmployeeFiles(documentModel.EmployeeDocument);
                var documentSave = false;
                if (documentModel.EmployeeDocument != null)
                {
                    if (!documentModel.EmployeeDocument.Any(s => s.Filename == string.Empty))
                    {
                        documentSave = SaveEmployeeDocument(documentModel);

                        if (!documentSave)
                        {
                            ArchiveEmployeeFiles(documentModel.EmployeeDocument);
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

        public string ValidateEmployeeFiles(List<EmployeeDocument> employeeDocument)
        {
            string error = string.Empty;
            foreach (var doc in employeeDocument)
            {
                error = ValidateFileFormat(GlobalUpload.DocumentType.EMPLOYEEDOCUMENT, doc.Filename);
                if (!string.IsNullOrEmpty(error))
                    return error;
            }
            return error;
        }

        public List<EmployeeDocument> UploadEmployeeFiles(List<EmployeeDocument> employeeDocuments)
        {

            foreach (var doc in employeeDocuments)
            {
                doc.Filename = Upload(GlobalUpload.DocumentType.EMPLOYEEDOCUMENT, doc.Base64, doc.Filename);
                doc.FileType = Path.GetExtension(doc.Filename);
                if (doc.Filename == string.Empty)
                {
                    ArchiveEmployeeFiles(employeeDocuments);
                }
            }

            return employeeDocuments;
        }

        public string Upload(GlobalUpload.DocumentType uploadFolder, string Base64Url, string fileName)
        {
            string uploadPath = GetUploadFolderPath(uploadFolder);
            fileName = fileName.Replace(Path.GetExtension(fileName), string.Empty) + "_" + Guid.NewGuid().ToString() + Path.GetExtension(fileName);
            if (!File.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            uploadPath = uploadPath + fileName;
            byte[] tempBytes = Convert.FromBase64String(Base64Url);
            File.WriteAllBytes(uploadPath, tempBytes);
            return fileName;

        }

        public string ValidateFileFormat(GlobalUpload.DocumentType upload, string fileName)
        {
            string invalid = string.Empty;
            if (GlobalUpload.DocumentType.EMPLOYEEDOCUMENT == upload)
            {
                if (_tenant.DocumentFormat.Contains(Path.GetExtension(fileName).ToUpper()))
                    return string.Empty;
                else
                    return "Invalid file format uploaded. Valid formats: " + _tenant.DocumentFormat;
            }
            else if (GlobalUpload.DocumentType.PRODUCTIMAGE == upload)
            {
                if (_tenant.ProductImageFormat.Contains(Path.GetExtension(fileName).ToUpper()))
                    return string.Empty;
                else
                    return invalid + _tenant.ProductImageFormat;
            }
            else if (GlobalUpload.DocumentType.LOGO == upload)
            {
                if (_tenant.LogoImageFormat.Contains(Path.GetExtension(fileName).ToUpper()))
                    return string.Empty;
                else
                    return invalid + _tenant.LogoImageFormat;
            }
            else
                return string.Empty;
        }

        public void ArchiveEmployeeFiles(List<EmployeeDocument> documents)
        {
            string uploadPath = GetUploadFolderPath(GlobalUpload.DocumentType.EMPLOYEEDOCUMENT);

            string filePath = string.Empty;
            foreach (var doc in documents)
            {
                filePath = uploadPath + doc.Filename;
                if (File.Exists(uploadPath))
                {
                    File.Move(filePath, GlobalUpload.ArchiveFolder.ARCHIVED.ToString() + "\\" + doc.Filename);
                }
            }
        }

        public bool SaveEmployeeDocument(EmployeeDocumentModel documentModel)
        {
            return new DocumentRal(_tenant).SaveEmployeeDocument(documentModel);
        }

        public Result GetEmployeeDocumentById(int documentId, string password)
        {
            try
            {
                var document = new DocumentRal(_tenant).GetEmployeeDocumentById(documentId);
                string base64 = string.Empty;
                if (document != null)
                {
                    if (document.IsPasswordProtected)
                    {
                        if (document.Password == password)
                        {
                            base64 = GetBase64(GlobalUpload.DocumentType.EMPLOYEEDOCUMENT, document.FileName);
                            document.Base64Url = base64;
                        }
                        else
                        {
                            string errorMessage = "Invalid Password !!!";
                            _result = Helper.ErrorMessageResult(errorMessage);
                            return _result;
                        }
                    }
                    else
                    {
                        base64 = GetBase64(GlobalUpload.DocumentType.EMPLOYEEDOCUMENT, document.FileName);
                        document.Base64Url = base64;
                    }
                }

                _resultContent.Add(document.WithName("Document"));
                _result = Helper.BindSuccessResult(_resultContent);
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public string GetBase64(GlobalUpload.DocumentType module, string fileName)
        {
            string baseFolder = GetUploadFolderPath(module);

            string path = baseFolder + fileName;

            string base64data = string.Empty;

            if (!File.Exists(path))
                return string.Empty;

            using ( FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                byte[] data = new byte[(int)fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                base64data = Convert.ToBase64String(data);
            }

            return base64data;
        }

        public Result GetEmployeeDocumentByEmployeeId(int employeeId)
        {
            try
            {
                var lstDocumentById = new DocumentRal(_tenant).GetEmployeeDocumentByEmployeeId(employeeId);
                if (lstDocumentById.Count > 0)
                {
                    foreach (var item in lstDocumentById)
                    {
                        item.Base64Url = GetBase64(GlobalUpload.DocumentType.EMPLOYEEDOCUMENT, item.FileName);
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

        public Result DeleteEmployeeDocument(int documentId)
        {
            try
            {
                return ResultWrap(new DocumentRal(_tenant).DeleteEmployeeDocument, documentId, "Result");
            }
            catch (Exception ex)
            {
                _result = Helper.BindFailedResult(ex, HttpStatusCode.Forbidden);
            }
            return _result;
        }

        public void ArchiveFile(GlobalUpload.DocumentType uploadFolder, string fileName)
        {
            string filePath = GetUploadFolderPath(uploadFolder) + fileName;
            if (File.Exists(filePath))
            {
                File.Move(filePath, GetUploadFolderPath(uploadFolder) + GlobalUpload.ArchiveFolder.ARCHIVED.ToString() + "\\" + fileName);
            }
        }

        public void DeleteFile(GlobalUpload.DocumentType uploadFolder, string fileName)
        {
            string filePath = GetUploadFolderPath(uploadFolder) + fileName;
            if (File.Exists(filePath))
            {
                string archiveFolder = GetUploadFolderPath(uploadFolder) + GlobalUpload.ArchiveFolder.ARCHIVED.ToString();
                if (!File.Exists(archiveFolder))
                    Directory.CreateDirectory(archiveFolder);
                File.Move(filePath, archiveFolder + "\\" + fileName);
            }
        }

        private string GetUploadFolderPath(GlobalUpload.DocumentType module)
        {
            string path = string.Empty;
            string subPath = string.Empty;
            switch (module)
            {
                case GlobalUpload.DocumentType.EMPLOYEEDOCUMENT:
                    subPath = _tenant.DocumentUploadFolder;
                    break;
                case GlobalUpload.DocumentType.PRODUCTIMAGE:
                    subPath = _tenant.ProductImageFolder;
                    break;
                case GlobalUpload.DocumentType.LOGO:
                    subPath = _tenant.LogoImageFolder;
                    break;

                default:
                    subPath = _tenant.GeneralDocumentFolder + module.ToString() + "\\" ;
                    break;
            }

            subPath = subPath.Replace("TENANT_NAME", _tenant.SchemaName);

            return path + subPath;

        }

        public void SaveThumbnail(int Width, int Height, string base64String, string saveFilePath)
        {
            byte[] byteBuffer = Convert.FromBase64String(base64String);

            var streamImg = new MemoryStream(byteBuffer);
            Bitmap sourceImage = new Bitmap(streamImg);
            using (Bitmap objBitmap = new Bitmap(Width, Height))
            {
                objBitmap.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                using (Graphics objGraphics = Graphics.FromImage(objBitmap))
                {
                    // Set the graphic format for better result cropping   
                    objGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    objGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    objGraphics.DrawImage(sourceImage, 0, 0, Width, Height);

                    // Save the file path, note we use png format to support png file   
                    objBitmap.Save(saveFilePath);
                }
            }
        }

        public int AddDocument(DocumentDto documentModel)
        {
           
            string fileName = Upload(documentModel.DocumentType, documentModel.Document.Base64, documentModel.Document.FileName);

            documentModel.Document.OriginalFileName = documentModel.Document.FileName;
            documentModel.Document.FileName = fileName;
            documentModel.Document.FilePath = GetUploadFolderPath(documentModel.DocumentType) + fileName;

            var result = new DocumentRal(_tenant).AddDocument(documentModel);

            if (!(result > 0))
            {
                DeleteFile(documentModel.DocumentType, fileName);
            }

            return result;
        }

        public Result GetDocument(int documentTypeId, GlobalUpload.DocumentType documentType)
        {
            var document = new DocumentRal(_tenant).GetDocument(documentTypeId);

            if (document.Document != null)
            {
                document.Document.Base64 = GetBase64(documentType, document.Document.FileName);
            }
            _resultContent.Add(document.WithName("Document"));
            _result = Helper.BindSuccessResult(_resultContent);

            return _result;
        }
        public Result DeleteDocument(int documentTypeId, GlobalUpload.DocumentType documentType)
        {
            var docRal = new DocumentRal(_tenant);
            var doc = docRal.GetDocument(documentTypeId);
            var result = docRal.DeleteDocument(documentTypeId);

            if (result)
            {
                DeleteFile(documentType, doc.Document.FileName);
            }

            _resultContent.Add(result.WithName("Result"));
            _result = Helper.BindSuccessResult(_resultContent);

            return _result;
        }

        public DocumentViewModel GetDocumentById(int documentId, GlobalUpload.DocumentType documentType)
        {
            var document = new DocumentRal(_tenant).GetDocumentById(documentId);

            document.Document.Base64 = GetBase64(documentType, document.Document.FileName);
            
            return document;
        }

        public bool DeleteDocumentById(int documentId, GlobalUpload.DocumentType documentType)
        {
            var docRal = new DocumentRal(_tenant);
          
            var doc = docRal.GetDocumentById(documentId);
            var result = docRal.DeleteDocument(documentId);

            if (result)
            {
                DeleteFile(documentType, doc.Document.FileName);
            }
            
            return result;
        }


    }

}
